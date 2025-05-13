using UnityEngine;
using System;
using System.Collections.Generic;
public class CropObject : InteractableEntity,IInteractable
{
    string id;

    List<DropTable> dropTable;
    [SerializeField]FarmTile myTile;
    protected override void NPCInteract()
    {
        Harvest();
        Debug.Log("CropInteract!");

    }
    void Harvest()
    {
        GameManager.instance.itemFactory.ItemDrop(dropTable, this.transform.position);
        for(int i = 0; i < dropTable.Count; i++)
        {
            Debug.Log($"Drop Table {dropTable[i].dropItem}");
        }
        myTile.Harvest();
        dropTable.Clear();
        this.gameObject.SetActive(false);
    }
    public void SetDropTable(List<DropTable> table)
    {
        dropTable = table;
    }
    
    ItemBase ItemMake(int id)
    {
        return GameManager.instance.itemFactory.ItemMake(id);
    }
}
