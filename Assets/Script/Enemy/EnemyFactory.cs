using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnNewEnemyAddParam
{
    public EnemyControl control;
}


public class EnemyFactory : Singleton<EnemyFactory>
{
    public List<EnemyControl> activeEnemy = new List<EnemyControl>();
    public event OnNewEnemyAddEvent OnNewEnemyAddEvent;

    public EnemyControl CreateEnemy(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab, null);
        EnemyControl result = obj.GetComponent<EnemyControl>();
        result.OnEnemyDead += OnEnemyDead;
        activeEnemy.Add(result);
        OnNewEnemyAddParam param = new OnNewEnemyAddParam();
        param.control = result;
        OnNewEnemyAddEvent?.Invoke(param);
        return result;
    }

    public void RemoveEnemy(EnemyControl instance)
    {
        activeEnemy.Remove(instance);
    }


    private void OnEnemyDead(OnEnemyDeadParam param)
    {
        
        activeEnemy.Remove(param.instance);
    }
}
