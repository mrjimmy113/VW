using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileControl_Gun02 : ProjectileControl
{
    [SerializeField]
    private float aliveTime = 2f;

    private float timeAlive = 0f;




    public override void OnUpdate()
    {
        timeAlive += Time.deltaTime;
        if(timeAlive >= aliveTime)
        {
            
            SpawnImpact();
            Collider2D[] cols = detect.DetectEnemy(trans.position);
            impact.OnImpact(cols);
            Despawn();
        }

    }

    public override void OnHitEnemy()
    {
        base.OnHitEnemy();
    }

    public override void Setup(ProjectileData data)
    {
        base.Setup(data);
        model.rotation = Quaternion.identity;
        timeAlive = 0f;
        model.Rotate(Vector3.forward, ((ProjectileMovement_Angle_Straight)movement).moveDegree);
    }
}
