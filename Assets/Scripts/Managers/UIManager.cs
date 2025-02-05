using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UIManager : MonoBehaviour,Isubject
{

    public List<GameObject> openUIObj = new List<GameObject>();


    GameObject dialogueObj;
    NPCDialoguePanel dialoguePanel;
    
    public GameObject inventoryObj;
    InventoryPanel inventoryPanel;

    [SerializeField]
    GameObject shopPanelObj;
   
    ShopPanel shopPanel;

    [SerializeField]
    GameObject shopPopUpObj;
    ShopPopUpPanel shopPopUp;
    List<IObserver> observers = new List<IObserver>();

    public ShopManager shopManager;

    public GraphicRaycaster grapicRaycaster;
    public EventSystem eventSystem;
    private void Awake()
    {
        dialogueObj = GameObject.Find("DialoguePanel");
        dialoguePanel = dialogueObj.transform.GetComponent<NPCDialoguePanel>();
        inventoryObj = GameObject.Find("InventoryPanel");
        inventoryPanel = inventoryObj.transform.GetComponent<InventoryPanel>();
        shopPanelObj = GameObject.Find("ShopPanel");
        shopPanel = shopPanelObj.transform.GetComponent<ShopPanel>();
        shopPopUpObj = GameObject.Find("ShopPopUpPanel");
        shopPopUp = shopPopUpObj.transform.GetComponent<ShopPopUpPanel>();
        if (shopManager == null)
        {
            shopManager = GameObject.Find("ShopManager").transform.GetComponent<ShopManager>();
        }
        grapicRaycaster = GameObject.Find("Canvas").transform.GetComponent<GraphicRaycaster>();
        eventSystem = GameObject.Find("EventSystem").transform.GetComponent<EventSystem>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    #region Dialogue

    public void DialogueOpen(string dialogues)
    {
        dialogueObj.SetActive(true);
        dialoguePanel.SetDialogue(dialogues);
    }
    public void DialogueClose()
    {
        dialogueObj.SetActive(false);
        dialoguePanel.RemoveDialogue();
    }
    public void NPCRangeOut(NPC npc)
    {
        Debug.Log("Range Out!!");
        if (GameManager.instance.playerEntity.nowInteract == npc.gameObject && dialogueObj.activeSelf)
        {
            DialogueClose();
            Debug.Log("Range Out!! Script Active!");
            if( npc is ShopNPC)
            {
                ShopPanelClose();
                InventoryClose();
            }
            GameManager.instance.playerEntity.nowInteract = null;
        }
    }
    public void DialogueUIToggle(NPC npc)
    {
        if (dialogueObj.activeSelf)
        {
            DialogueClose();
        }
        else
        {
            DialogueOpen(npc.GetDialogue());
        }
    }

    #endregion
    #region inventory

    public void InventoryOpen()
    {
        inventoryObj.SetActive(true);
    }
    public void InventoryClose()
    {
        inventoryObj.SetActive(false);
    }
    public void OnInventoryKey()
    {
        if (inventoryObj.activeSelf)
        {
            InventoryClose();
        }
        else
        {
            InventoryOpen();
        }
    }
    #endregion
    #region ShopInteract
    public void ShopPanelOpen(List<int> items)
    {
        shopPanel.AddItemList(items);
        shopPanelObj.SetActive(true);
    }
    public void ShopPanelClose()
    {
        shopPanelObj.SetActive(false);
        
    }
    public void OnShopInteract(ShopNPC shopNPC,List<int> items)
    {
        if (GameManager.instance.playerEntity.nowInteract == shopNPC.gameObject && shopPanelObj.activeSelf)
        {
            ShopPanelClose();
            shopPanel.RemoveItems();
            inventoryObj.SetActive(false);
        }
        else if (GameManager.instance.playerEntity.nowInteract == shopNPC.gameObject && !shopPanelObj.activeSelf)
        {
            ShopPanelOpen(items);
            if (!inventoryObj.activeSelf)
            {
                inventoryObj.SetActive(true);
            }
            
        }
    }
    public void ShopPopUpOpen()
    {
        if (!shopPopUpObj.activeSelf)
        {
            shopPopUpObj.SetActive(true);
        }
        shopPopUp.UpdateMyData();
        
        
    }
    public void ShopPopUpClose()
    {
        shopPopUpObj.SetActive(false);
    }
    #endregion
    #region ObserverInterface
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
    #endregion
}
