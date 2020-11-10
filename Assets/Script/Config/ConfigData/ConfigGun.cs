using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConfigGunRecord
{
    [SerializeField]
    private int id = 0;
    [SerializeField]
    private string prefab = "";
    [SerializeField]
    private string activeSprite = "";
    [SerializeField]
    private string inActiveSprite = "";

    public int Id { get => id;}
    public string Prefab { get => prefab;}
    public string ActiveSprite { get => activeSprite;}
    public string InActiveSprite { get => inActiveSprite; }
}

public class ConfigGun : BYDataTable<ConfigGunRecord>
{
    public override void SetCompareObject()
    {
        recoreCompare = new ConfigCompareKey<ConfigGunRecord>("id");
    }

    public List<ConfigGunRecord> GetAll()
    {
        return records;
    }
}
