using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseInputManager : MonoBehaviour
{

    public IMouseInput mouseInput;

    public Sprite baseCursorImage;
    InventoryData inventory;
    public InventorySlot clickedSlot;
    CameraMovement cameraMove;
    GameObject mouseOBJ;
    MouseOBJ followScript;
    private void Awake()
    {

        inventory = GameManager.instance.playerEntity.inventory;
        cameraMove = GameObject.Find("CameraOBJ").transform.GetComponent<CameraMovement>();
        InputChangeToCamera();
        EventManager.instance.OnPlayerMouseinput.AddListener(OnPlayerInput);
        mouseOBJ = GameObject.Find("MouseFollowOBJ");
        followScript = mouseOBJ.transform.GetComponent<MouseOBJ>();
        mouseOBJ.SetActive(false);
    }

    void Start()
    {

    }
    private void Update()
    {

    }

    public void OnPlayerInput()
    {
        OnLeftClick();
        OnRightClick();
        OnmouseWeel();

    }
    void OnLeftClick()
    {
        if (Input.GetMouseButton(0))
        {
            if (mouseInput == null)
                return;
            mouseInput.OnLeftClick();
        }
    }
    void OnRightClick()
    {
        Debug.Log(1111);
        if (Input.GetMouseButton(1))
        {
            if (mouseInput == null)
                return;
            mouseInput.OnRightClick();
        }
    }
    void OnmouseWeel()
    {
        if (mouseInput == null)
            return;
        mouseInput.OnMouseWheel();
    }
    public void CusorImageChange(Sprite sprite)
    {
        Vector2 spot = Vector2.zero;
        followScript.MouseOBjImageChange(sprite);
    }

    public void InputChangeToCamera()
    {
        mouseInput = cameraMove;
    }

    public void InventoryInput()
    {

    }

    public void OnInventoryClick(InventorySlot slot)
    {
        Sprite sprite;
        ItemBase item;
        if (inventory.inventory[slot.slotNumber] != null)
        {
            clickedSlot = slot;
            sprite = slot.itemSprite.sprite;
            item = inventory.inventory[slot.slotNumber];
            if(sprite == null)
            {
                Debug.Log("Sprite Null");
                return;
            }
            CusorImageChange(sprite);
        }
    }

    public void CursorImageReset()
    {
        followScript.MouseOBJImageReset();
    }
}
