using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBehavior : MonoBehaviour
{
    public int id;
    public float rof;
    public float damage;
    public Transform projectile;
    public Transform impact;
    public Transform muzzleRight;
    public Transform muzzleLeft;
    private bool isRightMuzzle = false;
    public float extraSize = 1;

    private void OnEnable()
    {
        Pool projectilePool = new Pool(projectile.name, 10, projectile);
        Pool impactPool = new Pool(impact.name, 10, impact);

        PoolManager.instance.AddNewPool(projectilePool);
        PoolManager.instance.AddNewPool(impactPool);
    }


    public abstract void OnFire();

    public virtual void Setup(ConfigGunRecord record)
    {
        id = record.Id;
    }

    public virtual void SetupStart(ConfigGunDamageRecord configDmg, ConfigGunRofRecord configRof) 
    {
        damage = configDmg.Value;
        rof = configRof.Value;
        
    }

    protected Transform GetMuzzle(out bool isLeft)
    {
        isRightMuzzle = !isRightMuzzle;
        if (isRightMuzzle)
        {
            isLeft = false;
            return muzzleRight;
            
        }
        else
        {
            isLeft = true;
            return muzzleLeft;
        }

    }
}
