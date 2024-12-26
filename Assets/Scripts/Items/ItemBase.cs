using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rarity
{

}

public enum ItemType 
{ 
    None,
    Tool,
    Consum,
    Equip,
    Material
}

public class ItemBase : IStackable
{
    public int id;
    public string name;
    public Rarity rarity;
    public ItemType type;
    public string stat { get; set; }
    public string useeffect;
    public string description;
    public int price;
    public string icon;
    public string model;

    public int itemCount;
    public int maxStack;

    public bool throwable;
    int IStackable.maxStack => maxStack;

    public int AddStack(ItemBase item,int amount)
    {
        int overflow = 0;
        if (!CanStack(amount))
        {
            overflow = itemCount + amount - maxStack;
            itemCount = maxStack;
            item.itemCount = overflow;
            Debug.Log("itemCount = overflow;");
        }
        else
        {
            itemCount += amount;
            item.itemCount = 0;
            Debug.Log("itemCount = 0;");
        }
        return overflow;
    }

    public bool CanStack(int amount)
    {
        return itemCount + amount < maxStack;
    }

    public int ReturnRest(int amount)
    {
        int rest = 0;

        if (itemCount + amount > maxStack)
        {
            rest = itemCount + amount - maxStack;
        }

        return rest;
    }

}
