using UnityEngine;
using System;
using System.Collections.Generic;
public class FarmTile : MonoBehaviour,IGridObject
{
    [SerializeField] Material ableMeterial;
    [SerializeField] Material enAbleMeterial;

    [SerializeField]GameObject preViewObj;
    [SerializeField] GameObject farmObj;
    MeshRenderer preViewRender;

    public FarmTileType tileType;
    private void Awake()
    {
        SetUpPreview();
        tileType = FarmTileType.Soil;
    }
    void OnAwake()
    {
        
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
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "SkillColl")
        {
            //EffectCollider ec = other.transform.GetComponent<EffectCollider>();
            EffectPull ep = other.GetComponent<EffectPull>();
            string type = ep.targetType.ToString();
            FarmTileType targetType;
            if(Enum.TryParse(type, out targetType))
            {
                TurnOnPreview(targetType);
                preViewObj.SetActive(true);
            }
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "SkillColl")
        {
            preViewObj.SetActive(false);
        }
        
    }

    public Vector3 GetGridPosition()
    {
        return transform.position;
    }
}
