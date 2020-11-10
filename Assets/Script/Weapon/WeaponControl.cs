using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    private WeaponBehavior currentWeapon;
    private float timeAttack;


    private void Start()
    {
        currentWeapon = GetComponentInChildren<WeaponBehavior>();
        timeAttack = currentWeapon.rof;

    }

    

    private void Update()
    {
        timeAttack += Time.deltaTime;
        if (timeAttack >= currentWeapon.rof)
        {
            currentWeapon.OnFire();
            timeAttack = 0;
        }
    }


}
