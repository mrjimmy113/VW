using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class BuffDebuffControl : MonoBehaviour
{
    private List<BuffDebuffData> buffDebuffDatas = new List<BuffDebuffData>();
    public event Action<string, int, float> AddBuffDebuffEvent;
    public event Action<int, float> BuffDebuffProgressEvent;
    public event Action<int> RemoveBuffDebuffEvent;

    private PlayerControl playerControl;

    private void Start()
    {
        MissionManager.instance.OnMissionStart += MissionStart;
        playerControl = GetComponent<PlayerControl>();
    }

    private void MissionStart()
    {
        StartCoroutine(CheckBuffDebuff());
    }

    IEnumerator CheckBuffDebuff()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.2f);
        while (true)
        {
            List<BuffDebuffData> removeList = new List<BuffDebuffData>();
            foreach (var d in buffDebuffDatas)
            {
                d.remainTime -= 0.2f;
                BuffDebuffProgressEvent?.Invoke(d.cf.Id, d.remainTime);
                if (d.remainTime <= 0)
                {
                    removeList.Add(d);
                }
            }

            foreach (var d in removeList)
            {
                buffDebuffDatas.Remove(d);
/*                Type type = playerControl.GetType();
                PropertyInfo prop = type.GetProperty(d.cf.FieldName);
                prop.SetValue(playerControl, d.oldValue, null);*/
                RemoveBuffDebuffEvent?.Invoke(d.cf.Id);
            }

            yield return waitForSeconds;
        }
    }

    

    public void ApplyBuffDebuff(ConfigBuffDebuffRecord cf)
    {
        foreach (var d in buffDebuffDatas)
        {
            if (d.cf.Id == cf.Id)
            {
                d.remainTime = cf.EffectTime;
                AddBuffDebuffEvent?.Invoke(cf.Sprite, cf.Id, cf.EffectTime);
                return;
            }
        }

/*        Type type = playerControl.GetType();
        PropertyInfo prop = type.GetProperty(cf.FieldName);
        int oldValue = (int)prop.GetValue(playerControl);

        if (cf.IsBuff) prop.SetValue(playerControl, ((int)prop.GetValue(playerControl)) * cf.Value, null);
        else prop.SetValue(playerControl, ((int)prop.GetValue(playerControl)) / cf.Value, null);*/

        buffDebuffDatas.Add(new BuffDebuffData(cf));
        AddBuffDebuffEvent?.Invoke(cf.Sprite, cf.Id, cf.EffectTime);
    }
}

public class BuffDebuffData
{
    public ConfigBuffDebuffRecord cf;
    public float remainTime;

    public BuffDebuffData(ConfigBuffDebuffRecord cf)
    {
        this.cf = cf;
        remainTime = cf.EffectTime;
    }
}
