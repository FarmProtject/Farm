using UnityEngine;
using System;
using System.Collections.Generic;
[AutoRegisterEffect]
public class HoeEffect : EffectBase
{
    public override void Apply(GameObject go)
    {
        Debug.Log($"Apply!!! {go.name}");
        FarmTile targetScript = go.transform.GetComponent<FarmTile>();
        Debug.Log(targetScript);
        if (targetScript!=null && targetScript.GetTileType() == FarmTileType.Soil)
        {
            Debug.Log("Try Tile Type Change in HoeEffect");
            targetScript = go.transform.GetComponent<FarmTile>();
            ChangeMesh(targetScript);
            targetScript.SetTileType(FarmTileType.ReadySoil);
        }
    }
    void ChangeMesh(FarmTile farm)
    {
        Debug.Log($"Cahnge Mesh{farm.gameObject.name}");
    }
    protected override EffectBase Create()
    {
        return new HoeEffect();
    }
}
