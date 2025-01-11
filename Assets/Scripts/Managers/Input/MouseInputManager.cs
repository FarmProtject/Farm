using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class ClickActionStack
{
    //�������� Ŭ���׼��� �����ϱ� ���ؼ� �������·� ����
    public List<IClickAction> actions = new List<IClickAction>();
    private IClickAction defaultAction;
    public void Push(IClickAction action)
    {
        if (!actions.Contains(action))
        {
            actions.Add(action);
        }
    }
    public IClickAction Pop()
    {
        if (actions.Count > 0)
        {
            IClickAction lastAction = actions[actions.Count - 1];
            actions.RemoveAt(actions.Count - 1);
            return lastAction;
        }
        return null;
    }
    public IClickAction Peek()
    {
        return actions.Count > 0 ? actions[^1] : defaultAction;

    }
    public void RemoveAction(IClickAction action)
    {
        var foundAction = actions.FirstOrDefault(a => a == action || a.Equals(action));
        if (foundAction != null)
        {
            actions.Remove(foundAction);
        }
        else
        {
            Debug.Log("Cant fount equal action");
        }

    }
}
public class MouseInputManager : MonoBehaviour
{

    public ClickActionStack leftClick;
    public ClickActionStack rightClick;
    public ClickActionStack wheelAction;

    public Sprite baseCursorImage;
    InventoryData inventory;
    public InventorySlot clickedSlot;
    CameraMovement cameraMove;
    [SerializeField]
    GameObject mouseOBJ;
    MouseOBJ followScript;

    PlayerEntity playerEntity;
    GameObject playerObj;

    private void Awake()
    {
        inventory = GameManager.instance.playerEntity.inventory;
        cameraMove = GameManager.instance.camearaMove;
        //cameraMove = GameObject.Find("Main Camera").transform.GetComponent<CameraMovement>();
        
        EventManager.instance.OnPlayerMouseinput.AddListener(OnPlayerInput);
        mouseOBJ = GameObject.Find("MouseFollowOBJ");
        followScript = mouseOBJ.transform.GetComponent<MouseOBJ>();
        mouseOBJ.SetActive(false);
        playerEntity = GameManager.instance.playerEntity;
        playerObj = playerEntity.gameObject;

        leftClick = new ClickActionStack();
        rightClick = new ClickActionStack();
        wheelAction = new ClickActionStack();
    }

    void Start()
    {
        InputChangeToCamera();
        
    }
    private void Update()
    {

    }
    #region ���콺 ��ǲ �����
    public void OnPlayerInput()
    {
        OnLeftClick();
        OnRightClick();
        OnmouseWeel();

    }
    void OnLeftClick()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (leftClick == null)
                return;
            leftClick.Peek().Invoke();
        }
    }
    void OnRightClick()
    {
        if (Input.GetMouseButton(1))
        {
            if (rightClick == null)
                return;
            rightClick.Peek().Invoke();
        }
    }
    void OnmouseWeel()
    {
        if (wheelAction == null)
            return;
        wheelAction.Peek().Invoke();
    }
    #endregion
    public void CusorImageChange(Sprite sprite)
    {
        Vector2 spot = Vector2.zero;
        followScript.MouseOBjImageChange(sprite);
    }

    public void InputChangeToCamera()
    {
        cameraMove.ChangeAllMouseInput();
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
    public void InputFunctionCheck()
    {
        if(leftClick.actions.Count == 0)
        {
            cameraMove.AddLeftClick();
        }
        if(rightClick.actions.Count==0)
        {
            cameraMove.AddRightClick();
        }
        if(wheelAction.actions.Count == 0)
        {
            cameraMove.AddWheel();
        }
    }
    public void CursorImageReset()
    {
        followScript.MouseOBJImageReset();
    }

    public Vector3 OnCharactorFront()
    {
        Vector3 forward = playerObj.transform.forward;
        return forward;
    }
    public Vector3 OnCameraFront()
    {
        Vector3 forward = (playerObj.transform.position - cameraMove.gameObject.transform.position).normalized;
        return forward;
    }
    public void OnInteractOBJ()
    {

    }
}
