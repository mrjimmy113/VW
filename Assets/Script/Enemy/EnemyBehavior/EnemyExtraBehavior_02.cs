using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EnemyExtraBehavior_02 : EnemyExtraBehavior
{
    [Header("Enemy_02")]

    [SerializeField]
    private float healRadius = 3f;
    [SerializeField]
    private int healAmount = 1;
    [SerializeField]
    private int healCapacity = 3;

    [SerializeField]
    private float coolDown = 10f;    
    [SerializeField]
    private float healingTime = 5f;
    [SerializeField]
    private float healingDelay = 0.1f;

    private string healerTag = "Healer";

    //For Healing VFX
    private LineRenderer lineRenderer;

    private List<EnemyControl> healTargets = new List<EnemyControl>();

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public override void Setup()
    {
        StartCoroutine(CheckHealing());
        StartCoroutine(HealingVFX());
        control.OnEnemyDead += OnDead;
    }

    private void OnDead(int obj)
    {
        healTargets.Clear();
        lineRenderer.positionCount = 0;
        StopAllCoroutines();
    }

    IEnumerator CheckHealing()
    {
        WaitForSeconds wait = new WaitForSeconds(coolDown);
        yield return new WaitForSeconds(3f);
        while (true)
        {
            StartCoroutine(StartHealing());

            yield return wait;
            
        }
    }

    IEnumerator StartHealing()
    {
        WaitForSeconds wait = new WaitForSeconds(healingDelay);
        float time = 0;
        while(time < healingTime)
        {
            healTargets.Clear();
            Collider2D[] cols = Physics2D.
                OverlapCircleAll(transform.position, healRadius * transform.localScale.y, LayerConfig.ENEMY_LAYER);
            foreach(var c in cols)
            {
                if(c.tag != healerTag)
                {
                    EnemyControl control = c.GetComponent<EnemyControl>();
                    control.Heal(healAmount);
                    if (healTargets.Count < 3) healTargets.Add(control);
                }
            }
            yield return wait;
            time += healingDelay;
        }
        healTargets.Clear();
    }

    IEnumerator HealingVFX()
    {
        WaitForSeconds updateTime = new WaitForSeconds(0.02f);
        while(true)
        {
            yield return updateTime;
            
            List<Vector3> vector3s = new List<Vector3>();
            lineRenderer.positionCount = healTargets.Count * 2;
            foreach (var enemy in healTargets)
            {
                vector3s.Add(transform.position);
                vector3s.Add(enemy.transform.position);
            }
            
            
            lineRenderer.SetPositions(vector3s.ToArray());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, healRadius * transform.localScale.y);
    }
}
