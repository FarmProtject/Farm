using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
public class InventorySlot : MonoBehaviour
{
    public int slotNumber;
    Image itemSprite;
    Image backGround;
    public TextMeshProUGUI itemCount;
    GameObject myItemImageObj;
    GameObject myBackGroundOBJ;
    private void Awake()
    {
        myItemImageObj = transform.GetChild(1).gameObject;
        myBackGroundOBJ = transform.GetChild(0).gameObject;
        itemSprite = myItemImageObj.transform.GetComponent<Image>();
        backGround = myBackGroundOBJ.transform.GetComponent<Image>();
        itemCount = GetComponentInChildren<TextMeshProUGUI>();
        SetImageSize();
    }
    private void Start()
    {
        EventManager.instance.OnInventoryUpdate.AddListener(UpDateMySlot);
        UpDateMySlot();
    }

    void UpDateMySlot()
    {
        if (GameManager.instance.playerEntity.inventory.inventory.Count > slotNumber)
        {
            UpDateSprite();
            UpdateItemAmount();
            SetImageSize();
        }
        else
        {
            itemSprite.sprite = null;
            itemCount.text = "";
        }

    }

    void UpDateSprite()
    {
        if (GameManager.instance.playerEntity.inventory.inventory[slotNumber] != null)
        {
            itemSprite.sprite = GameManager.instance.testImage;
        }
            
        else
        {
            itemSprite.sprite = null;
            
        }
    }

    void UpdateItemAmount()
    {
        if(GameManager.instance.playerEntity.inventory.inventory[slotNumber]!=null)
        {
            itemCount.text ="id : "+ GameManager.instance.playerEntity.inventory.inventory[slotNumber].id+" Count : "+GameManager.instance.playerEntity.inventory.inventory[slotNumber].itemCount.ToString();
        }
        else
        {
            itemCount.text = "";
        }
    }

    void SetImageSize()
    {
        if (myItemImageObj == null) return;

        RectTransform imageRect = myItemImageObj.GetComponent<RectTransform>();
        imageRect.anchorMin = new Vector2(0.1f,0.1f);
        imageRect.anchorMax = new Vector2(0.9f,0.9f);
        imageRect.sizeDelta = new Vector2(0, 0);
    }
}
