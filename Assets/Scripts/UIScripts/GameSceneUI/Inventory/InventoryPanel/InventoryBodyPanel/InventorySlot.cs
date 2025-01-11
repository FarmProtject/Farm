using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class InvenRightClick : IClickAction
{
    InventorySlot slot;
    public InvenRightClick(InventorySlot slot)
    {
        this.slot = slot;
    }
    public void Invoke()
    {
        slot.InvenRightClick();
        Debug.Log($"ItemRightClick : {GameManager.instance.mouseManager.rightClick.actions.Count }");
    }
}
public class InvenLeftClick : IClickAction
{
    public void Invoke()
    {
        Debug.Log("leftClick");
        Debug.Log($"ItemRightClick : {GameManager.instance.mouseManager.rightClick.actions.Count }");
    }
}
public class InvenWheelAction : IClickAction
{
    public void Invoke()
    {
        Debug.Log("wheelAction");
    }
}

public class InventorySlot : MonoBehaviour
{
    public int slotNumber;
    public Image itemSprite;
    InventoryData inven;
    Image backGround;
    public TextMeshProUGUI itemCount;
    GameObject myItemImageObj;
    GameObject myBackGroundOBJ;
    Button myButton;
    InvenRightClick rightClick;
    InvenLeftClick leftClick;
    InvenWheelAction wheelAction;
    private void Awake()
    {
        myItemImageObj = transform.GetChild(1).gameObject;
        myBackGroundOBJ = transform.GetChild(0).gameObject;
        itemSprite = myItemImageObj.transform.GetComponent<Image>();
        backGround = myBackGroundOBJ.transform.GetComponent<Image>();
        itemCount = GetComponentInChildren<TextMeshProUGUI>();
        myButton = GetComponent<Button>();
        //inven = GameManager.instance.playerEntity.inventory;
        SetImageSize();
    }
    private void Start()
    {
        EventManager.instance.OnInventoryUpdate.AddListener(UpDateMySlot);
        UpDateMySlot();
        myButton.onClick.AddListener(OnClickItem);
        rightClick = new InvenRightClick(this);
        leftClick = new InvenLeftClick();
        wheelAction = new InvenWheelAction();
        inven = GameManager.instance.playerEntity.inventory;
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
        //���콺�� ���Ե����� �߰� �� ��Ŭ�� �׼��Լ� �߰�
        InventoryData inven = GameManager.instance.playerEntity.inventory;
        if (itemSprite.sprite != null && inven.inventory[slotNumber] != null)
        {
            Texture2D texture = itemSprite.sprite.texture;
            GameManager.instance.mouseManager.OnInventoryClick(this);
            Debug.Log("PickUp");
            GameManager.instance.mouseManager.rightClick.Push(rightClick);
        }
    }
    #region ������ �Ⱦ� ���
    void SlotItemDown()
    {
        if (inven == null)
        {
            inven = GameManager.instance.playerEntity.inventory;
        }
        inven.InventorySlotSwap(GameManager.instance.mouseManager.clickedSlot.slotNumber, slotNumber);
        
        GameManager.instance.mouseManager.CursorImageReset();
        GameManager.instance.mouseManager.rightClick.RemoveAction(GameManager.instance.mouseManager.clickedSlot.rightClick); // �������� �߻�, �ش繮���� �������� �������ִ� ��ü�� ���� ������ �ذ�
        
        GameManager.instance.mouseManager.clickedSlot = null;
        Debug.Log("ItemDown");
        //���콺 ���Ե����� ���� �� ��Ŭ�� �׼��Լ� ����
    }

    void ItemPickUpCancle()//��Ŭ������ ������ ���� ĵ��
    {
        if(GameManager.instance.mouseManager.clickedSlot != null)
        {
            GameManager.instance.mouseManager.clickedSlot = null;
            GameManager.instance.mouseManager.CursorImageReset();
            Debug.Log("PickUp Cancle");
            GameManager.instance.mouseManager.rightClick.RemoveAction(rightClick);
        }
        
    }
    void ClickFunctionChange()
    {
        GameManager.instance.mouseManager.rightClick.Push(rightClick);
        GameManager.instance.mouseManager.leftClick.Push(leftClick);
        GameManager.instance.mouseManager.wheelAction.Push(wheelAction);
    }
    #endregion
    public void InvenItemLeftClick()
    {
        
    }
    
    public void InvenRightClick()
    {
        ItemPickUpCancle();
    }
    public void InvenWheelAction()
    {

    }
}
