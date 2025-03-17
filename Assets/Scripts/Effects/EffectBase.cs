using UnityEngine;
using System;
using System.Collections.Generic;

public enum EffectTarget
{
    target,
    nonTarget,
    self,
    none
}

public enum EffectType
{
    heal
}
public class EffectBase : MonoBehaviour
{
    public int id;
    public string name;
    public EffectTarget target;
    public string functionName;
    public EffectType effectType;
    public int integerParameter;
    public float percentParam;
    public float colliderSizeX;
    public float colliderSizeY;
    public float colliderSizeZ;

}
