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
    [SerializeField]
    TextMeshProUGUI itemName;
    [SerializeField]
    TextMeshProUGUI itemCount;
    public ItemBase item;
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
        item = GameManager.instance.UIManager.shopManager.buyingItem;
        SetImage();
        SetItemCount();
        SetItemName();
    }

    void SetImage()
    {
        Debug.Log("Need to Write SetImage in ShopPopUpPanel");
    }
    void SetItemCount()
    {
        itemCount.text =  item.itemCount.ToString();
        Debug.Log("Need to Write ItemCOunt in ShopPopUpPanel");
    }
    void SetItemName()
    {
        itemName.text = item.name;
        Debug.Log("Need to Write SetItemName in ShopPopUpPanel");
    }

}