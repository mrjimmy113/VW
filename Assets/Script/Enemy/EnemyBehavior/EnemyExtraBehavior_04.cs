using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExtraBehavior_04 : EnemyExtraBehavior
{
    [Header("Enemy_04")]
    [SerializeField]
    private float modifySpeed = 10f;
    [SerializeField]
    private float slowCheckTime = 0.1f;
    [SerializeField]
    private float checkRadius = 0.5f;

    private LineRenderer lineRenderer;
    private bool isSlow;
    private PlayerControl playerControl;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }


    public override void Setup()
    {
        StartCoroutine(CheckSlow());
        StartCoroutine(SlowVFX());
        control.OnEnemyDead += OnDead;
    }

    private void OnDead(int obj)
    {
        lineRenderer.positionCount = 0;
        StopAllCoroutines();
    }

    IEnumerator CheckSlow()
    {
        WaitForSeconds wait = new WaitForSeconds(slowCheckTime);
        while (true)
        {
            yield return wait;
            Collider2D col = Physics2D.
                OverlapCircle(transform.position, checkRadius * transform.localScale.y, LayerConfig.PLAYER_LAYER);
            if(col != null)
            {
                isSlow = true;
                
                float dotP = Vector2.Dot(Vector2.up, (col.transform.position - transform.position));
                
                if(dotP < 0)
                {
                    playerControl.Slow(modifySpeed);
                }
            }else
            {
                isSlow = false;
            }
            
        }
    }

    IEnumerator SlowVFX()
    {
        WaitForSeconds updateTime = new WaitForSeconds(0.02f);
        while(true)
        {
            yield return updateTime;
            if(isSlow)
            {
                lineRenderer.positionCount = 2;
                List<Vector3> vector3s = new List<Vector3>();
                vector3s.Add(transform.position);
                vector3s.Add(playerControl.transform.position);
                lineRenderer.SetPositions(vector3s.ToArray());
            }else
            {
                lineRenderer.positionCount = 0;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, checkRadius * transform.localScale.y);
    }
}
