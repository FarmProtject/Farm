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
    [SerializeField]
    TextMeshProUGUI ItemName;
    [SerializeField]
    TextMeshProUGUI itemPrice;
    [SerializeField]
    Button myButton;
    public SetStringKey stringKey;

    List<IObserver> observers = new List<IObserver>();
    #region lifeCycle
    private void Awake()
    {
        OnAwake();
        shopBody = GameObject.Find("ShopBodyPanel").transform.GetComponent<ShopBodyPanel>();
        shopBody.Attach(this);
        baseWidth = shopBody.gameObject.GetComponent<RectTransform>().rect.width;
        baseHeight = shopBody.gameObject.GetComponent<RectTransform>().rect.height;
        myButton = transform.GetComponent<Button>();
        stringKey = ItemName.gameObject.transform.GetComponent<SetStringKey>();
        AddButtonFunction();
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
    #endregion
    #region observer pattern
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
    #endregion
    #region Button
    void AddButtonFunction()
    {
        myButton.onClick.AddListener(ButtonFunction);
    }

    void ButtonFunction()
    {
        GameManager.instance.UIManager.shopManager.SetBuyingItem(itemData);
        GameManager.instance.UIManager.shopManager.shopState = ShopState.buy;
        GameManager.instance.UIManager.ShopPopUpOpen();
        GameManager.instance.UIManager.shopManager.AddConfirmFunction();
        Debug.Log("buttonFundtion Added In ShopItemSlot");
        //GameManager.instance.UIManager.shopManager.SetEachPrice();
    }

    public void ShopSlotUIData()
    {
        SetSlotName();
        SetSlotPrice();
    }
    void SetSlotName()
    {
        ItemName.text = itemData.name;
    }
    void SetSlotPrice()
    {
        itemPrice.text = itemData.price.ToString();
        //itemPrice.text = itemData.id.ToString();
    }
    void SetSlotImage()
    {

    }
    #endregion
}
