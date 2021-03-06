﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExtraBehavior_11 : EnemyExtraBehavior
{
    [SerializeField]
    private float coolDown = 3f;
    [SerializeField]
    private float hpFactor = 10f;

    [SerializeField]
    private Transform prefab;

    public override void Setup()
    {
        StartCoroutine(StartCheck());
        prefab.localScale = Vector3.one;
    }

    IEnumerator StartCheck()
    {
        
        WaitForSeconds cd = new WaitForSeconds(coolDown);
        EnemyInfor infor = new EnemyInfor();
        infor.hp = Mathf.RoundToInt(control.inforEnemy.hp / hpFactor);
        infor.duplicate = null;
        infor.isDuplicate = false;
        infor.spawnBuffDebuff = false;
        infor.sizeScale = 1;
        infor.coinAmount = 0;

        yield return cd;

        while(true)
        {
            EnemyControl e = EnemyFactory.instance.CreateEnemy(prefab.gameObject);
            e.transform.position = transform.position;
            e.Setup(infor, false, false);
            yield return cd;
            
        }
    }
}
