using UnityEngine;
using System;
using System.Collections.Generic;
public class FarmItem : ItemBase
{
    public string layer = "FarmArea";
    public void RayToSoil()
    {

    }


}

public class PlowingTool : FarmItem,IItemEffect
{
    

    public void function()
    {
        throw new NotImplementedException();
    }
}