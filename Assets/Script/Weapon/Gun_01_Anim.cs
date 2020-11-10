using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Gun_01_Anim : MonoBehaviour
{
    public Transform core1;
    public Transform core2;
    public Transform bullet1;
    public Transform bullet2;

    private Gun_01_Behavior behavior;

    private Tween bullet1Idle;
    private Tween bullet2Idle;

    private void Awake()
    {
        
    }

    private void Start()
    {
        if(behavior == null)
        {
            behavior = GetComponent<Gun_01_Behavior>();
            behavior.OnFireEvent += OnFireEvent;
        }
        core1.DOLocalRotate(new Vector3(0, 0, 90), 0.1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
        core2.DOLocalRotate(new Vector3(0, 0, -90), 0.1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
        bullet1Idle = bullet1.DOScale(new Vector2(0.2f, 0.2f), 0.2f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        bullet2Idle =  bullet2.DOScale(new Vector2(0.2f, 0.2f), 0.2f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);

    }

    private void OnFireEvent(bool isLeft)
    {
        if(isLeft)
        {
            bullet1Idle.Kill();
            bullet1.localScale = Vector2.zero;
            bullet1.DOScale(new Vector2(0.25f, 0.25f), 0.5f).SetEase(Ease.Linear).OnComplete(() => 
            {
                bullet1Idle = bullet1.DOScale(new Vector2(0.2f, 0.2f), 0.2f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
            });
        }
        else
        {
            bullet2Idle.Kill();
            bullet2.localScale = Vector2.zero;
            bullet2.DOScale(new Vector2(0.25f, 0.25f), 0.5f).SetEase(Ease.Linear).OnComplete(() =>
            {
                bullet2Idle = bullet1.DOScale(new Vector2(0.2f, 0.2f), 0.2f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
            });
        }
        
        
        
        
    }
}
