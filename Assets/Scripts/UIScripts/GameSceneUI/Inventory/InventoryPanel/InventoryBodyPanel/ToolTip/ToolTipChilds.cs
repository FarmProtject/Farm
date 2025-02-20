using UnityEngine;
using System;
using System.Collections.Generic;
public class ToolTipChilds : UIBase
{
    protected override void Start()
    {
        
    }
    protected override void OnStart()
    {
        base.OnStart();
        Debug.Log(this.gameObject.name);
    }
}
