using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopItemSlot : UIBase,IObserver,Isubject
{
    ShopBodyPanel shopBody;
    public ItemBase itemData;
    Image itemImage;
    TextMeshProUGUI ItemName;
    TextMeshProUGUI itemPrice;

    List<IObserver> observers = new List<IObserver>();
    private void Awake()
    {
        OnAwake();
        shopBody = GameObject.Find("ShopBodyPanel").transform.GetComponent<ShopBodyPanel>();
        shopBody.Attach(this);
        baseWidth = shopBody.gameObject.GetComponent<RectTransform>().rect.width;
        baseHeight = shopBody.gameObject.GetComponent<RectTransform>().rect.height;
    }
    private void OnEnable()
    {
        shopBody.Attach(this);
    }
    private void OnDisable()
    {
        shopBody.Detach(this);
    }
    protected override void Start()
    {
        
    }
    protected override void OnStart()
    {
        base.OnStart();
        Notyfy();
    }

    void ObserverAdd()
    {
        foreach(IObserver obs in transform.GetComponentsInChildren<IObserver>())
        {
            Attach(obs);
        }
    }


    public void Invoke()
    {
        OnStart();
        Debug.Log("shopItemPanel Invoke!");
        Debug.Log($"ShopItemPanel {baseWidth}");
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
        if (observers == null)
        {
            Debug.Log("observer Null");
            return;
        }
            
        foreach(var obs in observers)
        {
            obs.Invoke();
        }
    }
}
