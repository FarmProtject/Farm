using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTailPanel : UIBase,IObserver,Isubject
{
    List<IObserver> obs = new List<IObserver>();
    InventoryPanel invenPanel;

    private void Awake()
    {
        invenPanel = GameObject.Find("InventoryPanel").GetComponent<InventoryPanel>();
        invenPanel.Attach(this);
    }

    protected override void Start()
    {

    }
    public void Attach(IObserver observer)
    {
        if (!obs.Contains(observer))
        {
            obs.Add(observer);
        }
        
    }

    public void Detach(IObserver observer)
    {
        if (obs.Contains(observer))
        {
            obs.Remove(observer);
        }
    }

    public void Invoke()
    {
        OnStart();
    }

    public void Notyfy()
    {
        foreach (IObserver obser in obs)
        {
            obser.Invoke();
        }
    }

    protected override void OnStart()
    {
        base.OnStart();
        Notyfy();
    }

}
