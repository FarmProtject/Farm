using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    protected RectTransform myRect;
    protected UIManager uiManager;
    protected virtual void Start()
    {
        SetupInitialField();
        SetPosition();
    }

    protected virtual void SetupInitialField()
    {
        myRect = this.gameObject.transform.GetComponent<RectTransform>();
    }
    protected virtual void SetPosition()
    {
        if(myRect == null)
        {
            myRect = this.transform.GetComponent<RectTransform>();
        }

        myRect.anchoredPosition = Vector2.zero;


        SetUISize();
        SetUIImage();
    }
    protected virtual void SetUIImage()
    {

    }
    protected virtual void SetUISize()
    {
        myRect.anchorMin = new Vector2(0.5f - 0.33f / 2, 0.45f - 0.67f / 2);
        myRect.anchorMax = new Vector2(0.5f + 0.33f / 2, 0.45f + 0.67f / 2);
        myRect.sizeDelta = Vector2.zero;
    }

    protected virtual void AddOnUIManager()
    {
        uiManager.openUIObj.Add(this.gameObject);
    }
    protected virtual void RemoveOnUIManager()
    {
        uiManager.openUIObj.Remove(this.gameObject);
    }
}
