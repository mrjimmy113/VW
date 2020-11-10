using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDialog : MonoBehaviour
{
    public DialogIndex dialogIndex;
    private BaseDialogAnimation dialogAnimation;

    private void Awake()
    {
        dialogAnimation = GetComponentInChildren<BaseDialogAnimation>();
        
    }

    public void ShowDialog(DialogParam param, Action callback)
    {
        Setup(param);
        dialogAnimation.OnShowAnim(() =>
        {

            
            OnShowDialog();
            
            callback?.Invoke();
        });

    }

    public virtual void OnShowDialog()
    {

    }
    public virtual void Setup(DialogParam param)
    {

    }
    public void HideDialog(Action callback)
    {
        dialogAnimation.OnHideAnim(() =>
        {
            OnHideDialog();
            callback?.Invoke();
        });

    }
    public virtual void OnHideDialog()
    {

    }
}
