using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ConfigPlayerCoinValueRecord
{
    [SerializeField]
    private int id = 0;
    [SerializeField]
    private int level = 0;
    [SerializeField]
    private int value = 0;
    [SerializeField]
    private int unlockFee = 0;

    public int Id { get => id; }
    public int Level { get => level; }
    public int Value { get => value; }
    public int UnlockFee { get => unlockFee; }
}

public class ConfigPlayerCoinValue : BYDataTable<ConfigPlayerCoinValueRecord>
{
    public override void SetCompareObject()
    {
        recoreCompare = new ConfigCompareKey<ConfigPlayerCoinValueRecord>("level");
    }
}
