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
        if (dataManager.items.ContainsKey(index) && dataManager.items[index] != null)
        {
            item = dataManager.items[index];
            return dataManager.items[index];
        }
        else if (dataManager.items.ContainsKey(index) && dataManager.items[index] == null)
        {
            item = dataManager.items[index] = ItemMake(index);
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
        ItemCategory category = ItemCategory.none;
        TryConvertEnum(dataManager.itemDatas.datas[index], "category", ref type);
        //TrySetValue(dataManager.itemDatas.datas[index],"type", ref type);
        ItemBase item = new ItemBase();
        switch (category)
        {
            case ItemCategory.farming:
                item = new Farming();
                break;
            case ItemCategory.equipment:
                item = new EquipmentItem();
                break;
            case ItemCategory.consumable:
                item = new Consumable();
                break;
            case ItemCategory.material:
                item = new MaterialItem();
                break;
            case ItemCategory.tools:
                item = new Tools();
                break;
            case ItemCategory.none:
                item = new ItemBase();
                break;
            default:
                break;
        }
        return SetItemData(index, item);
    }

    ItemBase SetItemData(int index, ItemBase item)
    {
        DataClass data = dataManager.itemDatas;
        if (TrySetValue(data.datas[index], "name", ref item.name))
        {
        }
        else
        {
            Debug.Log("Name Didn't Set");
        }
        if (TryConvertEnum(data.datas[index], "type", ref item.type))
        {

        }
        if (TryConvertEnum(data.datas[index], "category", ref item.category))
        {

        }
        if (TrySetValue(data.datas[index], "id", ref item.id))
        {
        }
        if (TrySetValue(data.datas[index], "description", ref item.description))
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
        if(item is EffectItem effectItem)
        {
            effectItem.useEffectKey = CategoryToTable(item.category)[effectItem.id]["useEffect"].ToString();
        }
        switch (item.category)
        {
            case ItemCategory.farming:
                if(item is Farming farm)
                {
                    farm.useEffectKey = CategoryToTable(item.category)[item.id]["useEffect"].ToString();
                }
                break;
            case ItemCategory.equipment:
                if (item is EquipmentItem equip)
                {
                    SetEquipStat(item.id, ref equip);
                    string slot = CategoryToTable(item.category)[item.id]["slot"].ToString();
                    //equip.useEffectKey = CategoryToTable(item.category)[item.id]["useEffect"].ToString();
                    equip.slot = (EquipSlot)Enum.Parse(typeof(EquipSlot), slot);
                }
                break;
            case ItemCategory.consumable:
                if (item is Consumable consum)
                {
                    consum.useEffectKey = CategoryToTable(item.category)[item.id]["useEffect"].ToString();
                }
                break;
            case ItemCategory.material:
                break;
            case ItemCategory.tools:
                if (item is Tools tool)
                {
                    tool.useEffectKey = CategoryToTable(item.category)[item.id]["useEffect"].ToString();
                }
                break;
            case ItemCategory.none:
                break;
            default:
                break;
        }
        return item;
    }

    public void SetEquipStat(int index, ref EquipmentItem item)
    {
        if (!dataManager.equipStat.ContainsKey(index))
        {
            Debug.Log("id Dind't Contain in ItemFactory SetEqupStatFunction");
            return;
        }
        item.equipStats = DeepCopyStatDict(dataManager.equipStat[index]);


        List<StringKeyDatas> datas = dataManager.equipStatDatas[index];
        for(int i = 0; i < datas.Count; i++)
        {
            Dictionary<string, int> data = new Dictionary<string, int>();
            foreach(string key in datas[i].datas.Keys)
            {
                int value;
                if(int.TryParse(datas[i].datas[key].ToString(), out value))
                {
                    data.Add(key, value);
                }
                else
                {
                    Debug.Log("Data Parsing Error");
                }
            }
            item.equipStats = DeepCopyStatDict(data);
        }
    }

    bool TrySetValue<T>(Dictionary<string, object> data, string key, ref T target)
    {
        if (data.ContainsKey(key) && data[key] != null)
        {
            try
            {
                target = (T)Convert.ChangeType(data[key], typeof(T));
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to convert{key}: {ex.Message}");
                return false;
            }
        }
        return false;
    }
    bool TryConvertEnum<T>(Dictionary<string, object> data, string key, ref T target) where T : struct, Enum
    {
        if (data.ContainsKey(key) && data[key] != null)
        {

            if (Enum.TryParse(data[key].ToString(), true, out target))
            {
                return true;
            }

        }



        return false;
    }
    Dictionary<string, int> DeepCopyStatDict(Dictionary<string, int> origin)
    {
        Dictionary<string, int> newDict = new Dictionary<string, int>();
        foreach (string key in origin.Keys)
        {
            newDict.Add(key, origin[key]);


        }

        return newDict;
    }
    Dictionary<int, Dictionary<string, string>> CategoryToTable(ItemCategory category)
    {
        switch (category)
        {
            case ItemCategory.farming:
                return dataManager.farmingItemData;
            case ItemCategory.tools:
                return dataManager.toolItemData;
            case ItemCategory.equipment:
                return dataManager.equipItemData;
            case ItemCategory.consumable:
                return dataManager.consumItemData;
            case ItemCategory.material:
                return null;//dataManager.items;
            case ItemCategory.none:
                return null;
            default:
                return null;
        }

    }
}
