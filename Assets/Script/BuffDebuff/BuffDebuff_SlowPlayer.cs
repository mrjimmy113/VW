using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDebuff_SlowPlayer : BuffDebuff
{
    public override void AfterEffect(EffectData data)
    {
        PlayerControl playerControl = BuffDebuffControl.instance.playerControl;
        playerControl.CurrentSpeed = playerControl.CurrentSpeed * cf.Value;
    }

    public override EffectData OnEffect()
    {
        PlayerControl playerControl = BuffDebuffControl.instance.playerControl;
        EffectData data = new EffectData();
        playerControl.CurrentSpeed = playerControl.CurrentSpeed / cf.Value;



        return data;
    }
}
