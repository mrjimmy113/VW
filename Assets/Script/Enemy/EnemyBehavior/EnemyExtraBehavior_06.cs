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

    private string eaterTag = "Eater";

    private void Start()
    {
        
    }

    public override void Setup()
    {
        StartCoroutine(StartCheck());
        control.OnEnemyDead += OnDead;
    }

    private void OnDead(int obj)
    {
        StopAllCoroutines();
    }

    IEnumerator StartCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(checkTime);
        WaitForSeconds cd = new WaitForSeconds(coolDown);
        bool isUseSkill;
        yield return new WaitForSeconds(3f);
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
                    c.GetComponent<EnemyExtraBehavior>().enabled = false;
                    c.transform.localScale = transform.localScale;
                    EnemyExtraBehavior_06 behavior = c.gameObject.AddComponent<EnemyExtraBehavior_06>();
                    behavior.Setup();
                    yield return new WaitForSeconds(2f);
                    control.currentSpeed = control.speed;
                    c.gameObject.SetActive(true);
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
