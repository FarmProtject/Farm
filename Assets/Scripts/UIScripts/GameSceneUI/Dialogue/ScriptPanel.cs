using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPanel : UIBase
{
    protected override void Start()
    {
        base.Start();
        SetUISize();
        SetPosition();
    }

    protected override void SetUISize()
    {
        myRect.anchorMin = new Vector2(0.3f, 0);
        myRect.anchorMax = new Vector2(1, 1);
        myRect.sizeDelta = new Vector2(0, 0);
    }

    protected override void SetPosition()
    {
        myRect.pivot = new Vector2(0, 0);
        myRect.anchoredPosition = new Vector2(0, 0);
    }
}
