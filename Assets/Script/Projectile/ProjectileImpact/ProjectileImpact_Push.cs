using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileImpact_Push : ProjectileImpact
{
    public override void OnImpact(Collider2D[] cols)
    {
        foreach (var c in cols)
        {
            c.GetComponent<EnemyControl>().OnPush(transform.position);
        }
    }
}
