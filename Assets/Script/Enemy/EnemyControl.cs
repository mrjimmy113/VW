using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnEnemyDeadParam
{
    public int coinAmount;
    public bool isCountDead;
    public EnemyControl instance;
    //public int enemyType;
}

public class OnEnemyDamagedParam
{

}

public class EnemyControl : MonoBehaviour
{
    
    [Header("Enemy Child Object")]
    private Transform trans;
    [SerializeField]
    public Transform dir = null;
    public Vector3 direction;
    [SerializeField]
    public Text txtHp = null;
    [SerializeField]
    public CircleCollider2D col = null;
    [SerializeField]
    private Transform kind_1 = null;
    [SerializeField]
    private Transform kind_2 = null;
    [SerializeField]
    public Transform model = null;
    [SerializeField]
    public Transform deadAnim = null;

    private Tween rotateTwin1 = null;
    private Tween rotateTwin2 = null;

    [Header("Enemy Stat")]
    public EnemyInfor inforEnemy;

    public float speed = 1;

    public float Speed { get => speed; set => speed = value; }
    private bool isHit = false;

    [SerializeField]
    public int hp = 1;
    [SerializeField]
    private bool canDuplicate = false;
    [SerializeField]
    private EnemyInfor[] duplication = null;
    [SerializeField]
    private int coinAmount = 0;
    [SerializeField]
    private bool spawnBuffDebuff = false;
    private EnemyExtraBehavior behavior = null;

    [SerializeField]
    private float moveSpeedPercentWhenHitted = 0.5f;
    public float moveDegree;
    public float currentSpeed;
    private Coroutine slowDownCoroutine;
    private bool isDead = false;
    public bool isChild = false;
    public bool isInvincible = false;

    

    public event Action<OnEnemyDeadParam> OnEnemyDead;
    public event Action<OnEnemyDamagedParam> OnEnemyDamaged;
    

    private Coroutine corCanCollideWithDown;
    


    
    private float minStartAngle = 200;
    private float maxStartAngle = 320;

    public bool isCountOnDead = false;

    public List<int> appliedBuffDebuffId = new List<int>();

    public void Setup(EnemyInfor enemyInfor, bool isDuplication, bool isCountOnDead)
    {
        this.inforEnemy = enemyInfor;
        this.isCountOnDead = isCountOnDead;
        hp = enemyInfor.hp;
        canDuplicate = enemyInfor.isDuplicate;
        duplication = enemyInfor.duplicate;
        spawnBuffDebuff = enemyInfor.spawnBuffDebuff;
        trans.localScale = Vector3.one * enemyInfor.sizeScale;
        txtHp.text = hp.ToString();
        coinAmount = enemyInfor.coinAmount;
        if(isDuplication) ChangeDir(20, 160);
        else ChangeDir(minStartAngle, maxStartAngle);

        behavior = GetComponent<EnemyExtraBehavior>();
        if(behavior != null) behavior.Setup();
    }

    

    private void Awake()
    {
        trans = transform;
        currentSpeed = speed;
        col = GetComponent<CircleCollider2D>();
        col.enabled = false;
    }


    private void Start()
    {
        StartCoroutine(CheckCollisionBoudary());
        StartCoroutine(CheckCollisionPlayer());
        rotateTwin1 = kind_1.DOLocalRotate(new Vector3(0, 0, 90), 1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
        rotateTwin2 = kind_2.DOLocalRotate(new Vector3(0, 0, -90), 1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
        OnStart();
    }

    public virtual void OnStart()
    {

    }


    public void SetChild()
    {
        isChild = true;
        StopCoroutine(CheckCollisionBoudary());
    }

    public void RemoveChild()
    {
        trans.SetParent(null);
        isChild = false;
        StartCoroutine(CheckCollisionBoudary());
    }

    private void Update()
    {
        if(!isDead && !isChild) trans.position += direction * Time.deltaTime * currentSpeed;
    }

    private void FixedUpdate()
    {
        if(!isDead && !isChild) col.enabled = Boudary.instance.IsInScreen(trans.position);
    }

    IEnumerator CheckCollisionPlayer()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.05f);
        while (true)
        {
           Collider2D hit2D = Physics2D.OverlapCircle(trans.position, 0.2f * trans.localScale.x, 1 << 12);
            if(hit2D != null)
            {
                hit2D.GetComponent<PlayerControl>().Dead();
                break;
            }

            yield return waitForSeconds;

        }
    }

