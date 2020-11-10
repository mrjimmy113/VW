using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExtraBehavior : MonoBehaviour
{
    protected EnemyControl control;

    private void OnEnable()
    {
        control = GetComponent<EnemyControl>();
    }

    public virtual void Setup()
    {

    }
}
