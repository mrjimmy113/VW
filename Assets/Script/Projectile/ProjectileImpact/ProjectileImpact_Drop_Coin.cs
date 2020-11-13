using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileImpact_Drop_Coin : ProjectileImpact
{
    public override void OnImpact(Collider2D[] cols)
    {
        foreach (var c in cols)
        {
            MissionManager.instance.AddGoldEanred(1);

        }
    }
}
