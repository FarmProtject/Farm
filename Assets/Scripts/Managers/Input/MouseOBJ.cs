using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MouseOBJ : MonoBehaviour
{
    GameManager gm;
    Image myImage;
    ItemBase item;
    private void Awake()
    {
        gm = GameManager.instance;
        myImage = transform.GetComponent<Image>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void MouseOBjImageChange(Sprite sprite)
    {
        if(sprite == null)
        {
            Debug.Log("sprite Null");
            return;
        }
        myImage.sprite = sprite;
    }
    public void OnItemClickStart()
    {

    }
    void OnItemClickEnd()
    {

    }
    public void OnItemClick(ItemBase moveItem)
    {
        item = moveItem;
        //item.icon으로 스프라이트 데이터 읽어오는 로직 필요
        //MouseOBjImageChange();
    }
}
