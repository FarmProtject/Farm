using UnityEngine;
using System;
using System.Collections.Generic;
public class EffectCaller : MonoBehaviour
{
    Dictionary<string, EffectBase> myEffects = new Dictionary<string, EffectBase>();
    Camera mainCamera;


    


    Vector3 MouseTarget(string targetTag)
    {

        float rayCastDist = 100f;
        Vector3 returnPos;
        Vector3 mousePos = Input.mousePosition;
        Ray ray = mainCamera.ScreenPointToRay(mousePos);

        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            returnPos = hit.point;
            returnPos.y = 0;
            return returnPos;
        }
        else
        {
            returnPos = ray.GetPoint(10);
            returnPos.y = 0;
            return returnPos;   //바닥이 있을경우 카메라와 바닥 벡터크기 계산 후 해당값을 넣을 필요가 있음
        }

    }
    Vector3 VectorTarget(string targetTag)
    {
        float rayCastDist = 1.5f;
        Vector3 returnVector;
        Vector3 worldPos;
        Vector3 origin = this.gameObject.transform.position;
        Vector3 mousePos = Input.mousePosition;
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            worldPos = hit.point;
            Debug.Log("Hit TO OBJ");
        }
        else
        {
            worldPos = ray.GetPoint(10); // 바닥이 있을경우 카메라와 바닥 벡터크기 계산 후 해당값을 넣을 필요가 있음
        }

        returnVector = worldPos - origin;
        returnVector.y = 0;
        returnVector = returnVector.normalized;
        return returnVector;

    }
    Vector3 SelfTarget()
    {
        return transform.position;
    }
}
