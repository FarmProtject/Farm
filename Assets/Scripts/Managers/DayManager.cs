using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;
public class DayManager : MonoBehaviour
{
    List<IDayTickable> listners;

    public UnityEvent DayEvents;
    
    public int day;

    public void Resister(IDayTickable target)
    {
        if (!listners.Contains(target))
        {
            listners.Add(target);
        }
    }

    public void NextDay()
    {
        foreach(IDayTickable target in listners)
        {
            target.DayPassed();
        }
    }

}
