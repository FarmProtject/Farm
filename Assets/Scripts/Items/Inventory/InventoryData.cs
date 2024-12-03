using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryData 
{
    [SerializeField]
    List<ItemBase> inventory = new List<ItemBase>();
    public int maxInventory = 40;
    public InventoryItemPanel inventoryUI;
    
    
    void SlotDataChange(ItemBase item)
    {
        int changeSlotNumber;
        
        if(item is StackItem stack)
        {
            for(int i = 0; i < inventory.Count; i++)
            {
                if(inventory[i] is StackItem inventoryStack && inventoryStack.id == stack.id)
                {
                    changeSlotNumber = i;

                    int rest = inventoryStack.AddStack(stack.itemCount);


                    
                }
            }
        }
        else
        {
            
        }
    }

    void LetRestItems(int rest,StackItem stack)
    {
        if (rest > 0)
        {
            stack.itemCount = rest;
        }
    }

    bool InventorySlotCheck()
    {
        bool CanGet = true;
        if (inventory.Count >= 40)
            CanGet = false;

        return CanGet;
    }

    int CheckSameItem(ItemBase item)
    {
        //¾øÀ» ½Ã -1
        int index = -1;
        for(int i = 0; i<inventory.Count; i ++)
        {
            if (inventory[i].id == item.id)
                index = i;
        }

        return index;
    }
    /*
    bool CheckItemAmount(StackItem invenStack, StackItem getStack)
    {
        bool moreMax = false;
        
        if(invenStack.itemCount+getStack.itemCount > invenStack.maxStack)
        {
            moreMax = true;
        }

        return moreMax;
    }
    */
    int AddNewItem(ItemBase item)
    {
        int changeSlot = inventory.Count;
        
        if (InventorySlotCheck())
        {
            inventory.Add(item);
        }

        return changeSlot;
    }

    
}
