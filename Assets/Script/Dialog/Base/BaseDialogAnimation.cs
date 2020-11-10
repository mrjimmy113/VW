using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


[RequireComponent(typeof(CanvasGroup))]
public class BaseDialogAnimation : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<CanvasGroup>().alpha = 0;
    }

    public virtual void OnShowAnim(Action callback)
    {
        gameObject.GetComponent<CanvasGroup>().DOFade(1, 0.25f).OnComplete(() => {

            callback?.Invoke();
        });
    }
    public virtual void OnHideAnim(Action callback)
    {
        gameObject.GetComponent<CanvasGroup>().DOFade(0, 0.25f).OnComplete(() => {


            callback?.Invoke();
        });
    }
    private void Reset()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.name = "BaseDialogAnimation";
    }
}
