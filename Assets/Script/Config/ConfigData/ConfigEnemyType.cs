using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[System.Serializable]
public class ConfigEnemyTypeRecord
{
    [SerializeField]
    private int id = 0;
    [SerializeField]
    private string prefab = "";

    public int Id { get => id;}
    public string Prefab { get => prefab;}
}


public class ConfigEnemyType :BYDataTable<ConfigEnemyTypeRecord> 
{
    public override void SetCompareObject()
    {
        recoreCompare = new ConfigCompareKey<ConfigEnemyTypeRecord>("id");
    }

    public ConfigEnemyTypeRecord GetRandom()
    {
        return records.OrderBy(r => Guid.NewGuid()).First();
    }
}
