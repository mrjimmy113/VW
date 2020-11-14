using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ConfigMissionRecord
{
    [SerializeField]
    private int id = 0;
    [SerializeField]
    private string waves = "";
    [SerializeField]
    private int clearEnergyReward = 0;
    [SerializeField]
    private int clearGunUnLockId = 0;
    

    public int Id { get => id;}
    public List<int> Waves { get => MyUltis.StringToIntegerList(waves);}
    public int ClearEnergyReward { get => clearEnergyReward; }
    public int ClearGunUnLockId { get => clearGunUnLockId;}

    public bool IsBossMission { get => id % 5 == 0 ? true : false; }
}

public class ConfigMission : BYDataTable<ConfigMissionRecord>
{
    public override void SetCompareObject()
    {
        recoreCompare = new ConfigCompareKey<ConfigMissionRecord>("id");
    }
}
