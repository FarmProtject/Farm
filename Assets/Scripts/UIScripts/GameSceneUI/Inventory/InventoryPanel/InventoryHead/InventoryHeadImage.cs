using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHeadImage : UIBase, IObserver
{
    [SerializeField]InventoryHeadPanel invenHead;
    
    private void Awake()
    {
        OnAwake();
    }
    protected override void Start()
    {
        
    }
    protected override void OnStart()
    {
        base.OnStart();
    }
    protected override void OnAwake()
    {
        base.OnAwake();
        invenHead = GameObject.Find("InventoryHeadPanel").transform.GetComponent<InventoryHeadPanel>();
        invenHead.Attach(this);
    }
    public void Invoke()
    {
        OnStart();
    }
}
