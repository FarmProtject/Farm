using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursorOBJ : MonoBehaviour
{
    public RectTransform canvasRect;
    public RectTransform mytRect;
    void Start()
    {
        canvasRect = GameObject.Find("Canvas").gameObject.transform.GetComponent<RectTransform>();
        mytRect = transform.GetComponent<RectTransform>();
    }

    void Update()
    {
        CursorFollow();
    }

    void CursorFollow()
    {
        Vector2 mousePosition = Input.mousePosition;

        mytRect.anchoredPosition = mousePosition;
    }
}
