using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class EffectBase
{
    public int id;
    public string name;
    ColliderInstatType collType;
    public TargetType target;
    public string functionName;
    public int integerParameter;
    public float percentParam;
    public float colliderHori;
    public float colliderVert;
    public float colliderHeight;

    public abstract void Apply(GameObject go);
}
