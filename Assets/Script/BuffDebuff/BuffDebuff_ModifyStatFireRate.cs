using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

public class BuffDebuff_ModifyStatFireRate : BuffDebuff
{
    public override EffectData OnEffect()
    {
        PlayerControl playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        EffectData data = new EffectData();
        playerControl.FireRate = playerControl.FireRate * cf.Value;
        


        return data;
    }

    public override void AfterEffect(EffectData data)
    {
        PlayerControl playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        playerControl.FireRate = playerControl.FireRate / cf.Value;
    }


}
