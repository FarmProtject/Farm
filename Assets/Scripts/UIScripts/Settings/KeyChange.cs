using UnityEngine;
using System;
using System.Collections.Generic;
public class KeyChange : MonoBehaviour
{
    KeySettings keySet;



    void OnAwake()
    {

    }

    void GetKeys(KeyCode getKey)
    {
        
        if(keySet.keyName.ContainsValue(getKey))
        {
            string keyName = null;

            foreach(string bindingKey in keySet.keyName.Keys)
            {
                keyName = bindingKey;
                break;
            }
            if (keyName != null)
            {
                keySet.ChangeKey(keyName, getKey);
            }
        }
        else if (keySet.quickSlotKeyName.ContainsValue(getKey))
        {
            int keyNumber = -1 ;
            KeyCode tempKey = getKey;
            foreach(int bindingKey in keySet.quickSlotKeyName.Keys)
            {
                keyNumber = bindingKey;
                break;
            }
            if (keyNumber != -1)
            {
                keySet.CahngeQuickSlotKey(keyNumber, getKey);
            }
            
        }
    }

}
