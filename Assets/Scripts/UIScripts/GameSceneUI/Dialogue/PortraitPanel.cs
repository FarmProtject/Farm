using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitPanel : UIBase
{
    [SerializeField] GameObject portraitOBJ;
    [SerializeField] NPCportrait portraitScript;
    private void Awake()
    {
        portraitOBJ = GameObject.Find("Portrait");
        portraitScript = portraitOBJ.GetComponent<NPCportrait>();
    }
    protected override void Start()
    {
        base.Start();
        SetUISize();
        SetPosition();

    }

    void Update()
    {
        
    }
    protected override void SetUISize()
    {
        myRect.anchorMin = new Vector2(0.0f, 0);
        myRect.anchorMax = new Vector2(0.3f, 1);
        myRect.sizeDelta = new Vector2(0, 0);
        portraitScript.panelHeight = myRect.rect.height;
        
    }
    protected override void SetPosition()
    {
        myRect.pivot = new Vector2(0, 0);
        myRect.anchoredPosition = new Vector2(0, 0);
    }
}
