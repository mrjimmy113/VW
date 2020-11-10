using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

public class ConfigUtilities { }

public class ConfigCompareKey<T> : RecordCompare<T> where T : class,new()
{
    FieldInfo fieldInfo;
    public ConfigCompareKey(string fieldName)
    {
        Type mType = typeof(T);
        fieldInfo = mType.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
    }
    public override T GetKeySearch(object key)
    {
        T keyObject = new T();
        fieldInfo.SetValue(keyObject, key);

        return keyObject;
    }

    public override int OnRecordCompare(T x, T y)
    {
        object var_1 = fieldInfo.GetValue(x);
        object var_2 = fieldInfo.GetValue(y);
        if (var_1 == null && var_2 == null)
            return 0;
        else if (var_1 != null && var_2 == null)
            return 1;
        else if (var_1 == null && var_2 != null)
            return -1;
        else
        {
            return ((IComparable)var_1).CompareTo(var_2);

        }
    }
}
public class Compare2KeySearch<T1,T2>
{
    public T1 key_1;
    public T2 key_2;
}
public class ConfigCompare2Key<T,T1,T2> : RecordCompare<T> where T : class, new()
{
    FieldInfo fieldInfo1;
    FieldInfo fieldInfo2;
    public ConfigCompare2Key(string fieldName1, string fieldName2)
    {
        Type mType = typeof(T);
        fieldInfo1 = mType.GetField(fieldName1, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        fieldInfo2 = mType.GetField(fieldName2, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
    }
    public override T GetKeySearch(object key)
    {
        Compare2KeySearch<T1,T2> keySearch = (Compare2KeySearch<T1,T2>)key;
        T keyObject = new T();
        fieldInfo1.SetValue(keyObject, keySearch.key_1);
        fieldInfo2.SetValue(keyObject, keySearch.key_2);
        return keyObject;
    }

    public override int OnRecordCompare(T x, T y)
    {
        object var_1 = fieldInfo1.GetValue(x);
        object var_2 = fieldInfo1.GetValue(y);
        int result = ((IComparable)var_1).CompareTo(var_2);
        if (result != 0)
            return result;
        else
        {
            object var_1_ = fieldInfo2.GetValue(x);
            object var_2_ = fieldInfo2.GetValue(y);
            return ((IComparable)var_1_).CompareTo(var_2_);
        }
    }
}
