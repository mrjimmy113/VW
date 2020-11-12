using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDebuff_SlowPlayer : BuffDebuff
{
    public override void AfterEffect(EffectData data)
    {
        PlayerControl playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        playerControl.FireRate = playerControl.FireRate * cf.Value;
    }

    public override EffectData OnEffect()
    {
        PlayerControl playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        EffectData data = new EffectData();
        playerControl.CurrentSpeed = playerControl.CurrentSpeed / cf.Value;



        return data;
    }
}
