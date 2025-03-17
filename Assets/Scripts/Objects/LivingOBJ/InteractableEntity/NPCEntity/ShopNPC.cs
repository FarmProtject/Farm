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
        if (dataManager.shopData.ContainsKey(id))
        {
            List<StringKeyDatas> shopData = dataManager.shopData[id];
            for (int i = 0; i < shopData.Count; i++)
            {
                int itemId = int.Parse(shopData[i].datas["itemId"].ToString());
                if (!shopItems.Contains(itemId))
                {
                    shopItems.Add(itemId);
                }
            }
        }
        else
        {
            Debug.Log("Don't Contain Id!");
        }
        
        
        
    }

    void GiveItemDataToShop()
    {
        Debug.Log("Need write GiveItemDataToShop Function in ShopNPC");
    }
}
