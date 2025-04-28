using UnityEngine;
using System;
using System.Collections.Generic;
public class WateringEffect : EffectBase
{
    public override void Apply(GameObject go)
    {
        Debug.Log($"Apply!!! {go.name}");
        FarmTile targetScript = go.transform.GetComponent<FarmTile>();
        Debug.Log(targetScript);
        if (targetScript != null && targetScript.GetTileType() == FarmTileType.ReadySoil)
        {
            Debug.Log("Try Tile Type Change in HoeEffect");
            targetScript = go.transform.GetComponent<FarmTile>();
            targetScript.isWet = true;
        }
    }

    protected override EffectBase Create()
    {
        return new WateringEffect();
    }
}
