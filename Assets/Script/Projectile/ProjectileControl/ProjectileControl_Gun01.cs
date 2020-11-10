using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileControl_Gun01 : ProjectileControl
{
    [SerializeField]
    private int numberOfHit = 5;

    private int currentHit = 0;

    public override void Setup(ProjectileData data)
    {
        base.Setup(data);
        currentHit = 0;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnHitEnemy()
    {
        currentHit++;
        if(currentHit >= numberOfHit)
        {
            Despawn();
        }
    }
}
