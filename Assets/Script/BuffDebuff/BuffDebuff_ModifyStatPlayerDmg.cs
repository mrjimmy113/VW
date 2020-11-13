using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class BuffDebuff_ModifyStatPlayerDmg : BuffDebuff
{



    public override EffectData OnEffect()
    {
        PlayerControl playerControl = BuffDebuffControl.instance.playerControl;
        EffectData data = new EffectData();
        playerControl.Damage = playerControl.Damage * cf.Value;



        return data;
    }

    public override void AfterEffect(EffectData data)
    {
        PlayerControl playerControl = BuffDebuffControl.instance.playerControl;
        playerControl.Damage = playerControl.Damage / cf.Value;
    }

}
