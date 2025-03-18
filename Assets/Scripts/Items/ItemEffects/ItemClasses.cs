using UnityEngine;
using System;
using System.Collections.Generic;

public enum ItemType
{
    Material,
    Tool,
    Potion,
    Seed,
    Fertilizer,
    Weapon,
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
    public string useEffectKey;
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


