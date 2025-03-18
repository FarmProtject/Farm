using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
public class ToolTipPanel : UIBase,Isubject
{
    ItemBase item;
    [SerializeField]
    InventorySlot slot;
    [SerializeField]
    Image itemImage;
    [SerializeField]
    GameObject headPaenl;
    [SerializeField]
    SetStringKey headNameText;
    [SerializeField]
    SetStringKey headTypeText;
    [SerializeField]
    SetStringKey itemCountText;
    [SerializeField]
    SetStringKey maxCountText;
    [SerializeField]
    SetStringKey usingEffectText;
    [SerializeField]
    SetStringKey descText;
    [SerializeField]
    TextMeshProUGUI goldValue;
    [SerializeField]
    GameObject bodyPanelPrefab;
    [SerializeField]
    GameObject tailPanel;
    ToolTipTailPanel tailPanelSc;

    List<GameObject> bodyPanels = new List<GameObject>();
    List<IObserver> observers = new List<IObserver>();
    private void Awake()
    {
        if(headPaenl == null)
        {
            headPaenl = this.transform.GetChild(0).gameObject;
        }
        this.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        
        UpdateItemInfo();
        Debug.Log(this.transform.position);
        OnStart();
    }
    protected override void OnStart()
    {
        base.OnStart();
        Notyfy();
    }
    protected override void Start()
    {
        //OnStart();
    }
    public void UpdateItem(ItemBase item)
    {
        this.item = item;
    }

    void UpdateItemInfo()
    {
        SetBodyPael();
        if (tailPanelSc == null)
        {
            tailPanelSc = tailPanel.transform.GetComponent<ToolTipTailPanel>();
        }
        if (item != null)
        {
            string key = item.id.ToString();
            itemImage.sprite = slot.itemSprite.sprite;
            
            headNameText.SetItemKey(key);
            descText.SetItemDiscKey(key);
            headTypeText.SetTypeKey(key);
            itemCountText.SetCountKey(key);
            maxCountText.SetMaxCountKey(key);
            usingEffectText.SetEffectKey(key);
            goldValue.text = item.price.ToString();
        }
    }

    public void Attach(IObserver observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }

    public void Detach(IObserver observer)
    {
        if (observers.Contains(observer))
        {
            observers.Remove(observer);
        }
    }

    public void Notyfy()
    {
        foreach(IObserver obs in observers)
        {
            obs.Invoke();
        }
    }
    #region 상단 세팅
    #endregion
    #region 중단세팅 판별
    void SetBodyPael()
    {
        //headPaenl.transform.position = new Vector3(0, 0, 0);
        int bodyCount = 0;
        float tailPanelPos = 0;
        if (item is EquipmentItem equipItem)
        {
            Debug.Log($"item is EquipItem {equipItem.equipStats.Count}");

            if (bodyPanels.Count < equipItem.equipStats.Count)
            {
                for (int i = bodyPanels.Count; i < equipItem.equipStats.Count; i++)
                {
                    float panelPos;
                    GameObject go = Instantiate(bodyPanelPrefab);
                    go.transform.SetParent(this.gameObject.transform);
                    bodyPanels.Add(go);
                    ToolTipBodyPanel panel = go.transform.GetComponent<ToolTipBodyPanel>();
                    ToolTipHeadPanel headPanelSc = headPaenl.transform.GetComponent<ToolTipHeadPanel>();
                    //Vector3 pos = headPaenl.transform.position;
                    panelPos = headPanelSc.myHeight;
                    panelPos += panel.myHeight / 2;
                    panel.transform.position = new Vector2(0, panelPos + (panel.myHeight * i));
                    panel.Invoke();
                }
            }
        }
        else if (item is EffectItem effectItem)
        {
            Debug.Log("item is effectItem");
            if (bodyPanels.Count < 1)
            {
                GameObject go = Instantiate(bodyPanelPrefab);
                bodyPanels.Add(go);
                go.transform.SetParent(this.gameObject.transform);
                ToolTipBodyPanel panel = go.transform.GetComponent<ToolTipBodyPanel>();
                ToolTipHeadPanel headPanelSc = headPaenl.transform.GetComponent<ToolTipHeadPanel>();
                //Vector3 pos = headPaenl.transform.position;
                panel.transform.position =new Vector2(0 ,headPanelSc.myHeight);
                panel.Invoke();
            }

            
           
        }
        else
        {
            Debug.Log("Item is ItemBase");
            if (bodyPanels.Count > 1)
            {
                for(int i = 0; i < bodyPanels.Count; i++)
                {
                    bodyPanels[i].SetActive(false);
                }
            }
        }
        for(int i = 0; i < bodyPanels.Count; i++)
        {
            if (bodyPanels[i].activeSelf)
            {
                bodyCount++;
            }
        }
        tailPanelPos += headPaenl.transform.GetComponent<ToolTipHeadPanel>().myHeight;
        //tailPanelPos += bodyPanelPrefab.transform.GetComponent<ToolTipBodyPanel>().myHeight * bodyCount;
        //tailPanelPos += tailPanel.transform.GetComponent<ToolTipTailPanel>().myHeight / 2;
        SetTailPanel(tailPanelPos);
    }
    #endregion
    #region 하단패널
    void SetTailPanel(float onTop)
    {
        if(tailPanelSc == null)
        {
            tailPanelSc = tailPanel.transform.GetComponent<ToolTipTailPanel>();
        }
        //onTop += tailPanelSc.myHeight / 2;
        tailPanelSc.SetPos(0, 0, 0, onTop);
        tailPanelSc.Invoke();
    }
    #endregion
    void SetMyDirection()
    { 
        Vector2 pos = this.transform.position;
        float centerX = Screen.width / 2;
        float centerY = Screen.height / 2;
        Vector2 centerPos = new Vector2(centerX, centerY);
        //기본위치 우상단
        if (myPos.x >= centerX)
        { //해당 UI의 위치가2,4분면일경우
            if (myPos.y >= centerY)
            { // 해당 UI가 2분면일경우
                SetPosToRight();
            }
            else
            {//해당 UI가 4분면일경우

                SetPosBotRight();

            }
        }
        else
        {//해당 UI가 1,3분면일경우
            if (myPos.y >= centerY) //1분면일경우
            {
                SePosTopLeft();
            }
            else
            {//3분면일경우
                SetPosBotLeft();
            }
        }
    }
    #region 팝업위치조절 기본위치 우상단
    void SePosTopLeft()
    { // 1사분면 마우스의 우하단 위치
        Vector2 pos = new Vector2(myPos.x, myPos.y - myHeight);
        myPos = pos;
    }
    void SetPosToRight()
    { // 2사분면 마우스의 좌하단 위치
        Vector2 pos = new Vector2(myPos.x - myWidth, myPos.y - myHeight);
        myPos = pos;
    }
    void SetPosBotLeft()
    {//3사분면 마우스의 우상단 위치
        //기본위치
    }
    void SetPosBotRight()
    {//4사분면 마우스의 좌상단 위치
        Vector2 pos = new Vector2(myPos.x - myWidth, myPos.y);
        myPos = pos;
    }

    #endregion
}
