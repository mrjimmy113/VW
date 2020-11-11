using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExtraBehavior_12 : EnemyExtraBehavior
{
    [SerializeField]
    private float checkTime = 0.1f;

    [SerializeField]
    private float checkRadius = 0.5f;

    private string leecherTag = "Leecher";

    private LineRenderer lineRenderer;
    private Transform leechTarget;

    public override void Setup()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    IEnumerator StartCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(checkTime);
        while(true)
        {
            Collider2D col = Physics2D.OverlapCircle(transform.position, checkRadius
                , LayerConfig.ENEMY_LAYER
                );
            if(col != null && col.tag != leecherTag)
            {
                lineRenderer.positionCount = 2;
                StartCoroutine(LeechVFX());
                control.SetChild();
                leechTarget = col.transform;
                col.GetComponent<EnemyControl>().OnEnemyDead += (p) =>
                {
                    control.RemoveChild();
                    lineRenderer.positionCount = 0;
                    StopCoroutine(LeechVFX());
                    leechTarget = null;
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
            List<Vector3> vector3s = new List<Vector3>();
            vector3s.Add(transform.position);
            vector3s.Add(leechTarget.position);
            lineRenderer.SetPositions(vector3s.ToArray());

            yield return wait;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, checkRadius * transform.localScale.y);
    }
}
