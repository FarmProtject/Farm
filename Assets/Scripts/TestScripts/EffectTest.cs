using UnityEngine;
using System;
using System.Collections.Generic;
public class EffectTest : MonoBehaviour
{
    [SerializeField]Grid getGrid;
    [SerializeField] GameObject prebObj;
    [SerializeField] LayerMask activeLayer;
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject playerObj;
    private void Start()
    {
        
    }
    private void Update()
    {
        OnFarmPlace();
    }

    void OnFarmPlace()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        
        if (Physics.Raycast(ray,out hit,100,activeLayer))
        {
            Vector3 directionVector;
            Vector3 targetVector;
            directionVector = hit.point - playerObj.transform.position;
            directionVector.y = 0;
            directionVector = directionVector.normalized;
            targetVector = playerObj.transform.position + directionVector;
            prebObj.transform.localPosition = getGrid.WorldToCell(targetVector);
        }

    }
    void OnMouseMove()
    {
        Vector3 mousePos = Input.mousePosition;
    }
}
