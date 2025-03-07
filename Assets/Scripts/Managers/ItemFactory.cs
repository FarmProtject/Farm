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

        TryConvertEnum(dataManager.itemDatas.datas[index], "type", ref type);
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
            case ItemCategory.consumable:
                if (item is ConsumItem consum)
                {
                    consum.useEffectKey = CategoryToTable(item.category)[item.id]["useEffect"].ToString();
                }
                break;
            case ItemCategory.equipment:
                if (item is EquipmentItem equip)
                {
                    SetEquipStat(item.id, ref equip);
                    string slot = CategoryToTable(item.category)[item.id]["slot"].ToString();
                    equip.useEffectKey = CategoryToTable(item.category)[item.id]["useeffect"].ToString();
                    equip.slot = (EquipSlot)Enum.Parse(typeof(EquipSlot), slot);
                }
                break;
            case ItemCategory.harvest:
                break;
            case ItemCategory.readySoil:
                break;
            case ItemCategory.soil:
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
    }

    public void SetEquipData()
    {

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
            case ItemCategory.consumable:
                return dataManager.consumItemData;
            case ItemCategory.equipment:
                return dataManager.equipItemData;
            case ItemCategory.harvest:
                return dataManager.harvestItemData;
            case ItemCategory.readySoil:
                return dataManager.readySoilItemData;
            case ItemCategory.soil:
                return dataManager.soilItemData;
            default:
                return null;
        }




    }
}
