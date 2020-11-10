using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement_Angle_Straight : ProjectileMovement
{
    private Vector3 direction;
    [SerializeField]
    private int minDegree = 70;
    [SerializeField]
    private int maxDegree = 110;

    public float moveDegree;

    public override void Move()
    {
        trans.position += direction * Time.unscaledDeltaTime * speed;
    }

    private void ChangeDir(float minDegree, float maxDegree)
    {
        moveDegree = UnityEngine.Random.Range(minDegree, maxDegree);
        float radiant = Mathf.Deg2Rad * moveDegree;
        Vector2 d = new Vector2(Mathf.Cos(radiant), Mathf.Sin(radiant));
        direction = d.normalized;
    }

    public override void Setup()
    {
        if(control.data.isRight)
        {
            ChangeDir(minDegree, 90);
        }
        else
        {
            ChangeDir(90, maxDegree);
        }
    }
}
