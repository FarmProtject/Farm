using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryItemPanel : UIBase,IObserver,Isubject
{
    GameObject headerBar;
    GridLayoutGroup myGrid;
    List<InventorySlot> inventorySlots = new List<InventorySlot>();
    InventoryPanel invenPanel;
    List<IObserver> observers = new List<IObserver>();
    public int cellCount = 5;
    private void Awake()
    {
        OnAwake();
        invenPanel = GameObject.Find("InventoryPanel").transform.GetComponent<InventoryPanel>();
        invenPanel.Attach(this);
        myGrid = transform.GetComponent<GridLayoutGroup>();
    }

    protected override void Start()
    {
        
        
    }
    protected override void OnStart()
    {
        base.OnStart();
        Notyfy();
        Debug.Log("ItemPanel Onstart");
        SetGridCellSize();
        GetGridCells();
    }

    void SetGridCellSize()
    {
        float spacex = myGrid.spacing.x * (cellCount-1);
        float spacey = myGrid.spacing.y * (cellCount-1);
        myGrid.cellSize = new Vector2((myRect.rect.width-spacex) / cellCount, (myRect.rect.width-spacey) / cellCount);
    }

    void GetGridCells()
    {
        int i = 0;
        foreach(InventorySlot slots in GetComponentsInChildren<InventorySlot>())
        {
            inventorySlots.Add(slots);
            slots.slotNumber = i;
            i++;
        }
    }

    public void Invoke()
    {
        OnStart();
    }

    public void Attach(IObserver observer)
    {
        observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void Notyfy()
    {
        Debug.Log("ItemPanel Notify");
        foreach(var obser in observers)
        {
            obser.Invoke();
        }
    }
}
