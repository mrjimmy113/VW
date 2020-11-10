using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseView : MonoBehaviour
{
    public ViewIndex viewIndex;
    
    private BaseViewAnimation viewAnimation;

    private void Awake()
    {
        viewAnimation = GetComponentInChildren<BaseViewAnimation>();
    }

    public  void ShowView(ViewParam param,Action callback)
    {
        Setup(param);
        viewAnimation.OnShowAnim(() =>
        {

            
            OnShowView();
            
            callback?.Invoke();
        });
    
    }
   
    public virtual void OnShowView()
    {

    }
    public virtual void Setup(ViewParam param)
    {

    }
    public void HideView(Action callback)
    {
        viewAnimation.OnHideAnim(() =>
        {
            OnHideView();
            callback?.Invoke();
        });
  
    }
    public virtual void OnHideView()
    {

    }
}
