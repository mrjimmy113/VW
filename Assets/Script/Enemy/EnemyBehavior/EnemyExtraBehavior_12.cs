using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExtraBehavior_12 : EnemyExtraBehavior
{
    [SerializeField]
    private float checkTime = 0.1f;

    [SerializeField]
    private float checkRadius = 0.5f;

    [SerializeField]
    private float percentPerLeech = 0.02f;

    [SerializeField]
    private float timeBetweenLeech = 0.5f;




    private string leecherTag = "Leecher";

    private LineRenderer lineRenderer;
    private Transform leechTarget;

    public override void Setup()
    {
        lineRenderer = GetComponent<LineRenderer>();
        StartCoroutine(StartCheck());
    }

    IEnumerator StartCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(checkTime);
        while(true)
        {
            Collider2D col = Physics2D.OverlapCircle(transform.position, checkRadius
                , LayerConfig.ENEMY_LAYER
                );
            if(col != null && !col.CompareTag(leecherTag))
            {
                
                lineRenderer.positionCount = 2;
                control.SetChild();
                leechTarget = col.transform;
                transform.SetParent(leechTarget);
                StartCoroutine(LeechVFX());
                StartCoroutine(Leeching());
                col.GetComponent<EnemyControl>().OnEnemyDead += (p) =>
                {
                    StopCoroutine(LeechVFX());
                    StopCoroutine(Leeching());
                    leechTarget = null;
                    control.RemoveChild();
                    lineRenderer.positionCount = 0;
                    StartCoroutine(StartCheck());
                    
                };
                StopCoroutine(StartCheck());
            }
            

            yield return wait;
        }
    }

    IEnumerator LeechVFX()
    {
        WaitForSeconds wait = new WaitForSeconds(0.02f);
        while(true)
        {
            if (leechTarget == null) break;
            List<Vector3> vector3s = new List<Vector3>();
            vector3s.Add(transform.position);
            vector3s.Add(leechTarget.position);
            lineRenderer.SetPositions(vector3s.ToArray());

            yield return wait;
        }
    }

    IEnumerator Leeching()
    {
        WaitForSeconds wait = new WaitForSeconds(timeBetweenLeech);
        EnemyControl enemyControl = leechTarget.GetComponent<EnemyControl>();
        int hpPerLeech = Mathf.RoundToInt(Mathf.Ceil(enemyControl.inforEnemy.hp * percentPerLeech));
        while (true)
        {
            if (leechTarget == null) break;
            enemyControl.ReduceHP(hpPerLeech);
            control.Heal(hpPerLeech);
            yield return wait;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, checkRadius * transform.localScale.y);
    }
}
