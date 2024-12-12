using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCportrait : UIBase
{
    public float panelHeight;
    [SerializeField] GameObject npcNamePanel;
    [SerializeField] GameObject dialoguePanel;
    GameObject shopPanel;
    NPCNamePanel npcNamePanelScript;
    private void Awake()
    {
        npcNamePanel = GameObject.Find("NPCNamePanel");
        npcNamePanelScript = npcNamePanel.GetComponent<NPCNamePanel>();
        dialoguePanel = GameObject.Find("DialoguePanel");
        shopPanel = GameObject.Find("ShopPanel");
    }
    protected override void Start()
    {
        base.Start();
        SetUISize();
        SetPosition();
    }

    protected override void SetUISize()
    {
        if(myRect == null)
        {
            myRect = transform.GetComponent<RectTransform>();
        }
        myRect.anchorMin = new Vector2(0.5f, 0.5f);
        myRect.anchorMax = new Vector2(0.5f, 0.5f);//Áß¾ÓÁ¤·Ä
        myRect.sizeDelta = new Vector2(panelHeight * 0.5f, panelHeight * 0.5f);
        npcNamePanelScript.panelHeight = myRect.rect.height;
    }
    protected override void SetPosition()
    {
        myRect.pivot = new Vector2(0.5f,0.3f);
        myRect.anchoredPosition = new Vector2(0,-20);
    }
}
