using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
public enum ShopState
{
    buy,
    sell
}
public class ShopManager : MonoBehaviour
{

    public ItemBase buyingItem;
    int itemCount;
    [SerializeField]
    Button shopConfirmButton;
    public ShopState shopState;
    private void Awake()
    {
        shopConfirmButton = GameObject.Find("ShopConfirmButton").transform.GetComponent<Button>();
        
    }

    private void Start()
    {
        
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
    }

    public void SetBuyingItem(ItemBase item)
    {
        buyingItem = item;
    }

    void AddBuyingFunction()
    {
        shopConfirmButton.onClick.AddListener(BuyingItem);
    }

    public void AddSellFunction()
    {
        shopConfirmButton.onClick.AddListener(SellItem);
    }
    public void BuyingItem()
    {
        buyingItem.itemCount = itemCount;
        Debug.Log(buyingItem.id);
        GameManager.instance.playerEntity.inventory.OnGetItemCheck(buyingItem);
    }
    public void SellItem()
    {
        GameManager.instance.playerEntity.inventory.DecreaseItemCount(buyingItem,itemCount);
    }
}
