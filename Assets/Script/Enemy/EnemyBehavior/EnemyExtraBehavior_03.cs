using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExtraBehavior_03 : EnemyExtraBehavior
{
    [Header("Enemy_03")]

    private string pushTag = "Pusher";
    [SerializeField]
    private float pushCheckTime = 0.1f;
    [SerializeField]
    private float checkRadius = 0.5f;

    public override void Setup()
    {
        StartCoroutine(CheckPush());
    }

    IEnumerator CheckPush()
    {
        WaitForSeconds wait = new WaitForSeconds(pushCheckTime);
        while(true)
        {
            yield return wait;
            Collider2D[] cols = Physics2D.
                OverlapCircleAll(transform.position, checkRadius * transform.localScale.y, LayerConfig.ENEMY_LAYER);
            foreach(var c in cols)
            {
                if(c.tag != pushTag)
                {
                    EnemyControl control = c.GetComponent<EnemyControl>();
                    control.OnPush(transform.position);
                    this.control.OnPush(control.transform.position);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, checkRadius * transform.localScale.y);
    }
}
