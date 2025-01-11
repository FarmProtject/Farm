using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldImage : UIBase,IObserver
{
    InventoryTailPanel inventoryTail;
    public void Invoke()
    {
        OnStart();
    }
    private void Awake()
    {
        inventoryTail = GameObject.Find("InvetoryTailPanel").transform.GetComponent<InventoryTailPanel>();
        inventoryTail.Attach(this);
    }
    protected override void Start()
    {
        
    }
    protected override void OnStart()
    {
        base.OnStart();
    }
}
