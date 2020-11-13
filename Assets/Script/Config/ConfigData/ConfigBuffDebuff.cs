using UnityEngine;
using System.Linq;
using System;

[Serializable]
public class ConfigBuffDebuffRecord
{
    [SerializeField]
    private int id = 0;
    [SerializeField]
    private string prefab = "";
    [SerializeField]
    private string sprite = "";
    [SerializeField]
    private int value = 0;
    [SerializeField]
    private int isBuff = 0;
    [SerializeField]
    private string fieldName = "";
    [SerializeField]
    private float effectTime = 0;
    [SerializeField]
    private int isChangeProjectile = 0; 
    [SerializeField]
    private int isEnemy = 0;

    public int Id { get => id;}
    public string Prefab { get => prefab;}
    public string Sprite { get => sprite;}
    public int Value { get => value;}
    public bool IsBuff { get => isBuff == 1? true : false;}
    public string FieldName { get => fieldName;}
    public float EffectTime { get => effectTime;}
    public bool IsChangeProjectile { get => isChangeProjectile == 1 ? true : false;  }
    public bool IsEnemy { get => isEnemy == 1 ? true : false; }
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


