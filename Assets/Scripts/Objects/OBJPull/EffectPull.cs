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
    Friendly
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

    [SerializeField] LayerMask gridLayer;
    private void Awake()
    {
        OnAwake();
    }
    private void FixedUpdate()
    {
        if (CatchGridObject())
        {

        }
        else
        {
            GetMousePosition();
        }
    }

    Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit , 100f) )
        {
            targetPos = hit.point;
            Debug.Log("Move To MousePosition");
            transform.position = targetPos;
        }
        return targetPos;
    }
    bool CatchGridObject()
    {
        Debug.Log("catchGridObject");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //LayerMask gridLayer = LayerMask.NameToLayer("GridObject");
        if (Physics.Raycast(ray, out RaycastHit hit,100f, gridLayer))
        {
            Debug.Log("Obj Hit!");
            IGridObject gridObject = hit.collider.GetComponent<IGridObject>();
            Debug.Log(hit.collider.name);
            if (gridObject != null)
            {
                targetPos = gridObject.GetGridPosition();
                transform.position = targetPos;
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
    public void SetEffectInfo(ColliderInstatType collType, Vector3 targetPos, float verti, float hori, float height)
    {
        this.collType = collType;
        this.targetPos = targetPos;
        this.verti = verti;
        this.hori = hori;
        this.height = height;
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
        this.transform.position = targetPos;
        //myObj.transform.position = Vector3.zero;
        SetColliderType(collType);
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
        }
        //�ݶ��̴� ��������
        //myObj.SetActive(true);
        targetObjs = GetObjectsToBox();
    }
    #region ������Ʈ �������

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
                //���� ������ ����
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

                targetPos.z = verti;
                size.x = hori;
                size.y = height;
                size.z = verti;
                collSize = size;
                //������ġ ����
                centerPos = targetPos;
                centerVerti = targetPos.z - (VertiDirectionSet()*(size.z / 2 -0.5f)); //����� �������� ���� �� ���Ͱ� �� ũ���� �ݶ��̴� ũ�� ������  ���� ������ǥ
                centerPos.z = centerVerti;
                Debug.Log("ToBack , Horizontal");
                break;
            case ColliderDirection.Vertical:
                targetPos.x = verti;
                size.x = verti;
                size.y = height;
                size.z = hori;
                collSize = size;
                Debug.Log("ToBack , Vertical");
                //���� ��ġ ����
                centerPos = targetPos;
                centerVerti = targetPos.x - (VertiDirectionSet() * (size.x/2 -0.5f));
                centerPos.x = centerVerti;
                break;
            default:
                break;
        }
        //�Ǻ�����, ��ġ����
        //myObj.transform.position = targetPos+pibotPos;
    }
    public List<GameObject> GetTargetObjs()
    {
        Debug.Log($"target Pos = {targetPos}");
        Debug.Log($"center Pos = {centerPos}");
        for(int i = 0; i<targetObjs.Count;i++)
        {
            Debug.Log($"TargetNames : {targetObjs[i].name}");
        }
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
            direction = ColliderDirection.Vertical;//y�� +-  �÷��̾� ����Ʈ ���� ���� ��� 
            Debug.Log("Direction is Vertical");
        }
        else
        {
            direction = ColliderDirection.Horizontal;//x�� +-���� �÷��̾� ����Ʈ ���� ���� �� 
            Debug.Log("Direction is Horizontal");
        }
    }
    #region �ݶ��̴� ����
    List<GameObject> GetObjectsToBox()
    {
        centerPos = targetPos;
        
        Collider[] hits = Physics.OverlapBox(centerPos, collSize / 2);
        List<GameObject> results = new List<GameObject>();
        Debug.Log($"CenterPos = {centerPos}");
        Debug.Log($"collSize = {collSize}");
        foreach (Collider go in hits)
        {
            SetGridPosition(go.gameObject);
            results.Add(go.gameObject);
            Debug.Log(go.gameObject.name);
        }
        //OnDrawGizmos();

        return results;
    }
    void SetGridPosition(GameObject go)
    {
        if(go.layer == LayerMask.NameToLayer("GridObject"))
        {
            this.transform.position = go.transform.position;
            targetPos = go.transform.position;
        }
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
    #region �����
    public void OnLeftClick()
    {

    }
    public void OnRightClick()
    {

    }
    #endregion
}
