using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDebuff_CallSupport : BuffDebuff
{
    public override EffectData OnEffect()
    {
        
        EffectData data = new EffectData();
        SupportControl support = GameObject.FindGameObjectWithTag("Support").GetComponent<SupportControl>();
        support.Summon();



        return data;
    }

    public override void AfterEffect(EffectData data)
    {
        SupportControl support = GameObject.FindGameObjectWithTag("Support").GetComponent<SupportControl>();
        support.BeGone();
    }
}
