using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConfigGunRofRecord
{
    [SerializeField]
    private int id = 0;
    [SerializeField]
    private int gunId = 0;
    [SerializeField]
    private int level = 0;
    [SerializeField]
    private int value = 0;
    [SerializeField]
    private int unlockFee = 0;

    public int Id { get => id; }
    public int GunId { get => gunId; }
    public int Level { get => level; }
    public int Value { get => value; }
    public int UnlockFee { get => unlockFee; }
}

public class ConfigGunRof : BYDataTable<ConfigGunRofRecord>
{
    public override void SetCompareObject()
    {
        recoreCompare = new ConfigCompare2Key<ConfigGunRofRecord, int, int>("gunId", "level");
    }
}
