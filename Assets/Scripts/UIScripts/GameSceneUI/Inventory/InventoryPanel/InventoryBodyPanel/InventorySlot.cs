using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
public class InventorySlot : MonoBehaviour,IMouseInput
{
    public int slotNumber;
    public Image itemSprite;
    Image backGround;
    public TextMeshProUGUI itemCount;
    GameObject myItemImageObj;
    GameObject myBackGroundOBJ;
    Button myButton;
    private void Awake()
    {
        myItemImageObj = transform.GetChild(1).gameObject;
        myBackGroundOBJ = transform.GetChild(0).gameObject;
        itemSprite = myItemImageObj.transform.GetComponent<Image>();
        backGround = myBackGroundOBJ.transform.GetComponent<Image>();
        itemCount = GetComponentInChildren<TextMeshProUGUI>();
        myButton = GetComponent<Button>();
        SetImageSize();
    }
    private void Start()
    {
        EventManager.instance.OnInventoryUpdate.AddListener(UpDateMySlot);
        UpDateMySlot();
        myButton.onClick.AddListener(OnClickItem);
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

    void OnClickItem()
    {
        if(GameManager.instance.mouseManager.clickedSlot == null)
        {
            SlotItemPickUp();
        }
        else
        {
            SlotItemDown();
        }
    }

    void SlotItemPickUp()
    {
        InventoryData inven = GameManager.instance.playerEntity.inventory;
        if (itemSprite.sprite != null && inven.inventory[slotNumber] != null)
        {
            Texture2D texture = itemSprite.sprite.texture;
            GameManager.instance.mouseManager.OnInventoryClick(this);
            Debug.Log("PickUp");
        }
    }
    
    void SlotItemDown()
    {
        InventoryData inven = GameManager.instance.playerEntity.inventory;
        inven.InventorySlotSwap(GameManager.instance.mouseManager.clickedSlot.slotNumber, slotNumber);
        GameManager.instance.mouseManager.clickedSlot = null;
        GameManager.instance.mouseManager.CursorImageReset();
        Debug.Log("ItemDown");
    }

    void MouseInputChange()
    {
        GameManager.instance.mouseManager.mouseInput = this;
    }
    public void OnLeftClick()
    {
        throw new System.NotImplementedException();
    }

    public void OnRightClick()
    {
        throw new System.NotImplementedException();
    }

    public void OnMouseWheel()
    {
        throw new System.NotImplementedException();
    }

    public void OnMouseInput()
    {
        throw new System.NotImplementedException();
    }

    public void AddMouseInput()
    {
        throw new System.NotImplementedException();
    }
}
