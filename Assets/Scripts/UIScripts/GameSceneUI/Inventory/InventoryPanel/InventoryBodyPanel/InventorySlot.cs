using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
    InventorySlot slot;
    public InvenLeftClick(InventorySlot slot)
    {
        this.slot = slot;
    }
    public void Invoke()
    {

        slot.InvenItemLeftClick();
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

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int slotNumber;
    [SerializeField]
    ItemBase item;
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
    [SerializeField]
    float rayCastRange = 100f;
    [SerializeField]
    string shopTag = "ShopUI";
    [SerializeField]
    int itemcount;
    [SerializeField]
    GameObject tooltipOBJ;
    ToolTipPanel tooltipPanel;
    private void Awake()
    {
        myItemImageObj = transform.GetChild(1).gameObject;
        myBackGroundOBJ = transform.GetChild(0).gameObject;
        itemSprite = myItemImageObj.transform.GetComponent<Image>();
        backGround = myBackGroundOBJ.transform.GetComponent<Image>();
        itemCount = GetComponentInChildren<TextMeshProUGUI>();
        myButton = GetComponent<Button>();
        if (tooltipOBJ != null)
        {
            tooltipPanel = tooltipOBJ.transform.GetComponent<ToolTipPanel>();
        }
        //inven = GameManager.instance.playerEntity.inventory;
        //GameManager.instance.playerEntity.SetInventoryNull();
        SetImageSize();
    }
    private void Start()
    {
        inven = GameManager.instance.playerEntity.inventory;
        EventManager.instance.OnInventoryUpdate.AddListener(UpDateMySlot);
        UpDateMySlot();
        myButton.onClick.AddListener(OnClickItem);
        rightClick = new InvenRightClick(this);
        leftClick = new InvenLeftClick(this);
        wheelAction = new InvenWheelAction();
        
    }

    public ItemBase GetItem()
    {
        return item;
    }
    void UpDateMySlot()
    {
        if (GameManager.instance.playerEntity.inventory.inventory.Count > slotNumber)
        {
            UpDateData();
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

    void UpDateData()
    {
        if (inven.inventory == null || slotNumber<0 || inven.inventory[slotNumber]==null)
        {
            return;
        }
        item = inven.inventory[slotNumber];
        if(item!=null)
        {
            if (item.itemCount == 0)
            {
                inven.inventory[slotNumber] = null;
            }
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
        if (GameManager.instance.playerEntity.inventory.inventory[slotNumber] != null)
        {
            itemCount.text = "id : " + GameManager.instance.playerEntity.inventory.inventory[slotNumber].id + " Count : " + GameManager.instance.playerEntity.inventory.inventory[slotNumber].itemCount.ToString();
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
        imageRect.anchorMin = new Vector2(0.1f, 0.1f);
        imageRect.anchorMax = new Vector2(0.9f, 0.9f);
        imageRect.sizeDelta = new Vector2(0, 0);
    }

    void OnClickItem()
    {
        if (GameManager.instance.mouseManager.clickedSlot == null)
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
        //마우스에 슬롯데이터 추가 후 우클릭 액션함수 추가
        InventoryData inven = GameManager.instance.playerEntity.inventory;
        if (itemSprite.sprite != null && inven.inventory[slotNumber] != null)
        {
            Texture2D texture = itemSprite.sprite.texture;
            GameManager.instance.mouseManager.OnInventoryClick(this);
            Debug.Log("PickUp");
            AddMyActions();
        }
    }
    #region 아이템 픽업 기능
    void SlotItemDown()
    {
        if (inven == null)
        {
            inven = GameManager.instance.playerEntity.inventory;
        }
        inven.InventorySlotSwap(GameManager.instance.mouseManager.clickedSlot.slotNumber, slotNumber);

        GameManager.instance.mouseManager.CursorImageReset();
        RemoveMyActions();
        GameManager.instance.mouseManager.clickedSlot = null;
        Debug.Log("ItemDown");
        //마우스 슬롯데이터 제거 후 우클릭 액션함수 제거
    }

    void ItemPickUpCancle()//우클릭으로 아이템 선택 캔슬
    {
        if (GameManager.instance.mouseManager.clickedSlot != null)
        {
            GameManager.instance.mouseManager.clickedSlot = null;
            GameManager.instance.mouseManager.CursorImageReset();
            Debug.Log("PickUp Cancle");
            RemoveMyActions();

        }

    }
    
    #endregion
    public void InvenItemLeftClick()
    {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            RayToShopLayer();
            Debug.Log("On UI");
            return;
        }
        else
        {
            ItemDrop();
            Debug.Log("ItemDrop");
        }
        

    }
    public void RayToShopLayer()
    {
        PointerEventData pointerEventData = new PointerEventData(GameManager.instance.UIManager.eventSystem);
        pointerEventData.position = Input.mousePosition;
        GraphicRaycaster caster = GameManager.instance.UIManager.grapicRaycaster;
        List<RaycastResult> results = new List<RaycastResult>();
        caster.Raycast(pointerEventData, results);
        Debug.Log("Lay Casted");
        
        foreach(var ui in results)
        {
            Debug.Log(ui.gameObject.name);
            if(ui.gameObject.tag == shopTag)
            {
                Debug.Log(111);
                ItemSell();
                return;
            }
        }
    }

    public void ItemDrop()
    {
        Debug.Log("ItemDrop");
        RemoveMyActions();
        ItemPickUpCancle();
    }
    public void ItemSell()
    {
        GameManager.instance.UIManager.shopManager.shopState = ShopState.sell;
        GameManager.instance.UIManager.shopManager.item = GameManager.instance.playerEntity.inventory.inventory[slotNumber];
        GameManager.instance.UIManager.shopManager.AddConfirmFunction();
        GameManager.instance.UIManager.ShopPopUpOpen();
        ItemPickUpCancle();
        RemoveMyActions();
        
        Debug.Log("sellItem");
    }
    public void InvenRightClick()
    {
        ItemPickUpCancle();
    }
    public void InvenWheelAction()
    {

    }
    public void AddMyActions()
    {
        GameManager.instance.mouseManager.rightClick.Push(rightClick);
        GameManager.instance.mouseManager.leftClick.Push(leftClick);
        //GameManager.instance.mouseManager.wheelAction.Push(wheelAction);
    }
    public void RemoveMyActions()
    {
        if (GameManager.instance.mouseManager.rightClick.actions.Contains(rightClick))
        {
            GameManager.instance.mouseManager.rightClick.RemoveAction(rightClick);
        }

        if (GameManager.instance.mouseManager.leftClick.actions.Contains(leftClick))
        {
            GameManager.instance.mouseManager.leftClick.RemoveAction(leftClick);
        }

        if (GameManager.instance.mouseManager.wheelAction.actions.Contains(wheelAction))
        {
            //GameManager.instance.mouseManager.wheelAction.RemoveAction(wheelAction);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            tooltipOBJ.SetActive(true);
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tooltipOBJ.activeSelf)
        {
            tooltipOBJ.SetActive(false);
        }
        
    }
}
