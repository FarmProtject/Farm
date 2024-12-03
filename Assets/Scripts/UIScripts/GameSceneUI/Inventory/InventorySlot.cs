using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventorySlot : MonoBehaviour
{
    Sprite mySprite;
    TextMeshProUGUI itemCount;
    private void Start()
    {
        mySprite = transform.GetComponent<Image>().sprite;
        itemCount = transform.GetComponent<TextMeshProUGUI>();
    }


    void OnGetNewItem()
    {

    }

    void UpdateItemCount(ItemBase item)
    {
        
    }

}
