using UnityEngine;
using System;
using System.Collections.Generic;
public class ShopPopUpHeadPanel : UIBase,IObserver
{
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
