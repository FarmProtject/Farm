using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackItem : ItemBase, IStackable
{
    public int itemCount;
    int maxStack;
    int IStackable.maxStack => maxStack;

    public int AddStack(int amount)
    {
        int overflow = 0;
        if (!CanStack(amount))
        {
            overflow = itemCount + amount - maxStack;
            itemCount = maxStack;
        }
        else
        {
            itemCount += amount;
        }
        return overflow;
    }

    public bool CanStack(int amount)
    {
        return itemCount+amount < maxStack;
    }

    public int ReturnRest(int amount)
    {
        int rest = 0;

        if(itemCount+amount > maxStack)
        {
            rest = itemCount + amount - maxStack;
        }

        return rest;
    }

}
