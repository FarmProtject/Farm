using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GoldText : UIBase,IObserver
{
    InventoryTailPanel inventoryTail;
    TextMeshProUGUI myText;
    PlayerEntity player;
    public void Invoke()
    {
        OnStart();
    }
    private void Awake()
    {
        inventoryTail = GameObject.Find("InvetoryTailPanel").transform.GetComponent<InventoryTailPanel>();
        inventoryTail.Attach(this);
        myText = transform.GetComponent<TextMeshProUGUI>();
        player = GameManager.instance.playerEntity;
        
    }
    protected override void Start()
    {
        
    }

    protected override void OnStart()
    {
        base.OnStart();
        EventManager.instance.OnInventoryUpdate.AddListener(GoldTextUpdate);
    }

    public void GoldTextUpdate()
    {
        myText.text = player.gold.ToString();
    }
}
