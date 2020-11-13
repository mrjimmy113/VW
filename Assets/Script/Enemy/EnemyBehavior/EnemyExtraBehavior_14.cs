using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExtraBehavior_14 : EnemyExtraBehavior
{
    [SerializeField]
    private int numberOfShoot = 3;

    [SerializeField]
    private float coolDown = 10f;
    [SerializeField]
    private float hpFactor = 10f;

    [SerializeField]
    private Transform prefab;

    public override void Setup()
    {
        StartCoroutine(StartCheck());
        prefab.localScale = Vector3.one;
    }

    IEnumerator StartCheck()
    {

        WaitForSeconds cd = new WaitForSeconds(coolDown);
        EnemyInfor infor = new EnemyInfor();
        infor.hp = Mathf.RoundToInt(control.inforEnemy.hp / hpFactor);
        infor.duplicate = null;
        infor.isDuplicate = false;
        infor.spawnBuffDebuff = false;
        infor.sizeScale = 1;
        infor.coinAmount = 0;

        yield return new WaitForSeconds(coolDown / 2);

        while (true)
        {
            float startAngle = UnityEngine.Random.Range(0, 360);

            for (int i = 0; i < numberOfShoot; i++)
            {
                EnemyControl enemyControl = EnemyFactory.instance.CreateEnemy(prefab.gameObject);
                enemyControl.transform.position = transform.position;
                enemyControl.Setup(infor, false, false);
                enemyControl.SetDirection(startAngle);
                startAngle = startAngle.NextAngle(360 / numberOfShoot);
            }



            yield return cd;

        }
    }
}
