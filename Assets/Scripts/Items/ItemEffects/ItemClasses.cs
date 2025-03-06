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
    consumable,
    equipment,
    harvest,
    readySoil,
    soil
}

public class EffectItem : ItemBase
{
    public string useEffectKey;
}
public class MaterialItem : ItemBase
{

}

public class ToolItem : EffectItem
{

}
public class PotionItem : EffectItem
{

}
public class SeedItem : EffectItem
{
    
}
public class FertilizerItem : EffectItem
{

}
public class WeaponItem : EffectItem
{

}
public class EquipmentItem : EffectItem
{
    public Dictionary<string, int> equipStats = new Dictionary<string, int>();
}
public class ConsumItem : EffectItem
{

}
