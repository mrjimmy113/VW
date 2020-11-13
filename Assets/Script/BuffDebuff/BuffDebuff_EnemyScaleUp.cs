using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDebuff_EnemyScaleUp : BuffDebuff
{
    public override void AfterEffect(EffectData data)
    {
        List<EnemyControl> enemies = EnemyFactory.instance.activeEnemy;
        foreach (var e in enemies)
        {
            if (e.appliedBuffDebuffId.Contains(cf.Id))
            {
                e.appliedBuffDebuffId.Remove(cf.Id);
                e.StartCoroutine(StartChangeSize(e, e.transform.localScale.x / cf.Value));
            }
        }
        EnemyFactory.instance.OnNewEnemyAddEvent -= OnNewEnemyAddEvent;
    }

    public override EffectData OnEffect()
    {
        EffectData data = new EffectData();
        
        List<EnemyControl> enemies = EnemyFactory.instance.activeEnemy;
        foreach (var e in enemies)
        {
            if (!e.appliedBuffDebuffId.Contains(cf.Id))
            {
                e.appliedBuffDebuffId.Add(cf.Id);
                e.StartCoroutine(StartChangeSize(e, e.transform.localScale.x * cf.Value));
            }
        }

        EnemyFactory.instance.OnNewEnemyAddEvent += OnNewEnemyAddEvent;
        return data;
    }

    private void OnNewEnemyAddEvent(OnNewEnemyAddParam param)
    {
        
        if (!param.control.appliedBuffDebuffId.Contains(cf.Id))
        {
            param.control.appliedBuffDebuffId.Add(cf.Id);
            param.control.StartCoroutine(StartChangeSize(param.control, param.control.transform.localScale.x * cf.Value));
        }
    }

    IEnumerator StartChangeSize(EnemyControl control, float toSize)
    {

        var currentScale = control.transform.localScale;
        Vector2 newSize = new Vector2(toSize, toSize);
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / 1f;
            control.transform.localScale = Vector2.Lerp(currentScale, newSize, t);
            yield return null;
        }
    }

}
