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

    public ItemBase GetItemData(int index)
    {
        ItemBase item;
        if (dataManager.items.ContainsKey(index) && dataManager.items[index]!=null)
        {
            item = dataManager.items[index];
            return dataManager.items[index];
        }
        else if(dataManager.items.ContainsKey(index)&&dataManager.items[index] == null)
        {
            item = dataManager.items[index]=ItemMake(index);
            return item;
        }
        else
        {
            item = ItemMake(index);
            dataManager.items.Add(index, item);
            return item;
        }
    }

    public ItemBase ItemMake(int index)
    {
        ItemType type = ItemType.None;

        TryConvertEnum(dataManager.itemDatas.datas[index],"type",ref type);
        //TrySetValue(dataManager.itemDatas.datas[index],"type", ref type);
        ItemBase item;
        switch (type)
        {
            case ItemType.Material:
                item = new MaterialItem();
                break;
            case ItemType.Tool:
                item = new ToolItem();
                break;
            case ItemType.Potion:
                item = new PotionItem();
                break;
            case ItemType.Seed:
                item = new SeedItem();
                break;
            case ItemType.Fertilizer:
                item = new FertilizerItem();
                break;
            case ItemType.Weapon:
                item = new WeaponItem();
                break;
            case ItemType.Equipment:
                item = new EquipmentItem();
                break;
            case ItemType.None:
                item = new ItemBase();
                Debug.Log("Item Type Error!");
                break;
            default:
                item = new ItemBase();
                Debug.Log("Item Type Error!");
                break;
        }
        return SetItemData(index,item);
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
        if(TryConvertEnum(data.datas[index],"type",ref item.type))
        {

        }
        if (TrySetValue(data.datas[index], "id", ref item.id))
        {
        }
        /*if (TrySetValue(data.datas[index], "type", ref item.type))
        {
        }*/
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
        if (TrySetValue(data.datas[index], "throwable", ref item.throwable))
        {

        }
        if(item is EquipmentItem equipItem)
        {
            SetEquipStat(item.id, ref equipItem);
        }
        SetItemFunction(ref item);
        return item;
    }
    public void SetItemFunction(ref ItemBase item)
    {
        
    }
    public void SetEquipStat(int index,ref EquipmentItem item)
    {
        if (!dataManager.equipStat.ContainsKey(index))
        {
            Debug.Log("id Dind't Contain in ItemFactory SetEqupStatFunction");
            return;
        }
        item.equipStats = DeepCopyStatDict(dataManager.equipStat[index]);
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
    bool TryConvertEnum<T>(Dictionary<string,object> data, string key, ref T target) where T : struct,Enum 
    {
        if(data.ContainsKey(key)&& data[key] != null)
        {

            if (Enum.TryParse(data[key].ToString(), true, out target))
            {
                return true;
            }

        }



        return false;
    }
    Dictionary<string,int> DeepCopyStatDict(Dictionary<string,int> origin)
    {
        Dictionary<string, int> newDict = new Dictionary<string, int>();
        foreach(string key in origin.Keys)
        {
            newDict.Add(key, origin[key]);


        }

        return newDict;
    }
}
