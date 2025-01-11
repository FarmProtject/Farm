using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ItemFactory : MonoBehaviour
{
    DataManager dataManager;



    void Start()
    {
        SetField();
    }

    void Update()
    {
        
    }


    void SetField()
    {
        dataManager = GameManager.instance.dataManager;
    }

    ItemBase SetItemData(int index, ItemBase item)
    {
        DataClass data = dataManager.itemDatas;
        if (TrySetValue(data.datas[index] ,"name", ref item.name))
        {
        }
        else
        {
            Debug.Log("Name Didn't Set");
        }
        if (TrySetValue(data.datas[index], "type", ref item.type))
        {
        }
        else
        {
            Debug.Log("type Didn't Contain");
        }
        if(TrySetValue(data.datas[index],"description",ref item.description))
        {

        }
        else
        {
            Debug.Log("Description Didn't set");
        }

        if (TrySetValue(data.datas[index], "price", ref item.price))
        {

        }
        else
        {

        }

        if (TrySetValue(data.datas[index], "maxstack", ref item.maxStack))
        {

        }
        else
        {

        }
        if (TrySetValue(data.datas[index], "icon", ref item.icon))
        {

        }
        if (TrySetValue(data.datas[index], "useeffect", ref item.useeffect))
        {

        }
        if (TrySetValue(data.datas[index], "throwable", ref item.throwable))
        {

        }
        if (TrySetValue(data.datas[index], "damage", ref item.damage))
        {

        }
        if (TrySetValue(data.datas[index], "defense", ref item.defense))
        {

        }
        if (TrySetValue(data.datas[index], "speed", ref item.speed))
        {

        }
        if (TrySetValue(data.datas[index], "slot", ref item.slot))
        {

        }
        return item;
    }

    bool TrySetValue<T>(Dictionary<string,object> data , string key,ref T target)
    {
        if (data.ContainsKey(key) && data[key] != null)
        {
            try
            {
                target = (T)Convert.ChangeType(data[key], typeof(T));
                return true;
            }
            catch( Exception ex)
            {
                Debug.LogError($"Failed to convert{key}: {ex.Message}");
                return false;
            }
        }
        return false;
    }
}
