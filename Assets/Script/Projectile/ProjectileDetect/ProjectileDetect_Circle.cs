using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDetect_Circle : ProjectileDetect
{
    [SerializeField]
    private float radius = 0.5f;

    public override Collider2D[] DetectEnemy(Vector3 pos)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, radius,enemyLayerMask);
        return colliders;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
