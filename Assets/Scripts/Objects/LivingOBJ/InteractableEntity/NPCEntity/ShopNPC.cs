using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPC : NPC, IInteractable
{
    
    List<int> shopItems = new List<int>();
    
    protected override void OnAwake()
    {
        base.OnAwake();
        uiManager = GameManager.instance.UIManager;
        SetItemData();
        shopItems.Add(100001);
        shopItems.Add(300011);
    }
    protected override void Start()
    {
        base.Start();
        SetItemData();
    }
    protected override void NPCInteract()
    {
        base.NPCInteract();
        uiManager.OnShopInteract(this,shopItems);
    }
    
    protected override void NPCRangeOut()
    {
        uiManager.NPCRangeOut(this);
    }
    void SetItemData()
    {
        
        DataManager dataManager = GameManager.instance.dataManager;
        List<StringKeyDatas> shopData = dataManager.shopData[id];
        if (!dataManager.shopData.ContainsKey(id))
        {
            Debug.Log("Don't Contain Id!");
        }
        for(int i = 0; i < shopData.Count; i++)
        {
            int itemId = int.Parse(shopData[i].datas["itemId"].ToString());
            shopItems.Add(itemId); 
        }
        
    }

    void GiveItemDataToShop()
    {
        Debug.Log("Need write GiveItemDataToShop Function in ShopNPC");
    }
}
