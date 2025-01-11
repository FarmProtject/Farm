using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryData 
{
    [SerializeField]
    public List<ItemBase> inventory = new List<ItemBase>();
    public int maxInventory = 40;
    public InventoryItemPanel inventoryUI;
    int gold;
    
    public void OnGetItemCheck(ItemBase item)
    {
        int index = CheckSameItem(item);
        if (index == -1)//�κ��丮�� ���� �������� �ִ��� üũ
        {//���������� ����X
            if (InventorySlotCheck())//�κ��丮 ���� Ȯ��
            {
                AddNewItem(item);
            }
            else
            {
                Debug.Log("Find New Item Inventory Full");
            }
        }
        else
        {//���� ������O
            int rest = inventory[index].AddStack(item, item.itemCount);//���� �������� ���� ++

            if (rest > 0 && InventorySlotCheck())
            {//���� ������ ����,���� �ִ�ġ ����, �κ��丮�� ����O
                AddNewItem(item);
                OnGetItemCheck(item);
            }
            else if (rest>0 && !InventorySlotCheck())
            {//���������� ����, �κ��丮�� ����X
                Debug.Log("Let Rest Item");
            }
            else
            {
                Debug.Log("otherResons");
            }
        }
        EventManager.instance.OnInventoryUpdate.Invoke();
    }


    bool InventorySlotCheck()
    {
        bool CanGet = true;
        for(int i = 0; i < inventory.Count; i++)
        {
            if(inventory[i] == null)
            {
                CanGet = true;
                return CanGet;
            }
        }
        if (inventory.Count >= maxInventory)
            CanGet = false;

        return CanGet;
    }

    int CheckSameItem(ItemBase item)
    {
        //���� �� -1
        int index = -1;
        for(int i = 0; i<inventory.Count; i ++)
        {
            if (inventory[i]!=null &&
                inventory[i].id == item.id &&
                inventory[i].itemCount<inventory[i].maxStack)
            {
                return index = i;
            }
                
        }

        return index;
    }
    
    void AddNewItem(ItemBase item)
    {
        for(int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = DeepCopyItemData(item);
                inventory[i].itemCount = item.itemCount;
                item.itemCount = 0;
                return;
            }
        }
        inventory.Add(DeepCopyItemData(item));
        inventory[inventory.Count - 1].itemCount = item.itemCount;
        item.itemCount = 0;
    }


    ItemBase DeepCopyItemData(ItemBase original)
    {
        return new ItemBase
        {
            id = original.id,
            itemCount = original.itemCount,
            maxStack = original.maxStack,
            
        };
    }
    public void InventorySlotSwap(int first,int second)
    {
        Debug.Log("InventorySlotSwap");
        ItemBase temp;
        if (inventory[first] == null)
            return;
        temp = inventory[first];
        if(inventory[second]== null)
        {
            inventory[second] = inventory[first];
            inventory[first] = null;
        }
        else if (inventory[second] != null && inventory[first].id == inventory[second].id && inventory[first].itemCount<inventory[first].maxStack)
        {
            inventory[second].itemCount = inventory[first].maxStack - inventory[first].itemCount;
            inventory[first].itemCount = inventory[first].maxStack;
        }
        else if (inventory[second] != null)
        {
            inventory[first] = inventory[second];
            inventory[second] = temp;
        }
        EventManager.instance.OnInventoryUpdate.Invoke();
    }
    
}
