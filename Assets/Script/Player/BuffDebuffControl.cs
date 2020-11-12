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
    private PlayerProjectileControl playerProjectileControl;

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
                d.AfterEffect(d.data);
                RemoveBuffDebuffEvent?.Invoke(d.cf.Id);
            }

            yield return waitForSeconds;
        }
    }

    

    public void ApplyBuffDebuff(ConfigBuffDebuffRecord cf,OnEffect OnEffect, AfterEffect AfterEffect)
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



        buffDebuffDatas.Add(new BuffDebuffData(cf,AfterEffect, OnEffect.Invoke() ));
        AddBuffDebuffEvent?.Invoke(cf.Sprite, cf.Id, cf.EffectTime);
    }

    public void ChangeProjectile()
    {
        List<int> buffIds = new List<int>();
        foreach(var bd in buffDebuffDatas)
        {
            buffIds.Add(bd.cf.Id);
        }

        playerControl.currentProjectile = 
            playerProjectileControl.GetCurrentProjectileSprite(buffIds);
    }
}

public class BuffDebuffData
{
    public ConfigBuffDebuffRecord cf;
    public float remainTime;
    public AfterEffect AfterEffect;
    public EffectData data;

    public BuffDebuffData(ConfigBuffDebuffRecord cf, AfterEffect AfterEffect,EffectData data)
    {
        this.AfterEffect = AfterEffect;
        this.cf = cf;
        this.data = data;
        remainTime = cf.EffectTime;

    }

    
}

public delegate EffectData OnEffect();
public delegate void AfterEffect(EffectData data);

public class EffectData
{
    public int oldIntData;
}


