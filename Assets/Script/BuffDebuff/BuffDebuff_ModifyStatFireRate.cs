using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

public class BuffDebuff_ModifyStatFireRate : BuffDebuff
{
    public override EffectData OnEffect()
    {
        PlayerControl playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        Type type = playerControl.GetType();
        PropertyInfo prop = type.GetProperty(cf.FieldName);
        EffectData data = new EffectData();
        data.oldIntData = (int)prop.GetValue(playerControl);

        if (cf.IsBuff) prop.SetValue(playerControl, ((int)prop.GetValue(playerControl)) * cf.Value, null);
        else prop.SetValue(playerControl, ((int)prop.GetValue(playerControl)) / cf.Value, null);

        return data;
    }

    public override void AfterEffect(EffectData data)
    {
        PlayerControl playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();

        Type type = playerControl.GetType();
        PropertyInfo prop = type.GetProperty(cf.FieldName);
        prop.SetValue(playerControl, data.oldIntData, null);

    }


}
