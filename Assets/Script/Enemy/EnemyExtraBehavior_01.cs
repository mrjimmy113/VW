using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExtraBehavior_01 : EnemyExtraBehavior
{
    [Header("Enemy_01")]

    [SerializeField]
    private float maxExtraSpeed = 5;
    [SerializeField]
    private float minExtraSpeed = 0;
    [SerializeField]
    private float timeToChange = 2f;

    public override void Setup()
    {
        StartCoroutine(ChangeSpeed());
    }

    IEnumerator ChangeSpeed()
    {
        WaitForSeconds wait = new WaitForSeconds(timeToChange);
        while (true)
        {
            yield return wait;
            control.currentSpeed = control.speed + UnityEngine.Random.Range(minExtraSpeed, maxExtraSpeed);
        }
    }
}
