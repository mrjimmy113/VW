using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDebuff_SlowEnemy : BuffDebuff
{
    public override void AfterEffect(EffectData data)
    {
        List<EnemyControl> enemies = EnemyFactory.instance.activeEnemy;
        foreach(var e in enemies)
        {
            if(e.appliedBuffDebuffId.Contains(cf.Id))
            {
                e.appliedBuffDebuffId.Remove(cf.Id);
                e.currentSpeed = e.currentSpeed * cf.Value;
            }
        }
        EnemyFactory.instance.OnNewEnemyAddEvent -= OnNewEnemyAddEvent;
    }

    public override EffectData OnEffect()
    {
        EffectData data = new EffectData();
        
        List<EnemyControl> enemies = EnemyFactory.instance.activeEnemy;
        foreach (var e in enemies)
        {
            if (!e.appliedBuffDebuffId.Contains(cf.Id))
            {
                e.appliedBuffDebuffId.Add(cf.Id);
                e.currentSpeed = e.currentSpeed / cf.Value;
            }
        }
        EnemyFactory.instance.OnNewEnemyAddEvent += OnNewEnemyAddEvent;
        ;
        return data;
    }

    private void OnNewEnemyAddEvent(OnNewEnemyAddParam param)
    {
        
        if (!param.control.appliedBuffDebuffId.Contains(cf.Id))
        {
            param.control.appliedBuffDebuffId.Add(cf.Id);
            param.control.currentSpeed = param.control.currentSpeed / cf.Value;
        }
    }

    
}