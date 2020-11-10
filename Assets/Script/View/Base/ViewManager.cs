using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ViewManager : Singleton<ViewManager>
{
    /// <summary>
    /// Event switch view , index is Previous view, View is currentView
    /// </summary>
    public event Action<ViewIndex, BaseView> OnSwitchViewEvent;
    public Transform anchorView;
    private Dictionary<ViewIndex, BaseView> dicView = new Dictionary<ViewIndex, BaseView>();
    public BaseView currentView;
    public ViewIndex initViewIndex = ViewIndex.EmptyView;
    // Start is called before the first frame update

    public override void OnAwake()
    {
        
        foreach (ViewIndex e in ViewConfig.viewIndices)
        {
            GameObject viewObject = Instantiate(Resources.Load("View/" + e.ToString(), typeof(GameObject))) as GameObject;
            viewObject.transform.SetParent(anchorView, false);
            BaseView view = viewObject.GetComponent<BaseView>();
            dicView.Add(view.viewIndex, view);
            viewObject.SetActive(false);
        }
        
        LoadScenceManager.instance.OnLoadScenceByNameComplete += (name) =>
        {
            if (name == "Buffer")
            {
                OnSwitchView(ViewIndex.HomeView);
            }
        };
        OnSwitchView(initViewIndex);
    }


    public void OnSwitchView(ViewIndex index,ViewParam param=null,Action<BaseView> callback=null)
    {
        if (index == ViewIndex.None)
            return;
         if(currentView!=null)
        {
            currentView.HideView(() =>
            {
                ViewIndex pre = currentView.viewIndex;
                currentView.gameObject.SetActive(false);
                currentView = dicView[index];
                currentView.gameObject.SetActive(true);
                currentView.ShowView(param,()=> {
                    callback?.Invoke(currentView);
                });
                OnSwitchViewEvent?.Invoke(pre,currentView);
            });

        }
         else
        {
            currentView = dicView[index];
            currentView.gameObject.SetActive(true);
            currentView.ShowView(param, () => {
                callback?.Invoke(currentView);
            });
            OnSwitchViewEvent?.Invoke(ViewIndex.None, currentView);
        }
    }
  
}
