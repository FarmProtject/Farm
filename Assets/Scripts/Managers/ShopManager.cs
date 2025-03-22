using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
public enum ShopState
{
    buy,
    sell
}
public class ShopManager : MonoBehaviour
{
    static ShopManager instance;
    public ItemBase item;
    [SerializeField]
    int tradePrice;
    [SerializeField]
    public int itemCount;
    GameObject popUpOBJ;
    [SerializeField]
    Button shopConfirmButton;
    public ShopState shopState;
    [SerializeField]
    GameObject inputFieldOBJ;
    [SerializeField]
    TMP_InputField inputfield;
    [SerializeField]
    TextMeshProUGUI eachPriceText;
    [SerializeField]
    TextMeshProUGUI sumPriceText;
    [SerializeField]
    PlayerEntity player;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        shopConfirmButton = GameObject.Find("ShopConfirmButton").transform.GetComponent<Button>();
        SetField();
    }

    private void Start()
    {
        
    }

    void SetField()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        if(inputFieldOBJ != null)
        {
            inputfield = inputFieldOBJ.transform.GetComponent<TMP_InputField>();
        }
        if(player == null)
        {
            player = GameManager.instance.playerEntity;
        }
        if(popUpOBJ == null)
        {
            popUpOBJ = GameObject.Find("ShopPopUpPanel");
        }
    }

    public void AddConfirmFunction()
    {
        if (shopConfirmButton.onClick != null)
        {
            shopConfirmButton.onClick.RemoveAllListeners();
        }
        switch (shopState)
        {
            case ShopState.buy:
                AddBuyingFunction();
                Debug.Log("buy function added in ShopManager");
                break;
            case ShopState.sell:
                AddSellFunction();
                Debug.Log("sell Function aDded in ShopManager");
                break;
            default:
                break;
        }
        SetEachPrice();
    }

    public void SetBuyingItem(ItemBase item)
    {
        this.item = item;
        SetEachPrice();
    }
    /*
    public void SetItemCount(int count)
    {
        itemCount = count;
    }
    */
    void AddBuyingFunction()
    {
        shopConfirmButton.onClick.RemoveAllListeners();
        shopConfirmButton.onClick.AddListener(BuyItem);
    }

    public void AddSellFunction()
    {
        shopConfirmButton.onClick.RemoveAllListeners();
        shopConfirmButton.onClick.AddListener(SellItem);
    }
    #region 팝업 인풋필드
    public void SetInputField()
    {
        switch (shopState)
        {
            case ShopState.buy:
                BuyCheck();
                SetSumPrice();
                break;
            case ShopState.sell:
                SellCheck();
                SetSumPrice();
                break;
            default:
                break;
        }
        SetSumPrice();
    }
    
    #endregion
    #region 팝업가격텍스트
    void SetEachPrice()
    {
        if(eachPriceText == null)
        {
            Debug.Log("eachPriceNull   In Shop Manager");
            return;
        }
        if(item == null)
        {
            Debug.Log("Item Null in ShopManager SetEachPrice");
            return;
        }
        //eachPriceText.text = item.price.ToString();
    }

    void SetSumPrice()
    {
        if(sumPriceText == null)
        {
            Debug.Log("SumPriceNull In ShopManager");
            return;
        }
        if(item == null)
        {
            Debug.Log("item Null in ShopManager SetSumPrice");
            return;
        }
        switch (shopState)
        {
            case ShopState.buy:
                break;
            case ShopState.sell:
                break;
            default:
                break;
        }
        
    }
    #endregion
    #region 아이템구매
    public void BuyCheck()
    {
        int buyItemCount = item.itemCount;
        item = GameManager.instance.playerEntity.inventory.OnGetItemCheck(item);
        item.itemCount = CheckBuyMaxCount(item, buyItemCount);
        tradePrice = CheckBuyPrice(item, item.itemCount);
        itemCount = item.itemCount;
    }
    void BuyItem()
    {
        BuyCheck();
        
        player.gold -= tradePrice;
        ItemBase newItem = GameManager.instance.itemFactory.ItemMake(item.id);
        Debug.Log($"new Item Name !{newItem.name}");
        newItem.itemCount = itemCount;
        player.inventory.AddinInventory(newItem);
        Debug.Log("item buy");
        popUpOBJ.SetActive(false);
    }
    int CheckBuyMaxCount(ItemBase item,int itemCount)
    {
        int price = item.price * itemCount;
        int returnCount = itemCount;
        if(player == null)
        {
            Debug.Log("Player Null In CheckBuyMaxCount");
            return 0;
        }
        if (price > player.gold) // 플레이어의 총 골드보다 아이템 가격의 총합이 높다면 아이템 갯수 재설정
        {
            returnCount = player.gold / item.price;
        }
        return returnCount;
    }
    int CheckBuyPrice(ItemBase item,int itemCount)
    {
        int price = item.price * itemCount;
        Debug.Log(price);
        if(price > player.gold)
        {
            item.itemCount = player.gold / item.price;
            price = item.price * itemCount;
        }
        SetSumPrice();
        return price;
    }
    #endregion
    #region 아이템판매
    int CheckSellMaxCount(ItemBase item, int itemCount)
    {
        int returnCount = itemCount;
        if(item.itemCount < returnCount)
        {
            returnCount = item.itemCount;
        }
        return returnCount;
    }
    void SellCheck()
    {
        if (!player.inventory.inventory.Contains(item))
        {
            Debug.Log("Can't Find Item In player Inventory in ShopManager Sellcheck");
            return;
        }
        itemCount = CheckSellMaxCount(item, itemCount);
    }
    #endregion
    public void SellItem()
    {
        if(item == null)
        {
            Debug.Log("item Data Null");
            return;
        }
        player.gold += item.price * itemCount;
        Debug.Log("sell");
        GameManager.instance.playerEntity.inventory.DecreaseItemCount(item, itemCount);
        
        EventManager.instance.OnInventoryUpdate.Invoke();
        popUpOBJ.SetActive(false);
    }
}
