using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExtraBehavior_08 : EnemyExtraBehavior
{
    [SerializeField]
    private float checkTime = 0.1f;
    [SerializeField]
    private float checkRadius = 0.5f;

    private List<EnemyControl> child = new List<EnemyControl>();

    public override void Setup()
    {
        StartCoroutine(StartCheck());
    }

    public override void OnDead(OnEnemyDeadParam param)
    {
        foreach (var c in child)
        {
            c.RemoveChild();
        }
        base.OnDead(param);
    }

    IEnumerator StartCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(checkTime);
        while(true)
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(
                transform.position, checkRadius * transform.localScale.y,
                LayerConfig.ENEMY_LAYER
                );
            foreach(var c in cols)
            {
                if(c.gameObject != gameObject)
                {
                    EnemyControl enemyControl = c.GetComponent<EnemyControl>();
                    enemyControl.transform.SetParent(transform);
                    enemyControl.SetChild();
                    child.Add(enemyControl);
                    
                    
                }
            }

            yield return wait;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, checkRadius * transform.localScale.y);
    }
}
