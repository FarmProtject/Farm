using UnityEngine;
using System;
using System.Collections.Generic;
[AutoRegisterEffect]
public class HoeEffect : EffectBase
{
    public override void Apply(GameObject go)
    {
        FarmTile targetScript = go.transform.GetComponent<FarmTile>();
        if (targetScript!=null && targetScript.GetTileType() == FarmTileType.Soil)
        {
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
