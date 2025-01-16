using UnityEngine;
using System;
using System.Collections.Generic;
public class ShopPopUpBodyPanel : UIBase,IObserver
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
