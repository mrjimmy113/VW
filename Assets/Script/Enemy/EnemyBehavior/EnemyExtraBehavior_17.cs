using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExtraBehavior_17 : EnemyExtraBehavior
{
    [SerializeField]
    private Transform prefab;
    [SerializeField]
    private int numberOfSpawnOnDead = 3;
    [SerializeField]
    private float hpFactor = 3f;


    public override void OnDead(OnEnemyDeadParam param)
    {
        EnemyInfor infor = new EnemyInfor();
        infor.hp = Mathf.RoundToInt(control.inforEnemy.hp / hpFactor);
        infor.duplicate = null;
        infor.isDuplicate = false;
        infor.spawnBuffDebuff = false;
        infor.sizeScale = 1;
        infor.coinAmount = 0;
        for (int i = 0; i < numberOfSpawnOnDead; i++)
        {
            Transform obj = Instantiate(prefab, null);
            obj.position = transform.position;
            obj.GetComponent<EnemyControl>().Setup(infor, true, false);
        }

        base.OnDead(param);
    }
}
