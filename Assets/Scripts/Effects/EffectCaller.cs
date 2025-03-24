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
            return returnPos;   //�ٴ��� ������� ī�޶�� �ٴ� ����ũ�� ��� �� �ش簪�� ���� �ʿ䰡 ����
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
            worldPos = ray.GetPoint(10); // �ٴ��� ������� ī�޶�� �ٴ� ����ũ�� ��� �� �ش簪�� ���� �ʿ䰡 ����
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
