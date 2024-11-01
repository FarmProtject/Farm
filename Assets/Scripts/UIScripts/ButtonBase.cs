using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public interface IButtonAction
{
    void OnButtonClick();
}



public abstract class ButtonBase : MonoBehaviour
{
    Button myButton;
    protected virtual void Start()
    {
        myButton = this.transform.GetComponent<Button>();
        AddButtonFunction();
    }

    protected virtual void AddButtonFunction()
    {
        myButton.onClick.AddListener(OnButtonClick);
    }

    protected abstract void OnButtonClick();
    
}
