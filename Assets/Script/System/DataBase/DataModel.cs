using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine.Events;
using System.Data;

public class DataModel : MonoBehaviour
{
    public DataEventTrigger onDataChange= new DataEventTrigger();
    private PlayerData player;
    public bool LoadData()
    {
        if (PlayerPrefs.HasKey("PlayerData"))
        {
            string s = PlayerPrefs.GetString("PlayerData");
            player = JsonConvert.DeserializeObject<PlayerData>(s);
            return true;
        }
        return false;
    }
    public void CreateNewData()
    {
        
        player = new PlayerData();
        //INFOR
        PlayerInfo info = new PlayerInfo();
        info.nickname = "Hero";
        info.enquip = 1;
        player.info = info;

        //STAT
        PlayerStat stat = new PlayerStat();
        stat.damageLevel = 1;
        stat.fireRateLevel = 1;
        stat.goldValueLevel = 1;
        stat.dailyGoldLevel = 1;
        player.stat = stat;

        //INVENTORY
        PlayerInventory inventory = new PlayerInventory();
        inventory.gold = 2000;
        inventory.energy = 50;
        inventory.diamond = 100;
        player.inventory = inventory;

        //GUN INVENTORY
        GunInfor gunInfor1 = new GunInfor();
        gunInfor1.id = 1;
        gunInfor1.damageLevel = 1;
        gunInfor1.rofLevel = 1;
        player.inventory.guns.Add(gunInfor1.id.ToKey(), gunInfor1);

        GunInfor gunInfor2 = new GunInfor();
        gunInfor2.id = 2;
        gunInfor2.damageLevel = 2;
        gunInfor2.rofLevel = 2;
        player.inventory.guns.Add(gunInfor2.id.ToKey(), gunInfor2);



        //MISSION;
        player.currentMisison = 1;

        long time = DateTimeOffset.Now.ToUnixTimeSeconds();
        player.goldStartTime = time;
        player.energyStartTime = time;
        
        player.goldDailySaved = 0;
        player.quest = null;

        SaveData();
        
    }
    private void OnDisable()
    {
        SaveData();
    }
    // Start is called before the first frame update
    private void SaveData()
    {
        string jSon = JsonConvert.SerializeObject(player);
        PlayerPrefs.SetString("PlayerData", jSon);
    }

 

    //------------------------------- read data -------------------------------------------
    public object Read(string path)
    {
        List<string> paths = path.GetPath();
        return ReadDataByPath(player, paths);

    }
    public object ReadDataByPath(object data, List<string> paths)
    {
        string key = paths[0];
        Type type = data.GetType();
        FieldInfo fieldInfo = type.GetField(key);
        object dataKey = fieldInfo.GetValue(data);
        if (paths.Count == 1)
        {
            return dataKey;
        }
        else
        {
            paths.RemoveAt(0);
            return ReadDataByPath(dataKey, paths);
        }
    }
    // ------------------- update data ------------------------
    public void UpdateData(string path, object dataNew)
    {
        List<string> paths = path.GetPath();
        UpdateDataByPath(player, dataNew, paths);
        onDataChange?.Invoke(path, dataNew);
        SaveData();
    }
    public void UpdateDataByPath(object data, object dataNew, List<string> paths)
    {

        string key = paths[0];
        Type type = data.GetType();
        FieldInfo fieldInfo = type.GetField(key);
        if (paths.Count == 1)
        {
            fieldInfo.SetValue(data, dataNew);
        }
        else
        {
            object dataKey = fieldInfo.GetValue(data);
            paths.RemoveAt(0);
            UpdateDataByPath(dataKey, dataNew, paths);
        }
    }
    // --------------------------- update data Dictionary -------------------------
    public void UpdateDataDic<T>(string path, object key, T dataNew)
    {
        List<string> paths = path.GetPath();
        object dataOut = null;
        UpdateDataDicByPath<T>(player, dataNew, paths, key, out dataOut);
        onDataChange?.Invoke(path+"/"+key.ToKey(), dataNew);
        onDataChange?.Invoke(path , dataOut);
        SaveData();
    }
    public void UpdateDataDicByPath<T>(object data, T dataNew, List<string> paths, object key,out object dataOut)
    {
        
        string fieldname = paths[0];
        Type type = data.GetType();
        
        FieldInfo fieldInfo = type.GetField(fieldname);
        if (paths.Count == 1)
        {
            Dictionary<string, T> dic = new Dictionary<string, T>();
            object dataKey = fieldInfo.GetValue(data);
            if (dataKey != null)
                dic = (Dictionary<string, T>)dataKey;
            string key_ = key.ToKey();
            dic[key_] = dataNew;
            dataOut = dic;
            fieldInfo.SetValue(data, dic);
        }
        else
        {
            object dataKey = fieldInfo.GetValue(data);
            paths.RemoveAt(0);
            UpdateDataDicByPath(dataKey, dataNew, paths,key, out dataOut);
        }
    }
}


public static class DataUtilities
{
    public static string ToKey(this object text)
    {
        string s = "K_" + text.ToString();
        return s;
    }

    public static int ReverseKey(this string text)
    {
        string s = text.Replace("K_", "");
        return Int32.Parse(s);
    }

    public static List<string> GetPath(this string path)
    {
        return path.Split('/').ToList();
    }
}
[System.Serializable]
public class DataEvent : UnityEvent<object>
{

}
public class DataEventTrigger : UnityEvent<string, object>
{

}

