using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BackGround : UIBase
{

    protected override void Start()
    {
        SetPosition();
        SetUISize();
    }




    protected override void SetPosition()
    {
        if(myRect == null)
        {
            SetupInitialField();
        }
        
        myRect.anchoredPosition = Vector2.zero;
        myRect.anchoredPosition = new Vector2(0, 0);
    }
    protected override void SetUISize()
    {
        myRect.anchorMin = new Vector2(0, 0);
        myRect.anchorMax = new Vector2(1, 1);
        myRect.sizeDelta = Vector2.zero;
    }
}
