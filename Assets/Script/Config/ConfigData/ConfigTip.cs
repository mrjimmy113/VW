using UnityEngine;
using System.Linq;
using System;

[System.Serializable]
public class ConfigTipRecord
{
    [SerializeField]
    private int id = 0;
    [SerializeField]
    private string tip = null;

    public int Id { get { return id; } }
    public string Tip { get { return tip; } }
}
public class ConfigTip : BYDataTable<ConfigTipRecord>
{

    public override void SetCompareObject()
    {
        recoreCompare = new ConfigCompareKey<ConfigTipRecord>("id");
    }

    public ConfigTipRecord GetRandom()
    {
        return records.OrderBy(r => Guid.NewGuid()).First();
    }
}
