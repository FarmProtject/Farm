using UnityEngine;
using System;
using System.Collections.Generic;

public class QuickSlotLeftClick : IClickAction
{
    QuickSlotManager quickSlotManager;
    public QuickSlotLeftClick(QuickSlotManager quickSlotManager)
    {
        this.quickSlotManager = quickSlotManager;
    }
    public void Invoke()
    {
        if (quickSlotManager.GetSelectSlot() != null && quickSlotManager.GetSelectSlot().item != null)
        {
            quickSlotManager.GetWorldPosition();
            quickSlotManager.ItemInvoke();
            Debug.Log("QuickSlot left Click Invoke");
        }
            
        else
        {
            Debug.Log("quickslot data null");
        }
    }

    
}

public class QuickSlotManager : MonoBehaviour,Isubject
{

    Dictionary<int, ItemBase> quickSlotItems = new Dictionary<int, ItemBase>();
    public QuickSlotLeftClick quickLeftClick ;
    List<QuickSlot> quickSlots = new List<QuickSlot>();
    List<IObserver> observers = new List<IObserver>();
    [SerializeField] GameObject quickSlotPanel;
    [SerializeField] QuickSlot selectSlot;
    [SerializeField] EffectPull effectPuller;
    List<GameObject> targetObjs;
    int slotNumber;

    Vector3 targetPos;
    private void Awake()
    {
        OnAwake();

    }

    void OnAwake()
    {
        quickLeftClick = new QuickSlotLeftClick(this);
        AddQuickslot();
        QuickSlotSet();
        Notyfy();
        GetPlayerEffectPuller();
    }
    public void SetTargetPos(Vector3 target)
    {
        if(selectSlot.item is EffectItem effectItem)
        {
            if (effectItem.effect.target != TargetType.Self)
            {
                targetPos = effectPuller.parentsObj.transform.position;
            }
            else
            {
                targetPos = this.gameObject.transform.position;
            }
        }
        
    }
    private void Start()
    {
        //SetLeftClick();
    }
    void GetPlayerEffectPuller()
    {
        if(effectPuller == null)
        {
            effectPuller = GameManager.instance.playerEntity.myEffcetPuller;
        }
        
    }
    public void SetSelectSlot(QuickSlot slot)
    {
        selectSlot = slot;
        if(selectSlot.item is EffectItem effectItem)
        {
            EffectBase effect = effectItem.effect;
            effectPuller.SetEffectInfo(effect.collType,targetPos,effect.colliderVert,effect.colliderHori,effect.colliderHeight);
            effectPuller.SetColliderType(effect.collType);

        }
        
    }
    public QuickSlot GetSelectSlot()
    {
        return selectSlot;
    }
    
    void UseQuickSlot()
    {
        if (quickSlotItems[slotNumber] != null)
        {
            quickLeftClick.Invoke();
        }
        
    }
    void AddQuickslot()
    {
        if (quickSlotPanel == null)
        {
            quickSlotPanel = GameObject.Find("QuickSlotPanel").gameObject;
        }

        QuickSlot[] quicks = quickSlotPanel.transform.GetComponentsInChildren<QuickSlot>();
        
        Debug.Log("Added");
        foreach(QuickSlot quick in quicks)
        {
            quickSlots.Add(quick);
        }
        
    }
    void QuickSlotSet()
    {

        KeySettings keySetting = GameManager.instance.keySettings;
        for(int i = 0; i < quickSlots.Count; i++)
        {
            quickSlots[i].slotNumber = i + 1;
            keySetting.quickSlots.Add(quickSlots[i].slotNumber, quickSlots[i]);
        }
    }
    public void ItemInvoke()
    {
        //effectPuller.GetEffectColl().ReSetCollObjs();
        effectPuller.SetTargetPos(targetPos);
        
        //effectPuller.myObj.SetActive(true);
        effectPuller.Invoke();
        targetObjs = effectPuller.GetTargetObjs();
        Debug.Log("ItemInvoke");
        for(int i = 0; i < targetObjs.Count; i++)
        {
            GetSelectSlot().ItemInvoke(targetObjs[i]);
            Debug.Log("Item Invoke to GameObject");
        }
        //effectPuller.myObj.SetActive(false);

    }
    public void GetWorldPosition()
    {
        EffectItem effectItem = null;
        if(selectSlot.item is EffectItem eft)
        {
            effectItem = eft;
        }
        else
        {
            Debug.Log("Item isn't EffectItem");
            return;
        }
        if((effectItem!=null && effectItem.effect.target == TargetType.Self))
        {
            targetPos = this.gameObject.transform.position;
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            targetPos = hit.point;
        }

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
        for(int i = 0; i < observers.Count; i++)
        {
            observers[i].Invoke();
        }
    }


}
