using UnityEngine;
using System;
using System.Collections.Generic;
enum ColliderDirection
{
    none,
    Horizontal,
    Vertical
}
public enum ColliderInstatType
{
    none,
    Around,
    ToBack
}
public class LeftEffectClick : IClickAction
{
    EffectPull effectScript;
    public LeftEffectClick(EffectPull pullScript)
    {
        effectScript = pullScript;
    }
    public void Invoke()
    {
        effectScript.OnLeftClick();
    }
}
public class RightEffectClick : IClickAction
{
    EffectPull effectScript;
    public RightEffectClick(EffectPull pullScript)
    {
        effectScript = pullScript;
    }
    public void Invoke()
    {
        effectScript.OnRightClick();
    }
}
public class EffectPull : MonoBehaviour
{
    GameObject parentsObj;
    [SerializeField]GameObject myObj;
    
    BoxCollider myCollider;
    ColliderDirection direction;
    ColliderInstatType collType;
    Vector3 myCenter;
    EffectCollider eftColl;
    LeftEffectClick OnLeftClickAction;
    RightEffectClick OnRightClickAction;
    int verti;
    int hori;
    int height;
    Vector3 targetPos;
    private void Awake()
    {
        OnAwake();
    }

    public void SetEffectInfo(ColliderInstatType collType, Vector3 targetPos, int verti, int hori, int height)
    {
        this.collType = collType;
        this.targetPos = targetPos;
        this.verti = verti;
        this.hori = hori;
        this.height = height;
    }
    void OnAwake()
    {
        parentsObj = transform.parent.gameObject;
        if (myCollider == null)
        {
            myCollider = myObj.transform.GetComponent<BoxCollider>();
        }
        if(eftColl == null)
        {
            eftColl = myCollider.transform.GetComponent<EffectCollider>();
        }
    }
    void SetColliderType(ColliderInstatType collType)
    {
        this.collType = collType;
    }
    public void Invoke()
    {
        myObj.SetActive(true);
        SetColliderType(collType);
        SetDirection();
        switch (collType)
        {
            case ColliderInstatType.none:
                break;
            case ColliderInstatType.Around:
                AroundSet(targetPos, verti, hori, height);
                break;
            case ColliderInstatType.ToBack:
                ToBackSet(targetPos, verti, hori, height);
                break;
            default:
                break;
        }
    }
    #region 오브젝트 생성방식
    void AroundSet(Vector3 targetPos, int verti, int hori, int height)
    {

        Vector3 collsize = new Vector3();
        switch (direction)
        {
            case ColliderDirection.none:
                break;
            case ColliderDirection.Horizontal:
                collsize.x = hori;
                collsize.z = verti;

                myCollider.size = collsize;
                break;
            case ColliderDirection.Vertical:
                collsize.x = verti;
                collsize.z = hori;

                myCollider.size = collsize;
                
                break;
            default:
                break;
        }
        myObj.transform.position = targetPos;
    }
    void ToBackSet(Vector3 targetPos, int verti, int hori, int height)
    {
        Vector3 collSize = new Vector3();
        Vector3 pibotPos = Vector3.zero;
        float pibotx = 0f;
        float pibotz = 0f;
        switch (direction)
        {
            case ColliderDirection.none:
                break;
            case ColliderDirection.Horizontal:

                collSize.x = hori;
                collSize.z = verti;
                //피봇계산
                pibotx = hori / 2;
                pibotz = verti / 2;
                pibotPos.x = pibotx;
                pibotPos.z = pibotz * VertiDirectionSet();
                pibotPos.y = 0.5f;
                myCollider.size = collSize;
                
                break;
            case ColliderDirection.Vertical:

                collSize.z = hori;
                collSize.x = verti;
                //피봇계산
                pibotx = verti/2;
                pibotz = hori/2;
                pibotPos.x = pibotx;
                pibotPos.z = pibotz * VertiDirectionSet();
                pibotPos.y = 0.5f;
                myCollider.size = collSize;
                
                break;
            default:
                break;
        }
        //피봇적용, 위치조정
        myObj.transform.position = targetPos+pibotPos;
    }

    #endregion
    void SetDirection()
    {
        Vector3 forward = parentsObj.transform.forward;
        forward.y = 0;
        forward = forward.normalized;
        if (Math.Abs(forward.x) > Math.Abs(forward.y))
        {
            direction = ColliderDirection.Vertical;//y축 +-  플레이어 프론트 벡터 방향 ←→ 
        }
        else
        {
            direction = ColliderDirection.Horizontal;//x축 +-생성 플레이어 프론트 벡터 방향 ↑ 
        }
    }
    /*
    void HoriObjSet(Vector3 targetPos, GameObject leftTarget, GameObject rightTarget)
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
    */
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
    #region 실행부
    public void OnLeftClick()
    {

    }
    public void OnRightClick()
    {

    }
    #endregion
}
