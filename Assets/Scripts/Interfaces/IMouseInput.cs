using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMouseInput
{
    void OnLeftClick();

    void OnRightClick();

    void OnMouseWheel();

    void OnMouseInput();

    void AddMouseInput();
}
