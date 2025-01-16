using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour
{

    public delegate void LayerDetection(string layerName);
    public event LayerDetection OnToolChange;

    FarmAreaController farmController;
    void Start()
    {
        farmController = GameObject.Find("FarmArea").transform.GetComponent<FarmAreaController>();
        OnToolChange += LayerChange;
    }

    void Update()
    {
        
    }

    public void InvokeLayerChange(string layerName)
    {
        OnToolChange.Invoke(layerName);
    }
    void LayerChange(string layerName)
    {
        farmController.rayLayer = LayerMask.NameToLayer(layerName);
    }
    
}
