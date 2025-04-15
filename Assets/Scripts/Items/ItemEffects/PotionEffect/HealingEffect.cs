using UnityEngine;
using System;
using System.Collections.Generic;
public class HealingEffect : EffectBase
{
    public override void Apply(GameObject go)
    {
        LivingEntity entity;

        if(entity = go.transform.GetComponent<LivingEntity>())
        {
            entity.AddHealthPoint(integerParameter);
        }
    }

    protected override EffectBase Create()
    {
        return new HealingEffect();
    }
}
