using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDetect_Forward : ProjectileDetect
{
    public override Collider2D[] DetectEnemy(Vector3 pos)
    {
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.up, 0.1f, enemyLayerMask);
        return MyUltisGeneric<Collider2D>.SingleToArray(hit.collider);
    }
}
