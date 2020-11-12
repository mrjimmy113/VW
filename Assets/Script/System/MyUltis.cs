using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Runtime.CompilerServices;

public class DistanceData
{
    public Transform trans;
    public float distance;
}
public static class MyUltis
{
    private static readonly int K_FORMAT_COUNTER = 1000;
    private static readonly int M_FORMAT_COUNTER = 1000 * 1000;


    public static Transform GetClosestColliderTransform(Collider[] colliders, Vector3 position)
    {
        List<DistanceData> dataList = new List<DistanceData>();
        foreach(var c in colliders)
        {
            DistanceData data = new DistanceData();
            data.trans = c.transform;
            data.distance = Vector3.Distance(position, c.transform.position);
            dataList.Add(data);
        }

        return dataList.OrderBy(x => x.distance).FirstOrDefault().trans;

    }

    public static List<int> StringToIntegerList(string str)
    {
        List<int> ls = new List<int>();
        string[] s = str.Split(';');
        foreach (string e in s)
        {
            int w = int.Parse(e);
            ls.Add(w);
        }

        return ls;
    }

    public static string ListIntToString(this List<int> input)
    {
        string result = "";
        foreach(var i in input)
        {
            result += (i + "");
        }
        return result;
    }

    public static string ToMinuteAndSecond(this int remainTime)
    {
        int minute = remainTime / 60;
        string strMinute = "";
        if (minute < 10) strMinute = "0" + minute;
        else strMinute = minute + "";

        int second = remainTime % 60;
        
        string strSecond = "";

        if (second < 10) strSecond = "0" + second;
        else strSecond = second + "";

        return strMinute + ":" + strSecond;
    }

    public static float NextAngle(this float angle, float increaseAmount)
    {
        float result = angle + increaseAmount;
        if(result > 360)
        {
            result -= 360;
        }

        return result;
    }
    


    public static string NumberNormalize (this int number)
    {
        if (number < K_FORMAT_COUNTER) return number.ToString();
        else if(number < M_FORMAT_COUNTER && number >= K_FORMAT_COUNTER)
        {

            return KFormat(number);

        }else if(number >= M_FORMAT_COUNTER)
        {
            return MFormat(number);
        }

        return "";
       
    }

    private static string KFormat(int number)
    {
        string result = (number / K_FORMAT_COUNTER).ToString();
        int lower = (number % K_FORMAT_COUNTER);
        if (lower > (K_FORMAT_COUNTER / 10))
        {
            result += ".";
            result += lower.ToString()[0];
        }
        result += "K";
        return result;
    }

    private static string MFormat(int number)
    {
        string result = (number / M_FORMAT_COUNTER).ToString();
        int lower = (number % M_FORMAT_COUNTER);
        if (lower > (M_FORMAT_COUNTER / 10))
        {
            result += ".";
            result += lower.ToString()[0];
        }
        result += "M";
        return result;
    }

}

public class MyUltisGeneric<T>
{
    public static T[] SingleToArray(T any)
    {
        if (any == null) return new T[0];
        else
        {
            T[] result = new T[1];
            result[0] = any;
            return result;
        }
    }
}