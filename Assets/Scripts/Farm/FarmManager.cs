using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FarmTileType
{
    none,
    Soil,
    ReadySoil,
    Crop
}

public class FarmManager : MonoBehaviour
{
    FarmAreaController farmController;
    public Dictionary<string, Dictionary<string, StringKeyDatas>> harvestData;
    public Dictionary<string, Dictionary<string, StringKeyDatas>> cropToLevel;
    ItemFactory factory;
    private void Awake()
    {
        OnAwake();
    }
    void Start()
    {
        
    }
    void OnAwake()
    {
        farmController = GameObject.Find("FarmArea").transform.GetComponent<FarmAreaController>();
        harvestData = GameManager.instance.dataManager.harvestData;
        cropToLevel = GameManager.instance.dataManager.harvestToLevel;
        factory = GameManager.instance.itemFactory;
    
    }
    void Update()
    {
        
    }
    public CropData MakeCropData(int groupid,int id)
    {
        CropData newData = new CropData();
        string groupKey = groupid.ToString();
        string idKey = id.ToString();
        newData.groudId = groupid;
        newData.id = id;
        factory.TrySetValue(harvestData[groupKey][idKey].datas, "name", ref newData.name);
        factory.TrySetValue(harvestData[groupKey][idKey].datas, "level", ref newData.level);
        factory.TrySetValue(harvestData[groupKey][idKey].datas, "nextlevel", ref newData.nextLevel);
        factory.TrySetValue(harvestData[groupKey][idKey].datas, "reqTime", ref newData.reqTime);
        factory.TrySetValue(harvestData[groupKey][idKey].datas, "hp", ref newData.hp);
        factory.TrySetValue(harvestData[groupKey][idKey].datas, "image", ref newData.Image);
        factory.TrySetValue(harvestData[groupKey][idKey].datas, "model", ref newData.model);
        factory.TrySetValue(harvestData[groupKey][idKey].datas, "texture", ref newData.texture);
        factory.TrySetValue(harvestData[groupKey][idKey].datas, "dropID", ref newData.dropId);
        return newData;
    }
}
