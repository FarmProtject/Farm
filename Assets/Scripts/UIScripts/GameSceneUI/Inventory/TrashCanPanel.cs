using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TrashCanPanel : UIBase
{

    float myWidth;
    GameObject trashCanImage;
    protected override void Start()
    {
        base.Start();
        SetUISize();
        SetPosition();
        trashCanImage = transform.GetChild(0).gameObject;
        SetTrashCanImage();
    }

    protected override void SetUISize()
    {
        myRect.anchorMin = new Vector2(0,0);
        myRect.anchorMax = new Vector2(1, 0);

        myWidth = myRect.parent.GetComponent<RectTransform>().rect.width;
        
        myRect.sizeDelta = new Vector2(-40, myWidth-40);
        Debug.Log(myRect.rect.width);
        Debug.Log(myRect.rect.height);
    }
    protected override void SetPosition()
    {
        myRect.pivot = new Vector2(0.5f, 0);
        myRect.anchoredPosition = new Vector2(0, 0);
    }

    void SetTrashCanImage()
    {
        RectTransform canRect = trashCanImage.GetComponent<RectTransform>();
        canRect.sizeDelta = new Vector2(myWidth-40, myWidth - 40);
    }
}
