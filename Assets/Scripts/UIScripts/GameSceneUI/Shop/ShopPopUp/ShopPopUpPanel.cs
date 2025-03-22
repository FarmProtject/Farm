using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
public class ShopPopUpPanel : UIBase, Isubject
{
    List<IObserver> observers = new List<IObserver>();
    [SerializeField]
    Image ItemImage;
    [SerializeField] SetStringKey itemName;
    [SerializeField] SetStringKey sumValue;
    [SerializeField] SetStringKey eachPrice;
    [SerializeField] SetStringKey confirmText;
    [SerializeField] SetStringKey cancleText;
    [SerializeField]
    //TextMeshProUGUI itemCount;
    public ItemBase item;
    [SerializeField]TMP_InputField popUpInputPanel;
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

    protected override void Start()
    {
        base.Start();
        
    }
    protected override void OnStart()
    {
        base.OnStart();
        foreach(var obs in this.transform.GetComponentsInChildren<IObserver>())
        {
            observers.Add(obs);
        }
        Notyfy();
        this.gameObject.SetActive(false);
    }
    
    public void UpdateMyData()
    {
        item = GameManager.instance.UIManager.shopManager.item;
        SetImage();
        SetItemCount();
        SetItemName();
    }

    void SetImage()
    {
        Debug.Log("Need to Write SetImage in ShopPopUpPanel");
    }
    public void SetItemCount()
    {
        if(item == null)
        {
            Debug.Log("item is Null");
            return;
        }
        /*
        if (popUpInputPanel.text != null)
        {
            if (int.TryParse(popUpInputPanel.text, out item.itemCount))
            {
                itemCount.text = item.itemCount.ToString();
            }
        }
        else
        {
            itemCount.text = item.itemCount.ToString();
        }
        */
        sumValue.SetStringText();
        eachPrice.SetStringText();
        sumValue.ValueTextReplace((item.itemCount*item.price).ToString());
        eachPrice.ValueTextReplace(item.price.ToString());
        sumValue.TextToStringText();
        eachPrice.TextToStringText();
        cancleText.SetStringText();
        cancleText.TextToStringText();
        confirmText.SetCommonKey(GameManager.instance.shopManager.shopState.ToString());
        confirmText.SetStringText();
        confirmText.TextToStringText();
    }
    void SetItemName()
    {
        if (item == null)
        {
            Debug.Log("item is Null");
            return;
        }
        itemName.SetItemKey(item.id.ToString());
        itemName.UpdateMyText();
        Debug.Log("Need to Write SetItemName in ShopPopUpPanel");
    }

}