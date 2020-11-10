using UnityEngine;
using System.Linq;
using System;

[Serializable]
public class ConfigBuffDebuffRecord
{
    [SerializeField]
    private int id;
    [SerializeField]
    private string prefab;
    [SerializeField]
    private string sprite;
    [SerializeField]
    private int value;
    [SerializeField]
    private int isBuff;
    [SerializeField]
    private string fieldName;
    [SerializeField]
    private float effectTime;

    public int Id { get => id;}
    public string Prefab { get => prefab;}
    public string Sprite { get => sprite;}
    public int Value { get => value;}
    public bool IsBuff { get => isBuff == 1? true : false;}
    public string FieldName { get => fieldName;}
    public float EffectTime { get => effectTime;}
}

public class ConfigBuffDebuff :BYDataTable<ConfigBuffDebuffRecord> 
{
    public override void SetCompareObject()
    {
        recoreCompare = new ConfigCompareKey<ConfigBuffDebuffRecord>("id");
    }

    public ConfigBuffDebuffRecord GetRandom()
    {
        return records.OrderBy(r => Guid.NewGuid()).First();
    }
}


