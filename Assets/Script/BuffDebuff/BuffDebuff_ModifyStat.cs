using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

public class BuffDebuff_ModifyStat : BuffDebuff
{
   


    public override void OnEffect(Transform transform)
    {
        PlayerControl target =  transform.GetComponent<PlayerControl>();
        target.ApplyBuffDebuff(cf);
        
    }


}
