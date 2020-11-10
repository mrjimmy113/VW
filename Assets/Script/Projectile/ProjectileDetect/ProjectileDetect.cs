using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileDetect :MonoBehaviour
{
    protected int enemyLayerMask = 1 << 8;


    public abstract Collider2D[] DetectEnemy(Vector3 pos);
    
}
