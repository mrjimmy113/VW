using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileData
{
    public int damage;
    public string projectilePoolName;
    public string impactPoolName;
    public Vector3 toPos;
    public float toPosTime;
    public bool isRight;
    public Sprite projectileSprite;
    public ProjectileExtraBehavior[] extraBehaviors;
}

public delegate void ProjectileExtraBehavior(Collider2D col);


public class ProjectileControl : MonoBehaviour
{
    protected ProjectileMovement movement = null;
    protected ProjectileDetect detect = null;
    protected ProjectileImpact impact = null;
    protected Transform trans;

    [SerializeField]
    protected float detectTime = 0.1f;

    public ProjectileData data;
    public Transform model;
    private SpriteRenderer rd;

    private void Awake()
    {
        if (movement == null) movement = GetComponent<ProjectileMovement>();
        if (detect == null) detect = GetComponent<ProjectileDetect>();
        if (impact == null) impact = GetComponent<ProjectileImpact>();

        trans = transform;
        rd = model.GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        StartCoroutine(DetectAndImpact());
    }


    private void Update()
    {
        movement.Move();
        if(!Boudary.instance.IsInScreen(transform.position))
        {
            Despawn();
        }
        OnUpdate();
    }

    public virtual void OnUpdate()
    {

    }

    IEnumerator DetectAndImpact()
    {
        WaitForSeconds wait = new WaitForSeconds(detectTime);

        while(true)
        {
            yield return wait;
            Collider2D[] cols = detect.DetectEnemy(trans.position);
            impact.OnImpact(cols);
            if (cols.Length > 0)
            {
                foreach(var c in cols)
                {
                    foreach(var b in data.extraBehaviors)
                    {
                        b.Invoke(c);
                    }
                }
                OnHitEnemy();
            }

        }
    }

    public virtual void OnHitEnemy()
    {
        SpawnImpact();
        Despawn();
    }

    public virtual void Setup(ProjectileData data)
    {
        this.data = data;
        if (data.projectileSprite != null) rd.sprite = data.projectileSprite;
        movement.Setup();
    }

    public void  Despawn()
    {
        StopAllCoroutines();
        PoolManager.instance.dicPool[data.projectilePoolName].Despwan(transform);
    }

    public void SpawnImpact()
    {
        Transform impact =  PoolManager.instance.dicPool[data.impactPoolName].Spwan();
        impact.position = trans.position;
        impact.GetComponent<ImpactControl>().Setup(data.impactPoolName);
        
    }


}
