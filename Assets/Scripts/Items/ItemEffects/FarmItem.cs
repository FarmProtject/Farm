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

public class SoilItem : FarmItem
{

}
public class ReadySoilItem : FarmItem
{

}
public class EquipItem : ItemBase
{

}
public class HarvestItem : ItemBase
{

}
public class ConsumItem : ItemBase
{

}
public class PlowingTool : FarmItem,IItemEffect
{
    

    public void function()
    {
        throw new NotImplementedException();
    }
}