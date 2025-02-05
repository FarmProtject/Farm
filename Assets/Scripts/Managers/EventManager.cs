using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EventManager : MonoBehaviour
{
    EventManager _instance;
    public static EventManager instance;

    public UnityEvent MoveOnUpDate;
    public UnityEvent OnPlayerInput;
    public UnityEvent OnInventoryUpdate;
    public UnityEvent OnPlayerMouseinput;
    public UnityEvent FarmAreaEvents;
    private void Awake()
    {
        SetUpField();
        
    }
    private void Start()
    {
        if (OnInventoryUpdate != null)
        {
            OnInventoryUpdate.Invoke();
        }
    }
    void Update()
    {
        UpdateEvents();
    }

    void UpdateEvents()
    {
        if (MoveOnUpDate != null)
        {
            MoveOnUpDate.Invoke();
        }
        if (OnPlayerInput != null)
        {
            OnPlayerInput.Invoke();
        }
        if (OnPlayerMouseinput != null)
        {
            OnPlayerMouseinput.Invoke();
        }
        if (FarmAreaEvents != null)
        {
            FarmAreaEvents.Invoke();
        }
        GameManager.instance.mouseManager.InputFunctionCheck();


    }


    void SetUpField()
    {
        if (_instance == null)
        {
            _instance = this;
            instance = _instance;
        }
    }

    void OnSCeneLoad()
    {

    }
}
