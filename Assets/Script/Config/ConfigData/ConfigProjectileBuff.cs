using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigProjectileBuffRecord
{
    [SerializeField]
    private int id;
    [SerializeField]
    private string idSequences;
    [SerializeField]
    private string sprite;

    public string IdSequences { get => idSequences;}
    public string Sprite { get => sprite;}
}

public class ConfigProjectileBuff :BYDataTable<ConfigBuffDebuffRecord>
{
    public override void SetCompareObject()
    {
        recoreCompare = new ConfigCompareKey<ConfigBuffDebuffRecord>("id");
    }
}
