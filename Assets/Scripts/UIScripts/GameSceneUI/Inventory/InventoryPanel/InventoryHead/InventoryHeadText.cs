using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHeadText : UIBase, IObserver
{
    [SerializeField] InventoryHeadPanel invenHead;
    private void Awake()
    {
        OnAwake();
        invenHead = GameObject.Find("InventoryHeadPanel").transform.GetComponent<InventoryHeadPanel>();
        invenHead.Attach(this);
    }
    protected override void OnAwake()
    {
        
        base.OnAwake();

    }
    protected override void OnStart()
    {
        base.OnStart();
    }
    protected override void Start()
    {
        
    }
    public void Invoke()
    {
        OnStart();
    }
}
