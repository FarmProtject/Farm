using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryItemPanel : UIBase
{
    GameObject headerBar;
    GridLayoutGroup myGrid;
    List<InventorySlot> inventorySlots = new List<InventorySlot>();
    protected override void Start()
    {
        base.Start();
        headerBar = GameObject.Find("InventoryHeader");
        SetUISize();
        myGrid = transform.GetComponent<GridLayoutGroup>();
        SetGridCellSize();
        GetGridCells();
    }
    protected override void SetUISize()
    {
        myRect.anchorMin = new Vector2(0f,0f);
        myRect.anchorMax = new Vector2(0.85f, 1);
        myRect.sizeDelta = new Vector2(-40, -40);
    }

    protected override void SetPosition()
    {
        myRect.pivot = new Vector2(0.5f, 0.5f);
        myRect.anchoredPosition = new Vector2(0, 0);
    }

    void SetGridCellSize()
    {
        myGrid.cellSize = new Vector2(myRect.rect.width / 10, myRect.rect.width / 10);
    }

    void GetGridCells()
    {
        int i = 0;
        foreach(InventorySlot slots in GetComponentsInChildren<InventorySlot>())
        {
            inventorySlots.Add(slots);
        }
    }
}
