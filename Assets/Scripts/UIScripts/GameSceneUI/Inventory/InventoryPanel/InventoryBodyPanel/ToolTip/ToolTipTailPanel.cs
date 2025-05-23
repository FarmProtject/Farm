using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;
public class ToolTipTailPanel : UIBase, IObserver
{
    ToolTipPanel tooltipPanel;
    public SetStringKey myDscString;
    public TextMeshProUGUI myGoldText;
    List<UIBase> observers = new List<UIBase>();
    private void Awake()
    {
        OnAwake();
    }
    protected override void Start()
    {

    }
    protected override void OnStart()
    {
        base.OnStart();
        SetObservers();
        Notyfy();
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        tooltipPanel = transform.parent.transform.GetComponent<ToolTipPanel>();
        tooltipPanel.Attach(this);
    }
    public void Invoke()
    {
        OnStart();
    }
    void SetObservers()
    {
        foreach (UIBase child in transform.GetComponentsInChildren<UIBase>(true))
        {
            if (child.gameObject == this.gameObject)
            {
                return;
            }
            Attach(child);
        }
    }
    public void Attach(UIBase observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }

    public void Detach(UIBase observer)
    {
        if (observers.Contains(observer))
        {
            observers.Remove(observer);
        }
    }
    public void Notyfy()
    {
        foreach (UIBase obs in observers)
        {
            obs.GetStart();
        }
    }
    public void SetPos(float left, float right, float bottom, float top)
    {
        onBottom = bottom;
        onLeft = left;
        onRight = right;
        onTop = top;

    }
}
