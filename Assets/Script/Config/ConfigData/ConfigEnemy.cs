using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConfigEnemyRecord
{
    [SerializeField]
    private int id = 0;
    [SerializeField]
    private int hp = 0;
    [SerializeField]
    private float sizeScale = 0;
    [SerializeField]
    private string prefab = "";
    [SerializeField]
    private int isDuplicate = 0;
    [SerializeField]
    private string enemyDuplicateIds = "";
    [SerializeField]
    private int coinAmount = 0;
    [SerializeField]
    private int spawnBuffDebuff = 0;

    public int Id { get => id; }
    public int Hp { get => hp; }
    public float SizeScale { get => sizeScale; }
    public string Prefab { get => prefab; }
    public bool IsDuplicate { get => isDuplicate == 1 ? true : false; }
    public List<int> EnemyDuplicateIds { get => MyUltis.StringToIntegerList(enemyDuplicateIds); }
    public int CoinAmount { get => coinAmount;}
    public bool SpawnBuffDebuff { get => spawnBuffDebuff == 1 ? true : false; }
}

public class ConfigEnemy: BYDataTable<ConfigEnemyRecord>
{
    public override void SetCompareObject()
    {
        recoreCompare = new ConfigCompareKey<ConfigEnemyRecord>("id");
    }
}
