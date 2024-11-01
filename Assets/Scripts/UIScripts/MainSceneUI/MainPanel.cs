using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanel : UIBase
{

    protected override void SetUISize()
    {
        myRect.anchorMin = new Vector2(0.5f - 0.33f/2, 0.45f - 0.67f/2);
        myRect.anchorMax = new Vector2(0.5f + 0.33f/2, 0.45f + 0.67f/2);
        myRect.sizeDelta = Vector2.zero;
        SetChildrensSize();
    }

    private void SetChildrensSize()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            RectTransform rect =  transform.GetChild(i).transform.GetComponent<RectTransform>();
            ChildeSize(rect);
            Debug.Log("Size Set");

        }
    }
    private void ChildeSize(RectTransform rect)
    {
        rect.anchorMin = new Vector2(5, 5);
        rect.anchorMax = new Vector2(5, 5f);
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.sizeDelta = new Vector2(myRect.rect.width, myRect.rect.height*0.25f);
    }
}
