using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItemScript : MonoBehaviour
{
    ItemBase item = new ItemBase();
    public int itemCount;
    void Start()
    {
        SetTestData();
    }

    void Update()
    {
        itemCount = item.itemCount;
        if (item.itemCount <= 0)
        {
            Destroy(this.gameObject, 0.5f) ;
        }
    }

    public void ItemTest()
    {

    }
    public void SetTestData()
    {

        item.id = UnityEngine.Random.Range(1, 5);
        item.maxStack = item.id;
        item.itemCount = UnityEngine.Random.Range(1, 5);
        item.name = item.id.ToString();

    }


    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Player")
        {
            Debug.Log("playerEnter!");
            GameManager.instance.playerEntity.inventory.AddinInventory(this.item);
            itemCount = item.itemCount;
            
        }
    }
}
