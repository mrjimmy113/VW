using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExtraBehavior_10 : EnemyExtraBehavior
{
    [SerializeField]
    private float maxExtraSize = 5;
    [SerializeField]
    private float minExtraSize = 0;
    [SerializeField]
    private float timeBetweenChange = 2f;
    [SerializeField]
    private float timeChange = 1f;

    public override void Setup()
    {
        StartCoroutine(ChangeSize());
    }

    IEnumerator ChangeSize()
    {
        WaitForSeconds wait = new WaitForSeconds(timeBetweenChange + timeChange);
        while (true)
        {
            yield return wait;
            float size = control.inforEnemy.sizeScale + UnityEngine.Random.Range(minExtraSize, maxExtraSize);
            StartCoroutine(StartChangeSize(size));
        }
    }

    IEnumerator StartChangeSize(float toSize) 
    {
        
        var currentScale = control.transform.localScale;
        Vector2 newSize = new Vector2(toSize, toSize);
        var t = 0f;
        while(t < 1)
        {
            t += Time.deltaTime / timeChange;
            control.transform.localScale = Vector2.Lerp(currentScale, newSize, t);
            yield return null;
        }
    }
}
