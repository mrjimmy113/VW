using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExtraBehavior_10 : EnemyExtraBehavior
{
    [SerializeField]
    private float maxExtraSize = 5;
    [SerializeField]
    private float minExtraSize = 0;
    [SerializeField]
    private float timeToChange = 2f;

    public override void Setup()
    {
        StartCoroutine(ChangeSize());
    }

    IEnumerator ChangeSize()
    {
        WaitForSeconds wait = new WaitForSeconds(timeToChange);
        while (true)
        {
            yield return wait;
            control.currentSpeed = control.speed + UnityEngine.Random.Range(minExtraSize, maxExtraSize);
        }
    }
}
