using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExtraBehavior_06 : EnemyExtraBehavior
{
    [SerializeField]
    private float checkTime = 0.1f;
    [SerializeField]
    private float checkRadius = 0.5f;
    [SerializeField]
    private float coolDown = 20f;
    [SerializeField]
    private float startDelayTime = 5f;
    [SerializeField]
    private Transform prefab;

    private string eaterTag = "Eater";

    private void Start()
    {
        
    }

    public override void Setup()
    {
        StartCoroutine(StartCheck());
        
    }

    IEnumerator StartCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(checkTime);
        WaitForSeconds cd = new WaitForSeconds(coolDown);
        bool isUseSkill;
        yield return new WaitForSeconds(startDelayTime);
        while (true)
        {
            isUseSkill = false;
            Collider2D[] cols = Physics2D.
                OverlapCircleAll(transform.position, checkRadius * transform.localScale.y, LayerConfig.ENEMY_LAYER);
            foreach (var c in cols)
            {
                if (c.tag != eaterTag && c.transform.localScale.y < transform.localScale.y)
                {
                    isUseSkill = true;
                    control.currentSpeed = 0;
                    c.gameObject.SetActive(false);

                    EnemyControl old = c.GetComponent<EnemyControl>();

                    EnemyInfor newEnemyInfor = control.inforEnemy;
                    newEnemyInfor.hp =old.hp;
                    newEnemyInfor.sizeScale = control.inforEnemy.sizeScale;

                    EnemyFactory.instance.RemoveEnemy(old);
                    Destroy(c.gameObject);

                    EnemyControl newEnemy = EnemyFactory.instance.CreateEnemy(prefab.gameObject);
                    newEnemy.gameObject.SetActive(false);
                    


                    
  
                    yield return new WaitForSeconds(2f);
                    control.currentSpeed = control.speed;
                    newEnemy.gameObject.SetActive(true);
                    newEnemy.Setup(newEnemyInfor, newEnemyInfor.isDuplicate, true);
                    break;
                }
            }

            if (isUseSkill) yield return cd;
            else yield return wait;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, checkRadius * transform.localScale.y);
    }
}
