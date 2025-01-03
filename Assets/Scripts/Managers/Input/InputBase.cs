using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputBase 
{
    protected GameObject selectObj;
    public GameManager gameManager;
    public abstract void OnPlayerInput();
}
