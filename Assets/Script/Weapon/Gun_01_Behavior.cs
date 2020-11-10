using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_01_Behavior : WeaponBehavior
{
    public event Action<bool> OnFireEvent;


    public override void OnFire()
    {
        Transform pj = PoolManager.instance.dicPool[projectile.name].Spwan();
        bool isLeft = false;
        pj.position = GetMuzzle(out isLeft).position;
        ProjectileData data = new ProjectileData();
        data.damage = 1;
        data.projectilePoolName = projectile.name;
        data.impactPoolName = impact.name;
        data.isRight = !isLeft;
        pj.GetComponent<ProjectileControl>().Setup(data);
        OnFireEvent?.Invoke(isLeft);
    }
}
