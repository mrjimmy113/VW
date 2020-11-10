using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ConfigWaveRecord
{
    [SerializeField]
    private int id = 0;
    [SerializeField]
    private int timeDelayOnEachSpawn = 0;
    [SerializeField]
    private int timeDelayToSpawn = 0;
    [SerializeField]
    private string enemyIds = "";
    [SerializeField]
    private string numbers = "";

    public int Id { get => id; }
    public int TimeDelayOnEachSpawn { get => timeDelayOnEachSpawn; }
    public int TimeDelayToSpawn { get => timeDelayToSpawn; }
    public List<int> EnemyIds { get => MyUltis.StringToIntegerList(enemyIds); }
    public List<int> Numbers { get => MyUltis.StringToIntegerList(numbers); }
}


public class ConfigWave : BYDataTable<ConfigWaveRecord>
{
    public override void SetCompareObject()
    {
        recoreCompare = new ConfigCompareKey<ConfigWaveRecord>("id");
    }
}
