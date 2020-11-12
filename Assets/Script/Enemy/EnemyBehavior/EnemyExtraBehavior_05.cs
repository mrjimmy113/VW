using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExtraBehavior_05 : EnemyExtraBehavior
{
    [SerializeField]
    private float checkTime = 0.1f;
    [SerializeField]
    private float checkRadius = 0.5f;
    [SerializeField]
    private float timeBetweenDodge = 0.5f;
    [SerializeField]
    private float extraSpeedDodge = 1.5f;
    

    public override void Setup()
    {
        StartCoroutine(StartCheck());
        
    }


    IEnumerator StartCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(checkTime);
        WaitForSeconds dodgeWait = new WaitForSeconds(timeBetweenDodge);
        while(true)
        {
            
            Collider2D col = Physics2D.OverlapCircle(transform.position,
                checkRadius * transform.localScale.y,
                LayerConfig.PROJECTILE_LAYER
                );

            if(col != null)
            {
                Vector3 collisionDir = (transform.position - col.transform.position).normalized;
                
                float angle = Vector2.Angle(Vector2.right, collisionDir );
                
                if (angle >= 0 && angle <= 180)
                {
                    angle = 360 - angle;
                }
                float radiant = Mathf.Deg2Rad * angle;
                Vector2 d = new Vector2(Mathf.Cos(radiant), Mathf.Sin(radiant));
                control.moveDegree = angle;
                control.dir.localPosition = d * 0.3f;
                control.direction = d;
                control.currentSpeed = control.currentSpeed * extraSpeedDodge;
                yield return dodgeWait;
                control.currentSpeed = control.currentSpeed / extraSpeedDodge;
                continue;
            }

            yield return wait;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, checkRadius * transform.localScale.y);
        
    }
}
