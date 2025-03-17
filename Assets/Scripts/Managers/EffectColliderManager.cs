using UnityEngine;
using System;
using System.Collections.Generic;
public class EffectColliderManager : MonoBehaviour
{

    EffectBase effect;
    GameObject myObj;
    [SerializeField] GameObject colliderPrefab;
    List<GameObject> colObjs = new List<GameObject>();
    LayerMask targetLayer;
    Camera mainCamera;

    public Vector3 GetMousePos()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 resultPos;
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 100,targetLayer))
        {
            resultPos = hit.point;

            return resultPos;
        }


        return mousePos;
    }

    public Vector3 TargetPos(Vector3 targetPos)
    {
        Vector3 resultPos = new Vector3();


        return resultPos;
    }

    public Vector3 TargetDirection(Vector3 targetPos)
    {
        Vector3 resultDirection = new Vector3();
        Vector3 start = myObj.transform.position;
        resultDirection = targetPos - start;
        resultDirection.y = 0;
        resultDirection = resultDirection.normalized;
        return resultDirection;

    }

    public Vector3 SelfTarget()
    {
        Vector3 resultPos = new Vector3();


        return resultPos;
    }

}
