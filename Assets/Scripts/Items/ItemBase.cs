using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rarity
{

}

public enum ItemType 
{ 

}

public class ItemBase 
{
    public int id { get; set; }
    public string name { get; set; }
    public Rarity rarity { get; set; }
    public string stat { get; set; }
    public string skill { get; set; }
    public string desc { get; set; }
    public int price { get; set; }
    public string icon { get; set; }
    public string model { get; set; }

}
