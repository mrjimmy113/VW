using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDebuff_DropGold : BuffDebuff
{
    public override EffectData OnEffect()
    {
        PlayerControl playerControl = BuffDebuffControl.instance.playerControl;
        EffectData data = new EffectData();
        playerControl.IsDropGold = true;



        return data;
    }

    public override void AfterEffect(EffectData data)
    {
        PlayerControl playerControl = BuffDebuffControl.instance.playerControl;
        playerControl.IsDropGold = false;
    }
}