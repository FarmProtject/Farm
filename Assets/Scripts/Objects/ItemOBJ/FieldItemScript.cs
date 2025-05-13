using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItemScript : MonoBehaviour
{
    ItemBase item;
    public int itemCount;
    void Start()
    {
    }



    public void SetItemData(ItemBase data)
    {
        item = data;
        itemCount = item.itemCount;
    }
    public void GetItem()
    {
        GameManager.instance.playerEntity.inventory.AddinInventory(this.item);
        itemCount = item.itemCount;
        if (itemCount <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Player" && itemCount>1)
        {
            Debug.Log("playerEnter!");
            GetItem();
        }
    }
}
