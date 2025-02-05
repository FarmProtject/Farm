using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class ShopPanel : UIBase, Isubject , IPointerClickHandler
{
    List<IObserver> observers = new List<IObserver>();
    List<GameObject> slots = new List<GameObject>();
    [SerializeField]
    GameObject SlotContents;
    [SerializeField]
    Dictionary<int,ItemBase> itemDict = new Dictionary<int, ItemBase>();
    List<ItemBase> itemList = new List<ItemBase>();
    ItemBase buyingItem;

    DataManager dataManager;

    GameObject PopUpObj;
    [SerializeField]
    GameObject slotPrefab;

    Image slotImage;
    TextMeshProUGUI nameText;
    TextMeshProUGUI priceText;
    #region lifeCycle
    private void Awake()
    {
        dataManager = GameManager.instance.dataManager;
        PopUpObj = GameObject.Find("ShopPopUpPanel");
        //SlotContents = GameObject.Find("SlotContents");
    }
    private void OnEnable()
    {
        SetSlotCount();
        SetSlotData();
    }

    private void OnDisable()
    {
        itemDict.Clear();
        
    }
    #endregion
    #region UISet
    public void AddItemList(List<int> itemIdList)
    {
        foreach(int index in itemIdList)
        {
            itemDict.Add(index, GameManager.instance.itemFactory.GetItemData(index));
            itemList.Add(itemDict[index]);
        }
    }
    public void RemoveItems()
    {
        itemDict.Clear();
        itemList.Clear();
    }
    

    protected override void OnStart()
    {
        base.OnStart();
        Notyfy();
        this.gameObject.SetActive(false);
    }
    #endregion
    #region observerPattern
    public void Attach(IObserver observer)
    {
        observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void Notyfy()
    {
        foreach(var obs in observers)
        {
            obs.Invoke();
        }
    }
    #endregion
    #region UIFunctions

    public void SetSelectItem(int id)
    {
        buyingItem = itemDict[id];
    }

    public void BuyingPopUpOpen(int index)
    {
        SetSelectItem(index);
        if (itemDict.ContainsKey(index))
        {
            PopUpObj.transform.GetComponent<ShopPopUpPanel>().item = itemDict[index];
        }
        else
        {
            Debug.Log("Item Key Error");
        }
        
    }

    #endregion
    #region SetSlotDatas
    void SetSlotCount()
    {
        if (itemDict.Count < 1)
        {
            return;
        }
        if (slots.Count < itemDict.Count)
        {
            for(int i = slots.Count; i < itemDict.Count; i++)
            {
                MakeSlotPrefab();
            }
        }
        else if(slots.Count == itemDict.Count)
        {
            return;
        }
        else
        {
            for(int i = itemDict.Count; i < slots.Count; i++)
            {
                slots[i - 1].SetActive(false);
            }
        }
    }
    void SetSlotData()
    {
        if (itemDict.Count < 1)
        {
            return;
        }
        for(int i = 0; i < itemDict.Count; i++)
        {
            ShopItemSlot slot = slots[i].transform.GetComponent<ShopItemSlot>();
            slot.itemData = itemList[i];
            slot.ShopSlotUIData();
        }
    }
    void MakeSlotPrefab()
    {
        GameObject go = Instantiate(slotPrefab);
        slots.Add(go);
        go.transform.SetParent(SlotContents.transform);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnClick ShopPanel");
    }
    #endregion
}
