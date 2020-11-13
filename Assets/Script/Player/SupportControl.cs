using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportControl : MonoBehaviour
{
    private PlayerControl playerControl = null;
    [SerializeField]
    private Transform projectile = null;
    [SerializeField]
    private Transform impact = null;

    [SerializeField]
    private float distanceToPlayer = 1f;
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float spaceBetweenProjectile = 0.1f;
    [SerializeField]
    private Transform muzzle = null;
    [SerializeField]
    private Transform forwardPoint = null;

    private void Awake()
    {
        playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();

    }

    private void Update()
    {
        if(Vector2.Distance(transform.position,playerControl.transform.position) > distanceToPlayer)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                playerControl.transform.position,
                Time.deltaTime * speed

                ) ;
        }
     
    }

    private Vector3[] GenerateToPos()
    {
        Vector3[] result = new Vector3[playerControl.FireRate];
        float space = (spaceBetweenProjectile * playerControl.FireRate) / 2;
        Vector3 startPoint = forwardPoint.position;
        startPoint.x -= space;
        result[0] = startPoint;
        for (int i = 1; i < playerControl.FireRate; i++)
        {
            Vector3 newPoint = result[i - 1];
            newPoint.x += spaceBetweenProjectile;
            result[i] = newPoint;
        }

        return result;
    }

    public void Fire()
    {
        Vector3[] points = GenerateToPos();

        foreach (var p in points)
        {
            Transform pj = PoolManager.instance.dicPool[projectile.name].Spwan();
            pj.position = muzzle.position;
            ProjectileData data = playerControl.GetProjectileData();
            data.toPos = p;
            pj.GetComponent<ProjectileControl>().Setup(data);
        }
    }

    public void Summon()
    {
        this.enabled = true;
        playerControl.OnFire += Fire;
    }

    public void BeGone()
    {
        playerControl.OnFire -= Fire;
        this.enabled = false;
        transform.DOMoveY(-6,1f);
    }
}
