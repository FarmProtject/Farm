using UnityEngine;
using System;
using System.Collections.Generic;
public class FarmTile : MonoBehaviour
{
    [SerializeField] Material ableMeterial;
    [SerializeField] Material enAbleMeterial;

    [SerializeField]GameObject preViewObj;
    MeshRenderer preViewMesh;

    FarmTileType tileType;
    private void Awake()
    {
        
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
            preViewMesh = preViewObj.GetComponent<MeshRenderer>();
        }

        preViewObj.transform.rotation = Quaternion.identity;
    }
    void TurnOnPreview(FarmTileType targetType)
    {
        preViewObj.SetActive(true);
        if(tileType == targetType)
        {
            preViewMesh.material = ableMeterial;
        }
        else
        {
            preViewMesh.material = enAbleMeterial;
        }

    }
    public void SetTileType(FarmTileType type)
    {
        tileType = type;
    }
    public FarmTileType GetTileType()
    {
        return tileType;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "SkillColl")
        {
            EffectPull ep = other.transform.GetComponent<EffectPull>();
            string type = ep.targetType.ToString();
            FarmTileType targetType;
            if(Enum.TryParse(type, out targetType))
            {
                TurnOnPreview(targetType);
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
}
