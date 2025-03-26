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

public class EffectBase
{
    public int id;
    public string name;
    public EffectTarget target;
    public string functionName;
    public int integerParameter;
    public float percentParam;
    public float colliderHori;
    public float colliderVert;
    public float colliderHeight;

}
