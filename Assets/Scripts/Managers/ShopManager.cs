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
    //[SerializeField]
    //int itemCount;
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
    }

    public void AddConfirmFunction()
    {
        switch (shopState)
        {
            case ShopState.buy:
                AddBuyingFunction();
                break;
            case ShopState.sell:
                AddSellFunction();
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
        shopConfirmButton.onClick.AddListener(BuyItem);
    }

    public void AddSellFunction()
    {
        shopConfirmButton.onClick.AddListener(SellItem);
    }
    #region 팝업 인풋필드
    public void SetInputField()
    {
        switch (shopState)
        {
            case ShopState.buy:
                BuyCheck();
                break;
            case ShopState.sell:
                SellCheck();
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
        eachPriceText.text = item.price.ToString();
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
        sumPriceText.text = (item.price * item.itemCount).ToString();
    }
    #endregion
    #region 아이템구매
    public void BuyCheck()
    {
        int buyItemCount = item.itemCount;
        item = GameManager.instance.playerEntity.inventory.OnGetItemCheck(item);
        item.itemCount = CheckBuyMaxCount(item, buyItemCount);
        tradePrice = CheckBuyPrice(item, item.itemCount);
    }
    void BuyItem()
    {
        BuyCheck();
        player.gold -= tradePrice;
        player.inventory.AddinInventory(item);
    }
    int CheckBuyMaxCount(ItemBase item,int itemCount)
    {
        int price = item.price * itemCount;
        int returnCount = itemCount;
        if (returnCount < player.gold) // 플레이어의 총 골드보다 아이템 가격의 총합이 높다면 아이템 갯수 재설정
        {
            returnCount = player.gold / item.price;
        }
        return returnCount;
    }
    int CheckBuyPrice(ItemBase item,int itemCount)
    {
        int price = item.price * itemCount;
        if(price > player.gold)
        {
            itemCount = player.gold / item.price;
            price = item.price * itemCount;
        }
        SetSumPrice();
        return price;
    }
    #endregion
    #region 아이템판매
    int SetSellMaxCount(ItemBase item, int itemCount)
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
        SetSellMaxCount(item, item.itemCount);
    }
    #endregion
    public void SellItem()
    {
        GameManager.instance.playerEntity.inventory.DecreaseItemCount(item, item.itemCount);
    }
}
