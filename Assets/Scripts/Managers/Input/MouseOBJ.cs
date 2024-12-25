using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MouseOBJ : MonoBehaviour
{
    GameManager gm;
    Image myImage;
    ItemBase item;
    RectTransform myRect;
    private void Awake()
    {
        gm = GameManager.instance;
        myImage = transform.GetChild(0).transform.GetComponent<Image>();
        myRect = transform.GetComponent<RectTransform>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        CursorFollow();
    }
    void OnStart()
    {
        
    }
    public void MouseOBjImageChange(Sprite sprite)
    {
        this.gameObject.SetActive(true);
        if(sprite == null)
        {
            Debug.Log("sprite Null");
            return;
        }
        myImage.sprite = sprite;
    }
    public void MouseOBJImageReset()
    {
        if(myImage.sprite != null)
        {
            myImage.sprite = null;
            this.gameObject.SetActive(false);
        }
    }
    void CursorFollow()
    {
        myRect.anchoredPosition = Input.mousePosition;
    }
}
