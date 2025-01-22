using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScriptPanel : UIBase
{
    TextMeshProUGUI textMesh;
    protected override void Start()
    {
        base.Start();
        SetUISize();
        SetPosition();
        textMesh = transform.GetComponentInChildren<TextMeshProUGUI>();
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

    public void SetText(string dialogue)
    {
        textMesh.text = dialogue;

    }
}
