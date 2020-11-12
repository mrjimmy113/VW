
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExtraBehavior_15 : EnemyExtraBehavior
{
    [SerializeField]
    private float healSpeed = 0.1f;
    [SerializeField]
    private float healTime = 10f;

    private int healAmount;


    private Coroutine startHealing;


    public override void Setup()
    {
        control.OnEnemyDamaged += OnEnemyDamaged;
        healAmount = Mathf.RoundToInt(Mathf.Ceil(control.inforEnemy.hp * healSpeed / healTime));
    }

    private void OnEnemyDamaged(OnEnemyDamagedParam obj)
    {
        if (startHealing != null)
        {
            StopCoroutine(startHealing);
            startHealing = null;
        }
        startHealing = StartCoroutine(StartHealing());
    }

    IEnumerator StartHealing()
    {
        WaitForSeconds wait = new WaitForSeconds(healSpeed);
        yield return new WaitForSeconds(1f);
        while(true)
        {
            control.Heal(healAmount);
            if(control.hp >= control.inforEnemy.hp)
            {
                StopCoroutine(StartHealing());
                break;
            }
            yield return wait;
            
        }
    }
}
