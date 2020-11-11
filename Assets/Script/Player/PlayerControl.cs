using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Child")]
    private Transform trans;
    private Vector2 startPoint;
    [SerializeField]
    private Transform muzzle = null;
    [SerializeField]
    private Transform projectile = null;
    [SerializeField]
    private Transform impact = null;
    [SerializeField]
    private Transform forwardPoint = null;
    [SerializeField]
    private Transform ship;
    [SerializeField]
    private Transform deadAnim;


    private WeaponControl weaponControl;
    private List<WeaponBehavior> activeGunList;
    private Vector3 newDestination;


    [Header("Stat")]
    [SerializeField]
    private float rof = 1f;
    [SerializeField]
    private float timeAttack = 0f;
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private int fireRate = 1;
    [SerializeField]
    private float spaceBetweenProjectile = 0.1f;
    [SerializeField]
    private float speed = 1f;
    private float currentSpeed;
    private Coroutine slowDownCoroutine;

    public int FireRate { get { return fireRate; } set { fireRate = value; } }


    private bool isFire = false;

    

    public event Action OnPlayerDead;

    private List<BuffDebuffData> buffDebuffDatas = new List<BuffDebuffData>();
    public event Action<string,int,float> AddBuffDebuffEvent;
    public event Action<int, float> BuffDebuffProgressEvent;
    public event Action<int> RemoveBuffDebuffEvent;


    

    private void Awake()
    {
        trans = transform;
        Pool projectilePool = new Pool(projectile.name, 100, projectile);
        Pool impactPool = new Pool(impact.name, 100, impact);

        PoolManager.instance.AddNewPool(projectilePool);
        PoolManager.instance.AddNewPool(impactPool);

        weaponControl = GetComponentInChildren<WeaponControl>();
        
        timeAttack = rof;
        currentSpeed = speed;
    }

    private void Start()
    {

       
        MissionManager.instance.OnMissionClearEvent += OnMissionClearEvent;
        MissionManager.instance.OnMissionStart += StartGame;

        List<int> activeGuns = new List<int>();
        activeGuns = DataAPIController.instance.GetAllActiveGun();
        activeGunList = new List<WeaponBehavior>();
        foreach(var g in activeGuns)
        {
            ConfigGunRecord record = ConfigurationManager.instance.gun.GetRecordByKeySearch(g);
            GameObject obj = Instantiate(Resources.Load(record.Prefab)) as GameObject;
            obj.transform.SetParent(weaponControl.transform);
            obj.transform.localPosition = Vector3.zero;
            obj.gameObject.SetActive(false);
            WeaponBehavior gun = obj.GetComponent<WeaponBehavior>();
            gun.Setup(record);
            activeGunList.Add(gun);
        }

        int enquipId = DataAPIController.instance.GetCurrentEnquipId();
        ChangeEnquip(enquipId);
        DataAPIController.instance.RegisterEvent(DataPath.ENQUIPS, ChangeEnquip);



        StartAnim();
    }

    private void ChangeEnquip(object obj)
    {
        int id = (int)obj;
        foreach (var g in activeGunList)
        {
            g.gameObject.SetActive(false);
            if (g.id == id)
            {
                g.gameObject.SetActive(true);
            }
        }
    }

    private void StartGame()
    {
        startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        InputManager.instance.OnControlDown += OnControlDown;
        InputManager.instance.OnControlOnDown += OnControlOnDown;
        InputManager.instance.OnControlUp += OnControlUp;
        isFire = true;
        ConfigPlayerDamageRecord cfDmg = ConfigurationManager.instance.playerDamage.GetRecordByKeySearch(DataAPIController.instance.GetCurrentDamageLevel());
        damage = cfDmg.Value;
        ConfigPlayerFireRateRecord cfFr = ConfigurationManager.instance.playerFireRate.GetRecordByKeySearch(DataAPIController.instance.GetCurrentFireRateLevel());
        fireRate = cfFr.Value;
        weaponControl.enabled = true;

        StartCoroutine(CheckBuffDebuff());
    }
   

    private void Update()
    {
        if(timeAttack >= rof && isFire)
        {
            Fire();
            timeAttack = 0;
        }

        timeAttack += Time.deltaTime;
        if(isFire)
        {
            trans.position = Vector3.MoveTowards(trans.position, newDestination, Time.deltaTime * currentSpeed);
        }
     
    }

   IEnumerator CheckBuffDebuff()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.2f);
        while(true)
        {
            List<BuffDebuffData> removeList = new List<BuffDebuffData>();
            foreach (var d in buffDebuffDatas)
            {
                d.remainTime -= 0.2f;
                BuffDebuffProgressEvent?.Invoke(d.cf.Id, d.remainTime);
                if (d.remainTime <= 0)
                {
                    removeList.Add(d);
                }
            }

            foreach (var d in removeList)
            {
                buffDebuffDatas.Remove(d);
                Type type = this.GetType();
                PropertyInfo prop = type.GetProperty(d.cf.FieldName);
                prop.SetValue(this, d.oldValue, null);
                RemoveBuffDebuffEvent?.Invoke(d.cf.Id);
            }

            yield return waitForSeconds;
        }
    }


    public void Dead()
    {
        isFire = false;
        ship.gameObject.SetActive(false);
        deadAnim.gameObject.SetActive(true);
        StartCoroutine(Deactive(1.5f));

        OnPlayerDead?.Invoke();
    }

    IEnumerator Deactive(float time)
    {
        yield return new WaitForSeconds(time);
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    private void OnMissionClearEvent()
    {
        isFire = false;
        Time.timeScale = 1;
        InputManager.instance.OnControlDown -= OnControlDown;
        InputManager.instance.OnControlOnDown -= OnControlOnDown;
        InputManager.instance.OnControlUp -= OnControlUp;
        EndAnim();
    }


    private void Fire()
    {
        Vector3[] points = GenerateToPos();
        
        foreach(var p in points)
        {
            Transform pj = PoolManager.instance.dicPool[projectile.name].Spwan();
            pj.position = muzzle.position;

            ProjectileData data = new ProjectileData();
            data.damage = damage;
            data.projectilePoolName = projectile.name;
            data.impactPoolName = impact.name;
            data.toPos = p;
            data.toPosTime = 0.1f;
            pj.GetComponent<ProjectileControl>().Setup(data);
        }
        
    }

    private void OnControlUp(Vector2 obj)
    {
        Time.timeScale = 0.1f;
        isFire = false;
    }

    private void OnControlDown(Vector2 point)
    {
        Time.timeScale = 1f;
        isFire = true;
        startPoint = Camera.main.ScreenToWorldPoint(point);
    }

    private void OnControlOnDown(Vector2 point)
    {
        
        
        
        Vector2 p = Camera.main.ScreenToWorldPoint(point);

        Vector3 pos = p - startPoint;

        Vector2 newPosition = trans.position + pos;

        newPosition = Boudary.instance.ClampBoudary(newPosition);

        newDestination = newPosition;

        startPoint = p;
    }

    private Vector3[] GenerateToPos()
    {
        Vector3[] result = new Vector3[fireRate];
        float space = (spaceBetweenProjectile * fireRate) / 2;
        Vector3 startPoint = forwardPoint.position;
        startPoint.x -= space;
        result[0] = startPoint;
        for (int i = 1; i < fireRate; i++)
        {
            Vector3 newPoint = result[i - 1];
            newPoint.x += spaceBetweenProjectile;
            result[i] = newPoint;
        }

        return result;
    }

    private void EndAnim()
    {
        trans.DOMoveY(6, 1f);
    }

    private void StartAnim()
    {
        trans.DOMoveY(-2, 1f);
    }

    public void OnPanelOpen()
    {
        trans.DOMoveY(0, 1f);
    }

    public void OnPanelClose()
    {
        trans.DOMoveY(-2, 1f);
    }

    public void ApplyBuffDebuff(ConfigBuffDebuffRecord cf)
    {
        foreach(var d in buffDebuffDatas)
        {
            if(d.cf.Id == cf.Id)
            {
                d.remainTime = cf.EffectTime;
                AddBuffDebuffEvent?.Invoke(cf.Sprite, cf.Id, cf.EffectTime);
                return;
            }
        }

        Type type = this.GetType();
        PropertyInfo prop = type.GetProperty(cf.FieldName);
        int oldValue = (int)prop.GetValue(this);

        if (cf.IsBuff) prop.SetValue(this, ((int)prop.GetValue(this)) * cf.Value, null);
        else prop.SetValue(this, ((int)prop.GetValue(this)) / cf.Value, null);

        buffDebuffDatas.Add(new BuffDebuffData(cf, oldValue));
        AddBuffDebuffEvent?.Invoke(cf.Sprite, cf.Id, cf.EffectTime);
    }

    public void Slow(float speed)
    {
        
        if (slowDownCoroutine != null)
        {
            StopCoroutine(slowDownCoroutine);
            slowDownCoroutine = null;
        }
        slowDownCoroutine = StartCoroutine(SlowDownSpeed(speed));

    }

    IEnumerator SlowDownSpeed(float modifySpeed)
    {
        float oldSpeed = currentSpeed;
        currentSpeed = modifySpeed;
        yield return new WaitForSeconds(0.5f);
        currentSpeed = oldSpeed;
    }

}

public class BuffDebuffData
{
    public ConfigBuffDebuffRecord cf;
    public int oldValue;
    public float remainTime;

    public BuffDebuffData (ConfigBuffDebuffRecord cf, int oldValue)
    {
        this.cf = cf;
        this.oldValue = oldValue;
        remainTime = cf.EffectTime;
    }
}
