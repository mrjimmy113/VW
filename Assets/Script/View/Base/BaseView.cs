using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewParam
{

}


public class BaseView : MonoBehaviour
{
    



    public  void ShowView(ViewParam param,Action callback)
    {
        Setup(param);

    
    }
   
    public virtual void OnShowView()
    {

    }
    public virtual void Setup(ViewParam param)
    {

    }
    public void HideView(Action callback)
    {
        
  
    }
    public virtual void OnHideView()
    {

    }
}
