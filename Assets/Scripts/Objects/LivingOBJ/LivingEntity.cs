using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Races { }


public class LivingEntity 
{
    public string name { get; set; }
    public string gender { get; set; }
    public int age { get; set; }
    public Races race { get; set; }
    public string prefab { get; set; }//외형 관련 정보
    public string faceSprite { get; set; }//얼굴 이미지

    public int level { get; set; }
    public int attack { get; set; }
    public int deffense { get; set; }
    public int healthPoint { get; set; }
    public int steminaPoint { get; set; }
    public float moveSpeed { get; set; }


}
