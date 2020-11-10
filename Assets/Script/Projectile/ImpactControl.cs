using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactControl : MonoBehaviour
{
    private string impactPoolName;
    [SerializeField]
    private float despawnTime = 0.5f;

    public void Setup(string impactPoolName)
    {
        this.impactPoolName = impactPoolName;
        StartCoroutine(Despawn());
    }

    IEnumerator  Despawn()
    {
        yield return new WaitForSecondsRealtime(despawnTime);
        PoolManager.instance.dicPool[impactPoolName].Despwan(transform);
    }
}
