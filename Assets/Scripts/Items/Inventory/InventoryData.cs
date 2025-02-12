using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class InventoryData 
{
    [SerializeField]
    public List<ItemBase> inventory = new List<ItemBase>();
    public int maxInventory = 40;
    public InventoryItemPanel inventoryUI;
    int gold;
    


    public void AddinInventory(ItemBase item)
    {
        ItemBase addedItem = OnGetCountCheck(item);
        if(addedItem != item)
        {
            AddinInventory(item);
            Debug.Log("Loof!");
        }
        if ((item!= addedItem && addedItem.itemCount <=0)||(item == addedItem && addedItem.itemCount <=0))
        {
            Debug.Log("Item Count <= 0 In inventoryData AddinVentoryFcuntion");
            return;
        }
        addedItem = OnGetItemCheck(addedItem);
        if(addedItem == null)
        {
            Debug.Log("Can't Get Item! ");
            return;
        }
        if(item == addedItem)
        {
            Debug.Log("Same Script");
            AddNewItem(addedItem);
        }
        else//���� �������� �ְų� ȹ��������� ������ �ִ뽺�ú��� �������
        {
            int index = -1;
            index = CheckSameItem(addedItem);
            if (index != -1)
            {
                int prevItemCount = addedItem.itemCount;
                inventory[index].AddStack(addedItem, addedItem.itemCount);
                if(prevItemCount!= addedItem.itemCount)
                {
                    AddinInventory(addedItem);
                }
                else
                {
                    Debug.Log("Item Get Loof Stoped");
                    return;
                }
            }
            else
            {
                AddNewItem(addedItem);
                Debug.Log("More than max Stack");
            }
            
        }
        EventManager.instance.OnInventoryUpdate.Invoke();
    }
    public ItemBase OnGetCountCheck(ItemBase item)
    {
        ItemBase getItem = item ;
        if (item.itemCount > item.maxStack)
        {
            getItem = GameManager.instance.itemFactory.ItemMake(item.id);
            getItem.itemCount = getItem.maxStack;
            item.itemCount -= getItem.maxStack;
            Debug.Log($"          {item.itemCount}");
            Debug.Log($"OnGetCountCheck{getItem.itemCount}");
        }

        return getItem;
    }
    public ItemBase OnGetItemCheck(ItemBase item)
    {
        int index = CheckSameItem(item);
        if (index == -1)//�κ��丮�� ���� �������� �ִ��� üũ
        {//���������� ����X
            if (InventorySlotCheck())//�κ��丮 ���� Ȯ��
            {
                //item = OnGetCountCheck(item); //������ ����Ȯ��
                return item;
            }

            else
            {
                return null;
            }
        }
        else
        {//���� ������O
            int amount = item.itemCount;
            int rest = 0;
            amount = inventory[index].maxStack - inventory[index].itemCount;
            ItemBase getItem = GameManager.instance.itemFactory.ItemMake(item.id);
            getItem.itemCount = amount;
            rest = item.itemCount - amount;
            item.itemCount = rest;
            if (amount > 0)
            {
                Debug.Log("work!");
            }
            else
            {
                Debug.Log("InventoryDataOnGetItemCheck Error");
                return null;
            }
            return getItem;
            
        }
        
    }

    /*
    public void OnGetItemCheck(ItemBase item)
    {
        int index = CheckSameItem(item);
        if (index == -1)//�κ��丮�� ���� �������� �ִ��� üũ
        {//���������� ����X
            if (InventorySlotCheck())//�κ��丮 ���� Ȯ��
            {
                //return item; 
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
    */

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
                Debug.Log("AddItem!");
                item.itemCount = 0;
                return;
            }
        }
        inventory.Add(DeepCopyItemData(item));


        inventory[inventory.Count - 1].itemCount = item.itemCount;
        item.itemCount = 0;
        EventManager.instance.OnInventoryUpdate.Invoke();
    }


    ItemBase DeepCopyItemData(ItemBase original)
    {
        var item = GameManager.instance.itemFactory.ItemMake(original.id);
        item.itemCount = original.itemCount;
        return item;
    }
    /*
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
    */
    public void InventorySlotSwap(int first, int second)
    {
        Debug.Log("InventorySlotSwap");

        if (inventory[first] == null)
            return;

        if (inventory[second] == null)
        {
            inventory[second] = inventory[first];
            inventory[first] = null;
        }
        else if (inventory[first].id == inventory[second].id)
        {
            // ���� �������̸� ������ ��ħ
            int transferableAmount = inventory[second].maxStack - inventory[second].itemCount;
            int moveAmount = Mathf.Min(transferableAmount, inventory[first].itemCount);

            inventory[second].itemCount += moveAmount;
            inventory[first].itemCount -= moveAmount;

            if (inventory[first].itemCount == 0)
                inventory[first] = null;
        }
        else
        {
            // �ٸ� �������̸� ��ġ�� ��ȯ
            (inventory[first], inventory[second]) = (inventory[second], inventory[first]);
        }

        EventManager.instance.OnInventoryUpdate.Invoke();
    }
    public void DecreaseItemCount(ItemBase item, int count)
    {
        var targetItem = inventory.FirstOrDefault(i => i == item);
        if(targetItem != null)
        {
            if (targetItem.itemCount < count)
            {
                count = targetItem.itemCount;
            }

            targetItem.itemCount -= count;
        }
        if(targetItem.itemCount == 0)
        {
            inventory.Remove(targetItem);
        }
    }
}
