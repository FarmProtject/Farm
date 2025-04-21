using UnityEngine;
using System;
using System.Collections.Generic;

[AttributeUsage(AttributeTargets.Class)]
public class AutoRegisterEffect : Attribute { }

[AutoRegisterEffect]
public abstract class EffectBase
{
    public int id;
    public string name;
    public ColliderInstatType collType;
    public TargetType target;
    public string functionName;
    public int integerParameter;
    public float percentParam;
    public float colliderHori;
    public float colliderVert;
    public float colliderHeight;

    public abstract void Apply(GameObject go);

    protected EffectBase()
    {
        AddToFactoryData();
    }

    void AddToFactoryData()
    {
        this.name = GetType().Name;
        GameManager.instance.dataManager.AddItemEffectDatas(this.name, this);
        Debug.Log($"AddTo Name {name}");
    }

    abstract protected EffectBase Create();
    public EffectBase GetNewScript()
    {
        return Create();
    }
}
