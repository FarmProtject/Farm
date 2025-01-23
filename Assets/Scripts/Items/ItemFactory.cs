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
            case ItemType.readySoilItem:
                item = new ReadySoilItem();
                break;
            case ItemType.soilItem:
                item = new SoilItem();
                break;
            case ItemType.equipmentItem:
                item = new EquipItem();
                break;
            case ItemType.harvestItem:
                item = new HarvestItem();
                break;
            case ItemType.consumableItem:
                item = new ConsumItem();
                break;
            case ItemType.None:
                item = new ItemBase();
                break;
            default:
                item = new ItemBase();
                Debug.Log("ItemTypeError");
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
        /*
        if (TrySetValue(data.datas[index], "useeffect", ref item.useeffect))
        {

        }*/
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
        if(item is FarmItem farmItem)
        {
            if (TrySetValue(data.datas[index], "layer", ref farmItem.layer))
            {

            }
        }
        SetItemFunction(ref item);
        return item;
    }
    public void SetItemFunction(ref ItemBase item)
    {
        
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
}
