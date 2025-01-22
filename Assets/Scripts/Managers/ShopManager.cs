using UnityEngine;
using System;
using System.Collections.Generic;
public class ShopManager : MonoBehaviour
{

    ItemBase buyingItem;

    public void SetBuyingItem(ItemBase item)
    {
        buyingItem = item;
    }
}
