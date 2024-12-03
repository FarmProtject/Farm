using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class UIMoveTest : MonoBehaviour,IDragHandler
{
    public RectTransform myRect;
    Canvas canvs;

    private void Start()
    {
        canvs = GameObject.Find("Canvas").transform.GetComponent<Canvas>();
        myRect = this.gameObject.transform.GetComponent<RectTransform>();
    }
    void Update()
    {

    }



    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector2 dragPos = eventData.position;
        myRect.anchoredPosition += new Vector2(eventData.delta.x, eventData.delta.y); 
    }

}
