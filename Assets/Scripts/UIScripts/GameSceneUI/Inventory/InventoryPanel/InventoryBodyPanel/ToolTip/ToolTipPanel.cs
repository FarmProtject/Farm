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
    GameObject bodyPanel;
    [SerializeField]
    GameObject tailPanel;

    List<IObserver> observers = new List<IObserver>();
    private void Awake()
    {
        this.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        UpdateItemInfo();
        Debug.Log(this.transform.position);
    }
    protected override void OnStart()
    {
        base.OnStart();
        Notyfy();
    }
    protected override void Start()
    {
        OnStart();
    }
    void UpdateItem()
    {
        item = slot.GetItem();
    }

    void UpdateItemInfo()
    {
        UpdateItem();
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
