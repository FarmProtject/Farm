using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class HeaderBar : UIBase,IDragHandler
{

    RectTransform parentsTr;
    float headerSize = 40f;
    protected override void Start()
    {
        base.Start();
        parentsTr = transform.parent.GetComponent<RectTransform>();
        SetUISize();
        SetPosition();
    }

    protected override void SetUISize()
    {
        myRect.anchorMin = new Vector2(0, 1);
        myRect.anchorMax = new Vector2(1, 1);
        myRect.sizeDelta = new Vector2(0, headerSize);
    }

    protected override void SetPosition()
    {
        myRect.pivot = new Vector2(0.5f, 1);
        myRect.anchoredPosition = new Vector2(0, headerSize);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        parentsTr.anchoredPosition += new Vector2(eventData.delta.x, eventData.delta.y);
    }
}
