using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rarity
{

}

public enum ItemType 
{ 

}

public class ItemBase : IStackable
{
    public int id { get; set; }
    public string name { get; set; }
    public Rarity rarity { get; set; }
    public string stat { get; set; }
    public string skill { get; set; }
    public string desc { get; set; }
    public int price { get; set; }
    public string icon { get; set; }
    public string model { get; set; }

    public int itemCount;
    public int maxStack;
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
