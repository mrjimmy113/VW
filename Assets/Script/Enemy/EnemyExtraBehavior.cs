using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExtraBehavior : MonoBehaviour
{
    protected EnemyControl control;

    private void OnEnable()
    {
        control = GetComponent<EnemyControl>();
        control.OnEnemyDead += OnDead;
    }

    public virtual void OnDead(OnEnemyDeadParam param)
    {
        StopAllCoroutines();
    }

    public virtual void Setup()
    {

    }
}
