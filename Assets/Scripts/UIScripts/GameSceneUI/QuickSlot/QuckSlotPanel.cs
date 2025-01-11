using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuckSlotPanel : UIBase,Isubject
{
    List<IObserver> observers = new List<IObserver>();
    GridLayoutGroup myGrid;
    public Vector3 gridSize = new Vector3(90,90,0);
    public Vector2 spacing = new Vector2(7.5f,0);
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

    private void Awake()
    {
        myGrid = transform.GetComponent<GridLayoutGroup>();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnStart()
    {
        SetGridInfo();
        base.OnStart();
    }
    void SetGridInfo()
    {
        myGrid.cellSize = gridSize;
        myGrid.spacing = spacing;
    }
    void Update()
    {
        
    }
}
