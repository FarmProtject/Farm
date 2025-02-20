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
        //�⺻��ġ ����
        if (myPos.x >= centerX)
        { //�ش� UI�� ��ġ��2,4�и��ϰ��
            if (myPos.y >= centerY)
            { // �ش� UI�� 2�и��ϰ��
                SetPosToRight();
            }
            else
            {//�ش� UI�� 4�и��ϰ��

                SetPosBotRight();

            }
        }
        else
        {//�ش� UI�� 1,3�и��ϰ��
            if (myPos.y >= centerY) //1�и��ϰ��
            {
                SePosTopLeft();
            }
            else
            {//3�и��ϰ��
                SetPosBotLeft();
            }
        }
    }
    #region �˾���ġ���� �⺻��ġ ����
    void SePosTopLeft()
    { // 1��и� ���콺�� ���ϴ� ��ġ
        Vector2 pos = new Vector2(myPos.x, myPos.y - myHeight);
        myPos = pos;
    }
    void SetPosToRight()
    { // 2��и� ���콺�� ���ϴ� ��ġ
        Vector2 pos = new Vector2(myPos.x - myWidth, myPos.y - myHeight);
        myPos = pos;
    }
    void SetPosBotLeft()
    {//3��и� ���콺�� ���� ��ġ
        //�⺻��ġ
    }
    void SetPosBotRight()
    {//4��и� ���콺�� �»�� ��ġ
        Vector2 pos = new Vector2(myPos.x - myWidth, myPos.y);
        myPos = pos;
    }

    #endregion
}
