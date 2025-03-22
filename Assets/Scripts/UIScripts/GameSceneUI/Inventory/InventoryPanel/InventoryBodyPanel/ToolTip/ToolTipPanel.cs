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
        this.transform.SetAsLastSibling();
    }
    private void OnEnable()
    {
        if(item == null)
        {
            this.gameObject.SetActive(false);
            return;
        }
        UpdateItemInfo();
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
    public void PanelUpdate(ItemBase item)
    {
        UpdateItem(item);
        UpdateItemInfo();
    }
    public void UpdateItem(ItemBase item)
    {
        this.item = item;
    }

    void UpdateItemInfo()
    {
        if(item == null)
        {
            return;
        }
        ActiveFalseBodys();
        SetBodyPael();
        HeadPanelStringSet();
        if (tailPanelSc == null)
        {
            tailPanelSc = tailPanel.transform.GetComponent<ToolTipTailPanel>();
        }
        if (item != null)
        {
            itemImage.sprite = slot.itemSprite.sprite;
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
    void HeadPanelStringSet()
    {
        headNameText.SetItemKey(item.id.ToString());
        GetItemType();
        itemCountText.SetCommonKey("count");
        maxCountText.SetCommonKey("maxstack");
        itemCountText.SetStringText();
        string countText = itemCountText.ValueTextReplace(item.itemCount.ToString());
        
        maxCountText.SetStringText();
        string maxText = maxCountText.ValueTextReplace( item.maxStack.ToString());

        itemCountText.SetMyText(countText);
        maxCountText.SetMyText(maxText);

        headNameText.EnableFuction();
        headTypeText.EnableFuction();

        //itemCountText.TextToStringText();
        //maxCountText.TextToStringText();
    }

    void GetItemType()
    {
        if(item is EquipmentItem equipItem)
        {
            headTypeText.SetTypeKey(item.slot.ToString());
        }
        else
        {
            headTypeText.SetTypeKey(item.category.ToString());
        }
    }
    #endregion
    #region 중단세팅 판별
    void SetBodyPael()
    {
        //headPaenl.transform.position = new Vector3(0, 0, 0);
        int bodyCount = 0;
        float tailPanelPos = 0;
        int activedBody = 0 ;
        if (item is EquipmentItem equipItem)
        {
            Debug.Log($"item is EquipItem {equipItem.equipStats.Count}");
            foreach(string key in equipItem.equipStats.Keys)
            {
                Debug.Log($" StatKey : {key}   StatValue {equipItem.equipStats[key]}" );
            }
            if (bodyPanels.Count < equipItem.equipStats.Count)
            {
                for (int i = bodyPanels.Count; i < equipItem.equipStats.Count; i++)
                {
                    float panelPos=0;
                    GameObject go = Instantiate(bodyPanelPrefab);
                    go.transform.SetParent(this.gameObject.transform);
                    bodyPanels.Add(go);
                    ToolTipBodyPanel panel = go.transform.GetComponent<ToolTipBodyPanel>();
                    ToolTipHeadPanel headPanelSc = headPaenl.transform.GetComponent<ToolTipHeadPanel>();
                    //Vector3 pos = headPaenl.transform.position;
                    panelPos = headPanelSc.myHeight;
                    panelPos += panel.myHeight;
                    panel.onTop = panelPos + (panel.myHeight * (equipItem.equipStats.Count-i));
                }
            }
            else
            {
                for(int i = 0; i < equipItem.equipStats.Count; i++)
                {
                    float panelPos = 0;
                    bodyPanels[i].SetActive(true);
                    ToolTipBodyPanel panel = bodyPanels[i].transform.GetComponent<ToolTipBodyPanel>();
                    ToolTipHeadPanel headPanelSc = headPaenl.transform.GetComponent<ToolTipHeadPanel>();
                    //Vector3 pos = headPaenl.transform.position;
                    panelPos = headPanelSc.myHeight;
                    panelPos += panel.myHeight;
                    panel.onTop = panelPos + (panel.myHeight * (equipItem.equipStats.Count - i));
                }
            }
        }
        else if (!(item is EquipmentItem) && (item is EffectItem effectItem))
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
            }
            else
            {
                bodyPanels[0].SetActive(true);
                ToolTipBodyPanel panel = bodyPanels[0].transform.GetComponent<ToolTipBodyPanel>();
                ToolTipHeadPanel headPanelSc = headPaenl.transform.GetComponent<ToolTipHeadPanel>();
                panel.transform.position = new Vector2(0, headPanelSc.myHeight);
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
        
        for(int i =0; i < bodyPanels.Count; i++)
        {
            if (bodyPanels[i].gameObject.activeSelf)
            {
                activedBody++;
                bodyPanels[i].transform.GetComponent<ToolTipBodyPanel>().Invoke();
            }
        }

        tailPanelPos += headPaenl.transform.GetComponent<ToolTipHeadPanel>().myHeight;
        tailPanelPos += activedBody * bodyPanelPrefab.GetComponent<ToolTipBodyPanel>().myHeight;
        BodyPanelInfoUpdate();
        SetTailPanel(tailPanelPos);
    }

    void BodyPanelInfoUpdate()
    {
        if (item is EquipmentItem equipitem)
        {
            int index = 0;
            foreach(string key in equipitem.equipStats.Keys)
            {
                bodyPanels[index].SetActive(true);
                SetStringKey stringKey = bodyPanels[index].transform.GetComponent<ToolTipBodyPanel>().myStringKey;
                if(stringKey == null)
                {
                    Debug.Log("stringKey SC is Null!");
                    return;
                }
                Debug.Log($"  Key : {key} Value : {equipitem.equipStats[key]}");
                stringKey.SetStatKey(key,equipitem.equipStats[key].ToString());
                stringKey.UpdateMyText();
                //stringKey.gameObject.SetActive(true);
                index++;
                
            }

        }
        else if(item is EffectItem effectItem)
        {
            bodyPanels[0].SetActive(true);
            SetStringKey stringKey = bodyPanels[0].transform.GetComponent<ToolTipBodyPanel>().myStringKey;
            stringKey.SetEffectKey(effectItem.useEffectKey);
            stringKey.UpdateMyText(); ;

        }
        
    }
    void ActiveFalseBodys()
    {//바디패널 비활성화
        for(int i = 0; i < bodyPanels.Count; i++)
        {
            bodyPanels[i].SetActive(false);
        }
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
        tailPanelSc.myDscString.SetItemDiscKey(item.id.ToString());
        tailPanelSc.myDscString.UpdateMyText();
        tailPanelSc.myGoldText.text = item.price.ToString();
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
