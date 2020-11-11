using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExtraBehavior_09 : EnemyExtraBehavior
{
    [SerializeField]
    private float checkTime = 0.1f;
    [SerializeField]
    private float checkRadius = 0.5f;

    public override void Setup()
    {
        StartCoroutine(StartCheck());
    }

    IEnumerator StartCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(checkTime);
        while(true)
        {
            Collider2D col = Physics2D.OverlapCircle(transform.position, checkRadius
                , LayerConfig.PLAYER_LAYER
                );
            if(col != null)
            {
                control.OnFollow(col.transform.position);
            }


            yield return wait;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, checkRadius * transform.localScale.y);
    }
}
