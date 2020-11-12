using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            if (c == null) continue;
            c.RemoveChild();
        }
        base.OnDead(param);
    }

    IEnumerator StartCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(checkTime);
        ContactFilter2D contactFilter2D = new ContactFilter2D();
        contactFilter2D.useLayerMask = true;
        contactFilter2D.SetLayerMask(LayerConfig.ENEMY_LAYER);
        Collider2D[] cols = new Collider2D[10];
        while (true)
        {
            
            int length = control.col.OverlapCollider(contactFilter2D, cols);
            if ( length > 0)
            {
                for(int i = 0; i < length;i++ )
                {
                    Collider2D c = cols[i];
                    EnemyControl enemyControl = c.GetComponent<EnemyControl>();
                    if (c.gameObject != gameObject && !enemyControl.isChild)
                    {
                        
                        enemyControl.transform.SetParent(transform);
                        enemyControl.SetChild();
                        child.Add(enemyControl);


                    }
                }
            }

            foreach(var ch in child.ToList())
            {
                if (ch == null) continue;
                int l = ch.col.OverlapCollider(contactFilter2D, cols);
                if(l > 0)
                {
                    for (int i = 0; i < length; i++)
                    {
                        Collider2D c = cols[i];
                        EnemyControl enemyControl = c.GetComponent<EnemyControl>();
                        if (c.gameObject != gameObject && !enemyControl.isChild)
                        {

                            enemyControl.transform.SetParent(transform);
                            enemyControl.SetChild();
                            child.Add(enemyControl);


                        }
                    }
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
