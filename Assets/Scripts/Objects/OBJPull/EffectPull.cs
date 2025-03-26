using UnityEngine;
using System;
using System.Collections.Generic;
enum ColliderDirection
{
    none,
    Horizontal,
    Vertical
}
enum ColliderInstatType
{
    none,
    Around,
    Front,
    ToBack
}
public class EffectPull : MonoBehaviour
{
    [SerializeField] GameObject objPreFab;
    GameObject parentsObj;
    List<GameObject> colliderObjs;
    ColliderDirection direction;
    ColliderInstatType collType;
    private void Awake()
    {
        
    }

    void OnAwake()
    {
        parentsObj = transform.parent.gameObject;
    }
    void Invoke(Vector3 targetPos, int verti, int hori, int height)
    {
        SetDirection();
        switch (collType)
        {
            case ColliderInstatType.none:
                break;
            case ColliderInstatType.Around:
                AroundSet(targetPos,verti,hori,height);
                break;
            case ColliderInstatType.Front:
                FrontSet(targetPos, verti, hori, height);
                break;
            case ColliderInstatType.ToBack:
                ToBackSet(targetPos, verti, hori, height);
                break;
            default:
                break;
        }
    }
    #region 오브젝트 생성방식
    void AroundSet(Vector3 targetPos,int verti,int hori,int height)
    {
        switch (direction)
        {
            case ColliderDirection.none:
                break;
            case ColliderDirection.Horizontal:
                for(int i = 0; i < verti; i++)
                {
                    
                    for(int j = 0; j < hori; j++)
                    {

                    }
                }
                break;
            case ColliderDirection.Vertical:
                for (int i = 0; i < verti; i++)
                {
                    for (int j = 0; j < hori; j++)
                    {

                    }
                }
                break;
            default:
                break;
        }

    }
    void ToBackSet(Vector3 targetPos, int verti, int hori, int height)
    {
        switch (direction)
        {
            case ColliderDirection.none:
                break;
            case ColliderDirection.Horizontal:
                for (int i = 0; i < verti; i++)
                {
                    Vector3 temp;
                    GameObject go = colliderObjs[i * 3];
                    temp = targetPos;
                    temp.y = targetPos.y + (i * VertiDirectionSet());
                    go.SetActive(true);
                    VertiOBjSet(temp, go);
                    for (int j = 0; j < hori; j++)
                    {
                        GameObject leftObj = colliderObjs[i + 1];
                        GameObject rightObj = colliderObjs[i + 2];
                        leftObj.SetActive(true);
                        rightObj.SetActive(true);
                        HoriObjSet(targetPos, leftObj, rightObj);
                    }
                }
                break;
            case ColliderDirection.Vertical:
                for (int i = 0; i < verti; i++)
                {
                    Vector3 temp;
                    GameObject go = colliderObjs[i * 3];
                    temp = targetPos;
                    temp.x = targetPos.x + (i * VertiDirectionSet());
                    go.SetActive(true);
                    VertiOBjSet(temp, go);
                    for (int j = 0; j < hori; j++)
                    {
                        GameObject leftObj = colliderObjs[i + 1];
                        GameObject rightObj = colliderObjs[i + 2];
                        leftObj.SetActive(true);
                        rightObj.SetActive(true);
                        HoriObjSet(targetPos, leftObj, rightObj);
                    }
                }
                break;
            default:
                break;
        }
    }
    void FrontSet(Vector3 targetPos, int verti, int hori, int height)
    {
        switch (direction)
        {
            case ColliderDirection.none:
                break;
            case ColliderDirection.Horizontal:
                break;
            case ColliderDirection.Vertical:
                break;
            default:
                break;
        }
    }
    #endregion
    void SetObjectCount(int objCount)
    {
        int enableObjCount = 0;
        if (colliderObjs.Count < objCount)
        {
            for (int i = colliderObjs.Count; i < objCount; i++)
            {
                GameObject go = Instantiate(objPreFab);
                go.transform.SetParent(this.gameObject.transform);
                colliderObjs.Add(go);
                go.SetActive(false);
            }
        }
        
        for(int i = 0; i< colliderObjs.Count; i++)
        {
            if (colliderObjs[i].activeSelf)
            {
                enableObjCount++;
            }
        }
        for(int i = 0; i < objCount; i++)
        {
            colliderObjs[i].SetActive(true);
        }
    }
    void SetDirection()
    {
        Vector3 forward = parentsObj.transform.forward;
        forward.y = 0;
        forward = forward.normalized;
        if (Math.Abs( forward.x )> Math.Abs( forward.y))
        {
            direction = ColliderDirection.Vertical;//y축 +-  플레이어 프론트 벡터 방향 ←→ 
        }
        else
        {
            direction = ColliderDirection.Horizontal;//x축 +-생성 플레이어 프론트 벡터 방향 ↑ 
        }
    }
    void HoriObjSet(Vector3 targetPos,GameObject leftTarget, GameObject rightTarget)
    { // 평행방향 콜라이더 생성
        Vector3 leftPos = Vector3.zero;
        Vector3 rightPos = Vector3.zero; 
        Vector3 tempPos;
        switch (direction)
        {
            case ColliderDirection.none:
                break;
            case ColliderDirection.Horizontal:
                tempPos = targetPos;
                tempPos.x = targetPos.x - 1;
                leftPos = tempPos;
                tempPos = targetPos;
                tempPos.x = targetPos.x + 1;
                rightPos = tempPos;
                break;
            case ColliderDirection.Vertical:
                tempPos = targetPos;
                tempPos.z = targetPos.z - 1;
                leftPos = tempPos;
                tempPos = targetPos;
                tempPos.z = targetPos.z + 1;
                rightPos = tempPos;
                break;
            default:
                break;
        }
        leftTarget.transform.position = leftPos;
        rightTarget.transform.position = rightPos;
    }
    void VertiOBjSet(Vector3 targetPos,GameObject vertObj)
    {
        vertObj.transform.position = targetPos;
    }
    int VertiDirectionSet()
    {
        int direct = 1;
        Vector3 forward = parentsObj.transform.forward;
        forward.y = 0;
        forward = forward.normalized;
        switch (direction)
        {
            case ColliderDirection.none:
                break;
            case ColliderDirection.Horizontal:
                if (forward.y > 0)
                {
                    direct = 1;
                }
                else
                {
                    direct = -1;
                }
                break;
            case ColliderDirection.Vertical:
                if (forward.x > 0)
                {
                    direct = 1;
                }
                else
                {
                    direct = -1;
                }
                break;
            default:
                break;
        }
        return direct;
    }
}
