using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExtraBehavior_05 : EnemyExtraBehavior
{
    [SerializeField]
    private float checkTime = 0.1f;
    [SerializeField]
    private float checkRadius = 0.5f;
    

    public override void Setup()
    {
        StartCoroutine(StartCheck());
        control.OnEnemyDead += OnDead;
    }

    private void OnDead(int obj)
    {
        StopAllCoroutines();
    }

    IEnumerator StartCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(checkTime);
        while(true)
        {
            yield return wait;
            Collider2D col = Physics2D.OverlapCircle(transform.position,
                checkRadius * transform.localScale.y,
                LayerConfig.PROJECTILE_LAYER
                );

            if(col != null)
            {
                control.OnPush(col.transform.position);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, checkRadius * transform.localScale.y);
    }
}
