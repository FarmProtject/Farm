using UnityEngine;
using System;
using System.Collections.Generic;
public class SeedingEffect : EffectBase
{
    public override void Apply(GameObject go)
    {
        FarmTile tile;
        if (tile = go.transform.GetComponent<FarmTile>())
        {
            if(tile.tileType == FarmTileType.ReadySoil)
            {
                int groupId = integerParameter;
                int id = (int)percentParam;
                CropData cropData = GameManager.instance.farmManager.MakeCropData(groupId, id);
                tile.SetCropData(cropData);



            }
        }
    }

    protected override EffectBase Create()
    {
        return new SeedingEffect();
    }
}
