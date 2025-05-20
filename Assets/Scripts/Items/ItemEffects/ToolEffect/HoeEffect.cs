using UnityEngine;
using System;
using System.Collections.Generic;
[AutoRegisterEffect]
public class HoeEffect : EffectBase
{
    Material readySoilMat;

    void OnAwake()
    {

    }
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
            targetScript.TurnOnPreview(target);
        }
    }
    void ChangeMesh(FarmTile farm)
    {
        if(readySoilMat == null)
        {
            readySoilMat = GameManager.instance.textureManager.readySoilMat;
        }
        farm.SetMaterial(readySoilMat);

    }
    protected override EffectBase Create()
    {
        return new HoeEffect();
    }
}
