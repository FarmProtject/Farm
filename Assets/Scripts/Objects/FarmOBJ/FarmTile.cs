using UnityEngine;
using System;
using System.Collections.Generic;
public class FarmTile : MonoBehaviour,IGridObject,IDayTickable
{
    MeshRenderer preViewRender;
    [SerializeField] Material ableMeterial;
    [SerializeField] Material enAbleMeterial;

    [SerializeField]GameObject preViewObj;
    [SerializeField] GameObject cropObj;

    CropData cropData;

    MeshRenderer cropMaterial;
    MeshFilter cropMesh;

    public FarmTileType tileType;

    public bool isWet;
    private void Awake()
    {
        OnAwake();
    }
    void OnAwake()
    {
        SetUpPreview();
        SetUpCropObj();
        tileType = FarmTileType.Soil;
        GameManager.instance.dayManager.Resister(this);
    }

    void SetUpPreview()
    {
        if(preViewObj == null)
        {
            Debug.Log("previeObj null");
            return;
        }
        if (preViewObj != null)
        {
            preViewRender = preViewObj.GetComponent<MeshRenderer>();
        }

        preViewObj.transform.rotation = Quaternion.identity;
    }
    public void SetCropData(CropData crop)
    {
        cropData = crop;
    }
    public CropData GetCropData()
    {
        return cropData;
    }
    void SetUpCropObj()
    {
        //cropData = cropObj.transform.GetComponent<CropData>();
        cropMaterial = cropObj.transform.GetComponent<MeshRenderer>();
        cropMesh = cropObj.transform.GetComponent<MeshFilter>();
    }

    void TurnOnPreview(FarmTileType targetType)
    {
        preViewObj.SetActive(true);
        if(tileType == targetType)
        {
            preViewRender.material = ableMeterial;
        }
        else
        {
            preViewRender.material = enAbleMeterial;
        }

    }
    public void SetTileType(FarmTileType type)
    {
        Debug.Log($"Tile Type Change {tileType} to {type}");

        tileType = type;
        Debug.Log(transform.position);
        Debug.Log($" Crreunt Tile Tpye {tileType}");
    }
    public FarmTileType GetTileType()
    {
        return tileType;
    }
    

    public Vector3 GetGridPosition()
    {
        return transform.position;
    }


    public int GetCropId()
    {
        return cropData.id;
    }

    public void SetCropMaterial(Material material)
    {
        cropMaterial.material = material;
        cropObj.transform.rotation = Quaternion.identity;
    }

    public void SetCropTexture(Mesh texture)
    {

        cropMesh.mesh = texture;

    }

    public void SetMyTextures()
    {
        Mesh mesh = GameManager.instance.bundleManager.LoadMeshBundle(cropData.id);
        Material material = GameManager.instance.bundleManager.LoadMaterial(cropData.id);
        SetCropMaterial(material);
        SetCropTexture(mesh);
        if (!cropObj.activeSelf)
        {
            cropObj.SetActive(true);
        }
    }


    public void Grow()
    {
        cropData.myTime++;

        if (cropData.myTime >= cropData.reqTime)
        {
            float leftTime = cropData.myTime - cropData.reqTime;
            CropLevelUp();
            cropData.myTime = leftTime;
        }
    }

    public void CropLevelUp()
    {
        if (cropData.nextLevel != 0)
        {
            int level = cropData.nextLevel;
            int groupId = cropData.groudId;
            int id = (int)GameManager.instance.dataManager.harvestToLevel[groupId.ToString()][level.ToString()].datas["id"];

            cropData = GameManager.instance.farmManager.MakeCropData(groupId, id);
            SetMyTextures();
        }
        //CropData cropData = GameManager.instance.farmManager.MakeCropData(groupId, id);
    }

    public void DayPassed()
    {
        if (cropData != null)
        {
            Grow();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SkillColl")
        {
            preViewObj.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "SkillColl")
        {
            preViewObj.SetActive(false);
        }
    }
}
