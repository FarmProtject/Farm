using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    protected RectTransform myRect;
    protected UIManager uiManager;
    [SerializeField]protected float baseWidth;
    [SerializeField] protected float baseHeight;
    public Vector2 anchorMin;
    public Vector2 anchorMax;
    public Vector2 myPos;
    public float myWidth;
    public float myHeight;

    public float onTop;
    public float onBottom;
    public float onLeft;
    public float onRight;

    private void Awake()
    {
        OnAwake();
    }

    protected virtual void Start()
    {
        OnStart();
    }
    
    protected virtual void SetupInitialField()
    {
        
    }
    protected virtual void OnAwake()
    {
        myRect = this.gameObject.transform.GetComponent<RectTransform>();
        uiManager = GameManager.instance.UIManager;
    }
    protected virtual void OnStart()
    {
        SetBaseSize();
        if (myRect == null)
        {
            myRect = this.transform.GetComponent<RectTransform>();
        }
        SetupInitialField();
        SetUISize();
        SetUIImage();
        SetPosition();
    }
    protected void SetBaseSize()
    {
        baseWidth = transform.parent.transform.GetComponent<RectTransform>().rect.width;
        baseHeight = transform.parent.transform.GetComponent<RectTransform>().rect.height;
    }
    public void GetStart()
    {
        OnStart();
    }
    public void SetLocalPosition()
    {
        
        if (myRect == null)
        {
            myRect = this.transform.GetComponent<RectTransform>();
        }
        SetBaseSize();
        SetUISize();
        myRect.localPosition = new Vector2(0, 0);
        myPos = new Vector2(0, 0);
        
        myRect.anchoredPosition = Vector2.zero;
        if (onTop != 0)
        {
            myPosOnTop();
        }
        if (onBottom != 0)
        {
            myPosOnBottom();
        }
        if (onLeft != 0)
        {
            myPosOnLeft();
        }
        if (onRight != 0)
        {
            myPosOnRight();
        }

        myRect.localPosition = myPos;
    }
    protected virtual void SetPosition()
    {
        if(myRect == null)
        {
            myRect = this.transform.GetComponent<RectTransform>();
        }
        myRect.anchoredPosition = Vector2.zero;
        if(onTop != 0)
        {
            myPosOnTop();
        }
        if(onBottom != 0)
        {
            myPosOnBottom();
        }
        if (onLeft != 0)
        {
            myPosOnLeft();
        }
        if (onRight != 0)
        {
            myPosOnRight();
        }

        myRect.anchoredPosition = myPos;
    }
    protected virtual void SetUIImage()
    {

    }
    protected virtual void SetUISize()
    {
        AnchorColculate();
    }

    protected virtual void AddOnUIManager()
    {
        if(uiManager != null)
        {
            uiManager.openUIObj.Add(this.gameObject);
        }
        
    }
    protected virtual void RemoveOnUIManager()
    {
        if(uiManager != null && uiManager.openUIObj.Contains(this.gameObject))
        {
            uiManager.openUIObj.Remove(this.gameObject);
        }
    }
    protected void AnchorColculate()
    {
        float myAnchorMinX;
        float myAnchorMaxX;
        float myAnchorMinY;
        float myAnchorMaxY;
        myRect.pivot = new Vector2(0, 0);
        myAnchorMinX = 0;
        myAnchorMinY = 0;
        if(myWidth==0 || myHeight==0 || baseWidth == 0 || baseHeight == 0)
        {
            Debug.Log($"My Size is 0 {this.gameObject.name}");
            Debug.Log($"Based width : {baseWidth} , Based Height : {baseHeight}");
            return;
        }
        myAnchorMaxX = (myWidth / baseWidth);
        myAnchorMaxY =(myHeight / baseHeight);
        anchorMin = new Vector2(myAnchorMinX,myAnchorMinY);
        anchorMax = new Vector2(myAnchorMaxX, myAnchorMaxY);

        myRect.anchorMin = anchorMin;
        myRect.anchorMax = anchorMax;
        
        myRect.sizeDelta = new Vector2(0, 0);
    }

    protected void myPosOnTop()
    {
        Vector2 myTopPos = new Vector2(0, baseHeight - onTop-myHeight);
        myPos += myTopPos;
    }
    protected void myPosOnBottom() 
    {
        Vector2 myBottomPos = new Vector2(0, onBottom);
        myPos += myBottomPos;
    }
    protected void myPosOnLeft()
    {
        Vector2 myLeftPos = new Vector2(onLeft, 0);
        myPos += myLeftPos;
    }
    protected void myPosOnRight()
    {
        Vector2 myRightPos = new Vector2(baseWidth - myWidth - onRight,0);
        myPos += myRightPos;
    }
    public void ChildsSetLocal()
    {
        foreach(UIBase childs in GetComponentsInChildren<UIBase>())
        {
            childs.SetLocalPosition();
        }
    }
}
