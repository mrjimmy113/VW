using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootLoader : Singleton<BootLoader>
{

    private void Awake()
    {

    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        ConfigurationManager.instance.InitConfig(() =>
        {
            DataAPIController.instance.OnInitData(() =>
            {
                ResourcesGainHandler.instance.InitHandler();
                DailyQuestControl.instance.InitQuest();
                LoadScenceManager.instance.LoadSceneByname("Buffer", () =>
                {
                    
                });
            });
        });
    }
}
