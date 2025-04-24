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
            effectItem.useEffectKey = (int)CategoryToTable(item.category)[effectItem.id]["useEffect"];
            
        }
        switch (item.category)
        {
            case ItemCategory.farm:
                if(item is Farming farm)
                {
                    farm.useEffectKey = (int)CategoryToTable(item.category)[item.id]["useEffect"];
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
                    consum.useEffectKey = (int)CategoryToTable(item.category)[item.id]["useEffect"];
                }
                break;
            case ItemCategory.material:
                break;
            case ItemCategory.tool:
                if (item is Tools tool)
                {
                    tool.useEffectKey = (int)CategoryToTable(item.category)[item.id]["useEffect"];
                }
                break;
            case ItemCategory.none:
                break;
            default:
                break;
        }
        if(item is EffectItem eft && !(item is EquipmentItem))
        {
            int id = (int)CategoryToTable(eft.category)[eft.id]["useEffect"];
            string effectName= null;
            if (dataManager.effectData.ContainsKey(id))
            {
                effectName = dataManager.effectData[id]["functionName"].ToString();
            }
            else
            {
                Debug.Log("NonData Effect");
                return item;
            }
            //string effectName = dataManager.effectData[id]["functionName"].ToString();
            if (effectName != null)
            {
                eft.effect = dataManager.effectBases[effectName].GetNewScript();
            }
            SetEffectData(eft.effect, id);
        }
        return item;
    }
    #region 이펙트데이터 
    void SetEffectData(EffectBase target, int id)
    {
        if (dataManager.effectData.ContainsKey(id))
        {
            Dictionary<string, object> data = dataManager.effectData[id];
            TrySetValue(data, "verti", ref target.colliderVert);
            TrySetValue(data, "hori", ref target.colliderHori);
            TrySetValue(data, "height", ref target.colliderHeight);
            TrySetValue(data, "functionName", ref target.functionName);
            TryConvertEnum(data, "targetType", ref target.target);
            TryConvertEnum(data, "collType", ref target.collType);
            TrySetValue(data, "integerParameter", ref target.integerParameter);
            TrySetValue(data, "percentParam", ref target.percentParam);
            Debug.Log($"Collider Type {target.collType}");
        }
        
    }
    #endregion
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

    public bool TrySetValue<T>(Dictionary<string, object> data, string key, ref T target)
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
    Dictionary<int, Dictionary<string, object>> CategoryToTable(ItemCategory category)
    {
        switch (category)
        {
            case ItemCategory.farm:
                return dataManager.farmingItemData;
            case ItemCategory.tool:
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
