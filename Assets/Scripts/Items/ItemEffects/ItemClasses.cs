using UnityEngine;
using System;
using System.Collections.Generic;

public enum ItemType
{
    material,
    tools,
    potion,
    seed,
    fertilizer,
    weapon,
    Equipment,
    None
}
public enum ItemCategory
{
    farm,
    equipment,
    consumable,
    material,
    tool,
    none
}

public class EffectItem : ItemBase
{
    public int useEffectKey;
    public EffectBase effect;

    public void ItemInvoke(GameObject go)
    {
        effect.Apply(go);
    }
}
public class MaterialItem : ItemBase
{

}
public class Tools : EffectItem
{

}
public class Farming : EffectItem
{

}
public class Consumable : EffectItem
{

}

public class EquipmentItem : EffectItem
{
    public Dictionary<string, int> equipStats = new Dictionary<string, int>();
}


