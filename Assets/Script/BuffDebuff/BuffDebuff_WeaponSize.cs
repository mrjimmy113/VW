using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDebuff_WeaponSize : BuffDebuff
{
    public override EffectData OnEffect()
    {
        PlayerControl playerControl = BuffDebuffControl.instance.playerControl;
        EffectData data = new EffectData();
        playerControl.AddExtraWeaponSize(cf.Value);



        return data;
    }

    public override void AfterEffect(EffectData data)
    {
        PlayerControl playerControl = BuffDebuffControl.instance.playerControl;
        playerControl.AddExtraWeaponSize(-cf.Value);
    }
}
