using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCNamePanel : UIBase
{
    public float panelHeight;

    protected override void Start()
    {
        base.Start();
        SetUISize();
        SetPosition();
    }

    protected override void SetPosition()
    {
        myRect.pivot = new Vector2(0.5f, 1);
        myRect.anchoredPosition = new Vector2(0, -20);
    }
    protected override void SetUISize()
    {
        myRect.anchorMin = new Vector2(0.5f, 0);
        myRect.anchorMax = new Vector2(0.5f, 0);
        myRect.sizeDelta = new Vector2(panelHeight, 30f);

    }
}
