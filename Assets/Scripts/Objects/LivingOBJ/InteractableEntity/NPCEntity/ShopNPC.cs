using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPC : NPC, IInteractable
{

    List<ItemBase> shopItems = new List<ItemBase>();
    GameObject shopPanelObj;
    GameObject inventoryObj;
    ShopPanel shopData;
    
    protected override void OnAwake()
    {
        base.OnAwake();
        shopPanelObj = GameManager.instance.UIManager.shopPanel;
        inventoryObj = GameManager.instance.UIManager.inventoryPanel;
        shopData = shopPanelObj.transform.GetComponent<ShopPanel>();
        SetItemData();
    }
    protected override void Start()
    {
        base.Start();
        SetItemData();
    }
    protected override void NPCInteract()
    {
        base.NPCInteract();
        OnShopOpen();
    }
    void OnShopOpen()
    {
        if (GameManager.instance.playerEntity.nowInteract == this.gameObject && shopPanelObj.activeSelf)
        {
            shopPanelObj.SetActive(false);
            inventoryObj.SetActive(false);
        }
        else if(GameManager.instance.playerEntity.nowInteract == this.gameObject && !shopPanelObj.activeSelf)
        {
            shopPanelObj.SetActive(true);
            if (!inventoryObj.activeSelf)
            {
                inventoryObj.SetActive(true);
            }
            GiveItemDataToShop();
        }
    }
    protected override void NPCRangeOut()
    {
        Debug.Log("Range Out!!");
        if (GameManager.instance.playerEntity.nowInteract == this.gameObject && dialoguePanel.activeSelf)
        {
            dialoguePanel.SetActive(false);
            shopPanelObj.SetActive(false);
            inventoryObj.SetActive(false);
            GameManager.instance.playerEntity.nowInteract = null;
            
            Debug.Log("Range Out!! Script Active!");
        }
    }
    void SetItemData()
    {
        Debug.Log("Need write SetItemData Function in ShopNPC");
    }

    void GiveItemDataToShop()
    {
        Debug.Log("Need write GiveItemDataToShop Function in ShopNPC");
    }
}
