using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    EventManager _instance;
    public static EventManager instance;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void SetUpField()
    {
        if(_instance == null)
        {
            _instance = this;
            instance = _instance;
        }
    }

    void OnSCeneLoad()
    {

    }
}
