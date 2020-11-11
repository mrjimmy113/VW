using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExtraBehavior_16 : EnemyExtraBehavior
{
    [SerializeField]
    private float maxExtraSize = 2f;
    [SerializeField]
    private float extraSizePerHit = 0.1f;
    [SerializeField]
    private float shrinkBackTime = 1f;

    private float originalSize;


    private Coroutine startGrow;


    public override void Setup()
    {
        control.OnEnemyDamaged += OnEnemyDamaged;
        originalSize = transform.localScale.x;
        
    }

    private void OnEnemyDamaged(OnEnemyDamagedParam obj)
    {
        if(transform.localScale.x <= maxExtraSize + originalSize)
        {
            transform.localScale = new Vector2(transform.localScale.x + extraSizePerHit,
            transform.localScale.y + extraSizePerHit);
        }
        if (startGrow != null)
        {
            StopCoroutine(startGrow);
            startGrow = null;
        }
        startGrow = StartCoroutine(StartShrink());
    }

    IEnumerator StartShrink()
    {
        
        yield return new WaitForSeconds(1f);
        var t = 0f;
        Vector3 currentSizeVector = new Vector3(originalSize, originalSize, 0);
        while(t < 1)
        {
            t += Time.deltaTime / shrinkBackTime;
            transform.localScale = Vector3.Lerp(transform.localScale, currentSizeVector, t);

            yield return null;
        }
       
    }
}
