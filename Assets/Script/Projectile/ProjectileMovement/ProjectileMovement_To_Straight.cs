using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement_To_Straight : ProjectileMovement
{
    public bool isReach = false;


  


    public override void Move()
    {
        
        if(isReach)
        {
            trans.position += Vector3.up * Time.unscaledDeltaTime * speed;
        }
    }

    public IEnumerator MoveToPosition(Vector3 position, float timeToMove)
    {
        var currentPos = trans.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.unscaledDeltaTime / timeToMove;
            trans.position = Vector3.Lerp(currentPos, position, t);
            if (Vector3.Distance(trans.position, control.data.toPos) <= 0.01f)
            {
                isReach = true;
                break;
            }
            yield return null;
        }
    }

    public override void Setup()
    {
        isReach = false;
        StartCoroutine(MoveToPosition(control.data.toPos, control.data.toPosTime));
    }
}
