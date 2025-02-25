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

public class MaterialItem : ItemBase
{

}

public class ToolItem : ItemBase
{

}
public class PotionItem : ItemBase
{

}
public class SeedItem : ItemBase
{
    
}
public class FertilizerItem : ItemBase
{

}
public class WeaponItem : ItemBase
{

}
public class EquipmentItem : ItemBase
{
    public Dictionary<string, int> equipStats = new Dictionary<string, int>();
}