    IEnumerator CheckCollisionBoudary()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.02f);
        while(true)
        {
            yield return waitForSeconds;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(trans.position, direction, 0.2f * trans.localScale.x, 1 << 11);
            //Collider2D hit2D = Physics2D.OverlapCircle(trans.position, 0.2f * trans.localScale.x, 1 << 11);
            if (raycastHit2D.collider != null)
            {
                Collider2D hit2D = raycastHit2D.collider;
                if (hit2D.gameObject.tag == "RightBoudary")
                {
                    if (moveDegree >= 180)
                    {
                        ChangeDir(260, 200);
                    }
                    else
                    {
                        ChangeDir(100, 170);
                    }
                }
                else if (hit2D.gameObject.tag == "LeftBoudary")
                {
                    if (moveDegree >= 180)
                    {
                        ChangeDir(280, 350);
                    }
                    else
                    {
                        ChangeDir(80, 10);
                    }
                }
                else if (hit2D.gameObject.tag == "UpBoudary")
                {
                    if (moveDegree >= 0 && moveDegree <=180)
                    {
                        
                        if (moveDegree >= 270)
                        {
                            ChangeDir(260, 190);
                        }
                        else
                        {
                            ChangeDir(280, 350);
                        }
                    }
                }


                else if (hit2D.gameObject.tag == "DownBoudary")
                {
                    float yPos = hit2D.transform.position.y - col.radius * trans.localScale.y;
                    if(corCanCollideWithDown == null) 
                        corCanCollideWithDown = StartCoroutine(CollideWithDownCheck(yPos));


                }

              
            }
        }
    }

    IEnumerator CollideWithDownCheck(float yPos)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.05f);
        while(true)
        {
            if(trans.position.y <= yPos)
            {
               
                trans.position = GetTopPos();
                corCanCollideWithDown = null;
                break;
            }

            yield return waitForSeconds;
        }
    }

    public Vector2 GetTopPos()
    {
        Vector2 pos = Boudary.instance.GetPositionOnTop(trans.position);
        pos.y += col.radius * trans.localScale.y;
        return pos;
    }

    public void OnDamage(int dmg)
    {
        if (isInvincible) return;

        if (isDead) return;
             
        if (slowDownCoroutine != null)
        {
            StopCoroutine(slowDownCoroutine);
            slowDownCoroutine = null;
        }
        slowDownCoroutine = StartCoroutine(SlowDownSpeed());
        ReduceHP(dmg);
    }

    public void ReduceHP(int amount)
    {
        hp -= amount;
        txtHp.text = hp.ToString();
        if (hp <= 0) Dead();
        else OnEnemyDamaged?.Invoke(null);
    }

    public void Heal(int heal)
    {
        hp += heal;
        txtHp.text = hp.ToString();
    }

    public void OnDamage(int dmg, Vector3 pos)
    {
        OnDamage(dmg);
        direction = (trans.position - pos).normalized;
        moveDegree = Vector2.Angle(Vector2.right, direction);
    }

    public void OnPush(Vector3 pos)
    {
        direction = (trans.position - pos).normalized;
        moveDegree = Vector2.Angle(Vector2.right, direction);

    }
    
    public void OnFollow(Vector3 pos)
    {
        direction = (pos - trans.position).normalized;
        moveDegree = Vector2.Angle(Vector2.right, direction);
    }

    public void SetDirection(float angle)
    {
        moveDegree = angle;
        float radiant = Mathf.Deg2Rad * moveDegree;
        Vector2 d = new Vector2(Mathf.Cos(radiant), Mathf.Sin(radiant));

        dir.localPosition = d * 0.3f;
        direction = dir.localPosition.normalized;
    }

    IEnumerator SlowDownSpeed()
    {
        if(!isHit)
        {
            currentSpeed = currentSpeed * moveSpeedPercentWhenHitted;
            isHit = true;
        }

        yield return new WaitForSeconds(0.5f);
        isHit = false;
        currentSpeed = currentSpeed / moveSpeedPercentWhenHitted;
    }

    private void Dead()
    {
        StopAllCoroutines();
        isDead = true;
        col.enabled = false;
        rotateTwin1.Kill();
        rotateTwin2.Kill();
        speed = 0f;
        if(isChild)
        {
            RemoveChild();
        }
        
        if(spawnBuffDebuff)
        {
            ConfigBuffDebuffRecord cf = ConfigurationManager.instance.buffDebuff.GetRandom();
            GameObject obj = Instantiate(Resources.Load(cf.Prefab) as GameObject, null);
            BuffDebuff buffDebuff = obj.GetComponent<BuffDebuff>();
            buffDebuff.Setup(cf);
            buffDebuff.transform.position = trans.position;
        }
        if(canDuplicate)
        {
            for (int i = 0; i < duplication.Length; i++)
            {
                EnemyInfor infor = duplication[i];

                EnemyControl enemy = EnemyFactory.instance.CreateEnemy(infor.prefab);
                enemy.gameObject.SetActive(false);
                enemy.Setup(infor, true, true);
                enemy.trans.position = trans.position;
                enemy.gameObject.SetActive(true);


            }
        }
        OnEnemyDeadParam param = new OnEnemyDeadParam();
        param.coinAmount = coinAmount;
        param.isCountDead = isCountOnDead;
        param.instance = this;
        //param.enemyType = inforEnemy.cf.EnemyTypeId;
        OnEnemyDead?.Invoke(param);
        model.gameObject.SetActive(false);
        deadAnim.gameObject.SetActive(true);
        Destroy(gameObject, 1f);


    }

    public void AddBuffDeBuffId()
    {

    }

    public void RemoveBuffDebuffId()
    {

    }

    
    // Calculate new Direction
    private void ChangeDir(float minDegree, float maxDegree)
    {
        moveDegree = UnityEngine.Random.Range(minDegree, maxDegree);
        float radiant = Mathf.Deg2Rad * moveDegree;
        Vector2 d = new Vector2(Mathf.Cos(radiant), Mathf.Sin(radiant));
        
        dir.localPosition = d * 0.3f;
        direction = dir.localPosition.normalized;
    }

}
