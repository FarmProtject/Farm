using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMouseInput
{
    void OnLeftClickStart();
    void OnLeftClickEnd();

    void OnRightClickStart();
    void OnRightClickEnd();

    void OnMouseWheel();


    void OnMouseInput();
}

public class MouseInputManager : MonoBehaviour
{

    IMouseInput leftMouseClick;
    IMouseInput rightMouseClick;
    IMouseInput onMouseWeel;
    void Start()
    {

    }


    public void OnPlayerInput()
    {
        OnLeftClick();
        OnRightClick();

    }
    void OnLeftClick()
    {
        if (Input.GetMouseButton(0))
        {
            if (leftMouseClick == null)
                return;
            leftMouseClick.OnLeftClickStart();
        }
    }
    void OnRightClick()
    {
        if (Input.GetMouseButton(1))
        {
            if (rightMouseClick == null)
                return;
            rightMouseClick.OnRightClickStart();
        }
    }
    void OnmouseWeel()
    {
        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            if (onMouseWeel == null)
                return;
            onMouseWeel.OnMouseWheel();
        }
    }
}
