using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExtraBehavior_13 : EnemyExtraBehavior
{
    [SerializeField]
    private float invincibleTime = 2f;

    [SerializeField]
    private float coodDown = 10f;

    public override void Setup()
    {
        StartCoroutine(StartCheck());
    }

    IEnumerator StartCheck()
    {
        WaitForSeconds buffTime = new WaitForSeconds(invincibleTime);
        WaitForSeconds cd = new WaitForSeconds(coodDown);
        yield return new WaitForSeconds(3f);
        while(true)
        {
            control.isInvincible = true;
            yield return buffTime;
            control.isInvincible = false;
            yield return coodDown;
        }
    }
}
