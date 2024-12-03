using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStackable 
{
    int maxStack { get; }
    int AddStack(int amount);
    bool CanStack(int amount);
}
