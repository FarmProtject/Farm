using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour,Isubject
{

    public List<GameObject> openUIObj = new List<GameObject>();
    public GameObject NPCPanel;
    public GameObject inventoryPanel;
    public GameObject shopPanel;
    List<IObserver> observers = new List<IObserver>();


    private void Awake()
    {
        NPCPanel = GameObject.Find("DialoguePanel");
        inventoryPanel = GameObject.Find("InventoryPanel");
        shopPanel = GameObject.Find("ShopPanel");
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }



    public void Attach(IObserver observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }

    public void Detach(IObserver observer)
    {
        if (observers.Contains(observer))
        {
            observers.Remove(observer);
        }
    }

    public void Notyfy()
    {
        foreach(IObserver obs in observers)
        {
            obs.Invoke();
        }
    }
}
