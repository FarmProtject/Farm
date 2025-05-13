using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
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

public enum TargetType
{
    none,
    Self,
    Soil,
    ReadySoil,
    Crop,
    Enemy,
    Friendly,
    Any
}
/*
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
}*/
public class EffectPull : MonoBehaviour
{
    public GameObject parentsObj;
    //public GameObject myObj;

    public TargetType targetType;
    //BoxCollider myCollider;
    ColliderDirection direction;
    ColliderInstatType collType;
    Vector3 myCenter;
    //EffectCollider eftColl;
    float verti;
    float hori;
    float height;
    Vector3 targetPos;
    Vector3 centerPos;
    Vector3 collSize = new Vector3();
    List<GameObject> targetObjs;
    List<FarmTile> previewObj = new List<FarmTile>();
    [SerializeField] LayerMask gridLayer;
    public GameObject debugobject;
    
    private void Awake()
    {
        OnAwake();
    }
    private void FixedUpdate()
    {
        transform.rotation = Quaternion.identity;
        if (CatchGridObject())
        {

        }
        else
        {
            GetMousePosition();
        }
        PositionAdjustmet();
    }
    void PositionAdjustmet()
    {
        SetDirection();
        switch (collType)
        {
            case ColliderInstatType.none:
                transform.position = targetPos;
                centerPos = targetPos;
                break;
            case ColliderInstatType.Around:
                
                AroundSet(targetPos, verti, hori, height);
                transform.position = centerPos;
                break;
            case ColliderInstatType.ToBack:
                ToBackSet(targetPos, verti, hori, height);
                transform.position = centerPos;
                break;
            default:
                break;
        }
        
    }
    Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit , 100f) )
        {
            targetPos = hit.point;
        }
        return targetPos;
    }
    bool CatchGridObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //LayerMask gridLayer = LayerMask.NameToLayer("GridObject");
        if (Physics.Raycast(ray, out RaycastHit hit,100f, gridLayer))
        {
            Debug.Log("Obj Hit!");
            IGridObject gridObject = hit.collider.GetComponent<IGridObject>();
            Debug.Log(hit.collider.name);
            if (gridObject != null)
            {
                //PositionAdjustmet();
                targetPos = gridObject.GetGridPosition();
                //previewObj = GetObjectsToBox();
                //PreviewObjOn();
                //transform.position = targetPos;
                Debug.Log("Move To Grid Position");
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    public void SetEffectInfo(ColliderInstatType collType, Vector3 targetPos, float verti, float hori, float height,TargetType targetType)
    {
        Debug.Log("CollType Set");
        this.collType = collType;
        this.targetPos = targetPos;
        this.verti = verti;
        this.hori = hori;
        this.height = height;
        this.targetType = targetType;
        PositionAdjustmet();
        PreviewObjReset();
    }
    void SetColliderSize()
    {

    }
    public void SetTargetPos(Vector3 pos)
    {
        targetPos = pos;
        Debug.Log($"Target Pos = {pos}");
    }
    void OnAwake()
    {
        if(parentsObj == null)
        {
            parentsObj = transform.GetComponentInParent<LivingEntity>().gameObject;
        }
    }
    public void SetColliderType(ColliderInstatType collType)
    {
        this.collType = collType;
    }
    public void Invoke()
    {
        //this.transform.position = targetPos;
        /*
        SetDirection();

        switch (collType)
        {
            case ColliderInstatType.none:
                Debug.Log("CollType Error!");
                break;
            case ColliderInstatType.Around:
                AroundSet(targetPos, verti, hori, height);
                break;
            case ColliderInstatType.ToBack:
                ToBackSet(targetPos, verti, hori, height);
                break;
            default:
                break;
        }*/
        //콜라이더 실제생성
        //myObj.SetActive(true);
        targetObjs = GetObjectsToBox();
    }
    #region 오브젝트 생성방식

    void AroundSet(Vector3 targetPos, float verti, float hori, float height)
    {
        Vector3 size = new Vector3();
        switch (direction)
        {
            case ColliderDirection.none:
                Debug.Log("Around , None Error");
                break;
            case ColliderDirection.Horizontal:

                size.x = hori;
                size.y = height;
                size.z = verti;
                collSize = size;
                Debug.Log("Around , Horizontal");
                //센터 포지션 보정
                centerPos = targetPos;
                break;
            case ColliderDirection.Vertical:

                size.x = verti;
                size.y = height;
                size.z = hori;
                collSize = size;
                Debug.Log("Around , Vertical");
                centerPos = targetPos;
                break;
            default:
                break;
        }
    }
    void ToBackSet(Vector3 targetPos, float verti, float hori, float height)
    {

        Vector3 size = new Vector3();
        float centerVerti;
        switch (direction)
        {
            case ColliderDirection.none:
                Debug.Log("ToBack , None Error");
                break;
            case ColliderDirection.Horizontal:

                //targetPos.z = verti;
                size.x = hori;
                size.y = height;
                size.z = verti;
                collSize = size;
                //센터위치 보정
                centerPos = targetPos;
                centerVerti = targetPos.z - (VertiDirectionSet()*((size.z-1)/ 2)); //사이즈를 절반으로 나눈 후 센터가 될 크기의 콜라이더 크기 절반을  빼면 센터좌표
                centerPos.z = centerVerti;
                Debug.Log("ToBack , Horizontal");
                break;
            case ColliderDirection.Vertical:
                //targetPos.x = verti;
                size.x = verti;
                size.y = height;
                size.z = hori;
                collSize = size;
                Debug.Log("ToBack , Vertical");
                //센터 위치 보정
                centerPos = targetPos;
                centerVerti = targetPos.x - (VertiDirectionSet() * ((size.x-1)/2));
                centerPos.x = centerVerti;
                break;
            default:
                break;
        }
        //피봇적용, 위치조정
        //myObj.transform.position = targetPos+pibotPos;
    }
    public List<GameObject> GetTargetObjs()
    {
        return targetObjs;
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
            Debug.Log("Direction is Vertical");
        }
        else
        {
            direction = ColliderDirection.Horizontal;//x축 +-생성 플레이어 프론트 벡터 방향 ↑ 
            Debug.Log("Direction is Horizontal");
        }
    }
    #region 콜라이더 생성
    List<GameObject> GetObjectsToBox()
    {
        //centerPos = targetPos;
        
        Collider[] hits = Physics.OverlapBox(centerPos, (collSize / 2)*0.9f,Quaternion.identity);
        List<GameObject> results = new List<GameObject>();
        //Instantiate(debugobject, centerPos, Quaternion.identity);
        Debug.Log($"CenterPos = {centerPos}");
        Debug.Log($"collSize = {collSize}");
        foreach (Collider go in hits)
        {
            results.Add(go.gameObject);
            Debug.Log(go.gameObject.name);
        }
        return results;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(centerPos, collSize);
    }
    #endregion
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
    public void PreviewObjOn()
    {
        Debug.Log("!!!!!!!!!!!!!!");
        FarmTileType tileType = ChangeType(targetType);
        if (tileType == FarmTileType.none)
        {
            return;
        }
        for (int i = 0; i < previewObj.Count; i++)
        {
            FarmTile tileScript = previewObj[i].transform.GetComponent<FarmTile>();
            if (tileScript != null)
            {
                tileScript.TurnOnPreview(targetType);
            }
        }
    }
    public void PreviewObjReset()
    {
        for(int i = 0; i < previewObj.Count; i++)
        {
            previewObj[i].TurnOffPreview();
        }
    }
    public FarmTileType ChangeType(TargetType target)
    {
        FarmTileType farmType = FarmTileType.none;
        string type = target.ToString();
        if(Enum.TryParse<FarmTileType>(type, out farmType))
        {
            
        }
        return farmType;
    }
    public void RemoveFarmTile(FarmTile tile)
    {
        previewObj.Remove(tile);
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
    #endregion
}
