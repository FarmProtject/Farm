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
        TryConvertEnum(dataManager.itemDatas.datas[index], "category", ref category);
        ItemBase item;
        switch (category)
        {
            case ItemCategory.farm:
                item = new Farming();
                break;
            case ItemCategory.equipment:
                item = new EquipmentItem();
                Debug.Log("EquipItem Make!!");
                break;
            case ItemCategory.consumable:
                item = new Consumable();
                break;
            case ItemCategory.material:
                item = new MaterialItem();
                break;
            case ItemCategory.tool:
                item = new Tools();
                break;
            case ItemCategory.none:
                item = new ItemBase();
                Debug.Log("None Type Make!!");
                break;
            default:
                Debug.Log("Default Type Make!!");
                item = new ItemBase();
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

        if (TrySetValue(data.datas[index], "maxStack", ref item.maxStack))
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
        if(!(item is EquipmentItem) && item is EffectItem effectItem)
        {
            effectItem.useEffectKey = CategoryToTable(item.category)[effectItem.id]["useEffect"].ToString();
        }
        switch (item.category)
        {
            case ItemCategory.farm:
                if(item is Farming farm)
                {
                    farm.useEffectKey = CategoryToTable(item.category)[item.id]["useEffect"].ToString();
                }
                break;
            case ItemCategory.equipment:
                if (item is EquipmentItem equip)
                {
                    SetEquipStat(item.id, ref equip);
                    string slot = CategoryToTable(item.category)[item.id]["slotType"].ToString();
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
            case ItemCategory.tool:
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
        
        if (!dataManager.equipStatDatas.ContainsKey(index))
        {
            Debug.Log("id Dind't Contain in ItemFactory SetEqupStatFunction");
            return;
        }

        List<StringKeyDatas> datas = dataManager.equipStatDatas[index];//List<Dictionary<string,object>>
        Dictionary<string, int> newData = new Dictionary<string, int>();
        for (int i = 0; i < datas.Count; i++)
        {
            string statType = datas[i].datas["statType"].ToString();
            int stat = int.Parse(datas[i].datas["statValue"].ToString());
            if (!newData.ContainsKey(statType))
            {
                newData.Add(statType, stat);
            }
        }
        item.equipStats = (newData);
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
            case ItemCategory.farm:
                Debug.Log("Get farmingItemData");
                return dataManager.farmingItemData;
            case ItemCategory.tool:
                Debug.Log("Get ToolItemData");
                return dataManager.toolItemData;
                
            case ItemCategory.equipment:
                return dataManager.equipItemData;
            case ItemCategory.consumable:
                return dataManager.consumItemData;
            case ItemCategory.material:
                return null;
            case ItemCategory.none:
                return null;
            default:
                return null;
        }

    }
}
