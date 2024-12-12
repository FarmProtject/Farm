using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShopHeadTextPanel : UIBase,IObserver
{
    ShopHeadPanel shopHead;
    TextMeshProUGUI shopText;
    protected override void OnAwake()
    {
        base.OnAwake();
        shopHead = transform.parent.GetComponent<ShopHeadPanel>();
        shopHead.Attach(this);
        shopText = transform.GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Awake()
    {
        OnAwake();
    }
    protected override void Start()
    {
        
    }
    void SetShopText()
    {
        shopText.text = "Shop";
    }

    protected override void OnStart()
    {
        base.OnStart();
    }

    public void Invoke()
    {
        OnStart();
    }
}
