using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public List<GameObject> openUIObj = new List<GameObject>();
    public GameObject NPCPanel;
    public GameObject inventoryPanel;
    public GameObject shopPanel;

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
}
