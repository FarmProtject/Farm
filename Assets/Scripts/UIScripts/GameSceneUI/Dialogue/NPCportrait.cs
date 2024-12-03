using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCportrait : UIBase
{
    public float panelHeight;
    [SerializeField] GameObject npcNamePanel;
    NPCNamePanel npcNamePanelScript;
    private void Awake()
    {
        npcNamePanel = GameObject.Find("NPCNamePanel");
        npcNamePanelScript = npcNamePanel.GetComponent<NPCNamePanel>();
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
        Debug.Log(panelHeight);
        myRect.sizeDelta = new Vector2(panelHeight * 0.5f, panelHeight * 0.5f);
        npcNamePanelScript.panelHeight = myRect.rect.height;
    }
    protected override void SetPosition()
    {
        myRect.pivot = new Vector2(0.5f,0.3f);
        myRect.anchoredPosition = new Vector2(0,-20);
    }
}
