using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[System.Serializable]
public class ConfigDailyQuestRecord
{
    [SerializeField]
    private int id = 0;
    [SerializeField]
    private string description = "";
    [SerializeField]
    private string prefab = "";
    [SerializeField]
    private int minRequire = 0;
    [SerializeField]
    private int maxRequire = 0;
    [SerializeField]
    private int minReward = 0;
    [SerializeField]
    private int maxReward = 0;

    public int Id { get => id; set => id = value; }
    public string Description { get => description;}
    public int MinRequire { get => minRequire;}
    public int MaxRequire { get => maxRequire;}
    public int MinReward { get => minReward;}
    public int MaxReward { get => maxReward;}
    public string Prefab { get => prefab;}
}

public class ConfigDailyQuest : BYDataTable<ConfigDailyQuestRecord>
{
    public override void SetCompareObject()
    {
        recoreCompare = new ConfigCompareKey<ConfigDailyQuestRecord>("id");
    }

    public List<ConfigDailyQuestRecord> GetRandomAmount(int amount)
    {
        return records.OrderBy(x => Guid.NewGuid()).Take(amount).ToList();
    }
}