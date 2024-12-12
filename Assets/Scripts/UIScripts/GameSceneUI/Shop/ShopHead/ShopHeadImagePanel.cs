using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopHeadImagePanel : UIBase,IObserver
{
    ShopHeadPanel shopHead;
    Image shopImage;
    protected override void OnAwake()
    {
        base.OnAwake();
        shopHead = transform.parent.GetComponent<ShopHeadPanel>();
        shopHead.Attach(this);
    }
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

    public void Invoke()
    {
        OnStart();
    }
}
