using UnityEngine;
using System;
using System.Collections.Generic;
public class CropData 
{
    public int groudId;
    public int id;
    public int level;
    public int nextLevel;
    public float myTime;
    public int reqTime;
    public int hp;
    public string name;
    public string Image;
    public string model;
    public string texture;
    public string dropId;


    public void SetGroupId(int groupId)
    {
        this.groudId = groupId;
    }
    public void SetId(int id)
    {
        this.id = id;
    }
}
