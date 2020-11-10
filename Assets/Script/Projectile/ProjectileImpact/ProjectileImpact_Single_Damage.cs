using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileImpact_Single_Damage : ProjectileImpact
{
    public override void OnImpact(Collider2D[] cols)
    {
        foreach (var c in cols)
        {
            c.GetComponent<EnemyControl>().OnDamage(control.data.damage);
            
        }
        
    }
}
