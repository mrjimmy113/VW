using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement_Straight : ProjectileMovement
{
    public override void Move()
    {
        trans.position += Vector3.up * Time.unscaledDeltaTime * speed;        
    }

    public override void Setup()
    {
        
    }
}
