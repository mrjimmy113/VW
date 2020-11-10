using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenceManager : Singleton<LoadScenceManager>
{
    private Coroutine coroutine_;
    public event Action<string> OnLoadScenceByNameComplete;
    

    public void LoadSceneIndex(int index, Action callBack)
    {

    }

    public void LoadSceneByname(string name, Action callBack)
    {
        if (coroutine_ != null)
        {
            StopCoroutine(coroutine_);
            coroutine_ = null;
        }

        coroutine_ = StartCoroutine(LoadByName(name, callBack));

    }

    public void LoadScenceByNameInstant(string name, Action callBack)
    {
        if (coroutine_ != null)
        {
            StopCoroutine(coroutine_);
            coroutine_ = null;
        }

        coroutine_ = StartCoroutine(LoadByNameWithOutLoadingView(name, callBack));
    }

    IEnumerator LoadByName(string sceneName, Action callBack)
    {
        yield return new WaitForSecondsRealtime(0.2f);
        LoadingView loadView = null;
        ViewManager.instance.OnSwitchView(ViewIndex.LoadingView, null, (view) =>
        {
            loadView = (LoadingView)view;
        });

        yield return new WaitUntil(() => loadView != null);

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        while (async.isDone)
        {
            Debug.Log(async.isDone);
            if (async.progress < 0.5f)
                loadView.UpdateProgress(async.progress);
        }



        float count = 0.5f;
        while (count < 1)
        {
            yield return new WaitForSecondsRealtime(0.05f);
            count += 0.01f;

            loadView.UpdateProgress(count);
        }
        yield return new WaitForSecondsRealtime(1);
        callBack?.Invoke();
        OnLoadScenceByNameComplete?.Invoke(sceneName);
        
        

    }

    IEnumerator LoadByNameWithOutLoadingView(string sceneName, Action callBack)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        yield return new WaitUntil(() => async.isDone);
        callBack?.Invoke();
        OnLoadScenceByNameComplete?.Invoke(sceneName);



    }


}