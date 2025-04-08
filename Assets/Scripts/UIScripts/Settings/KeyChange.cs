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
            string keyName;

            foreach(string bindingKey in keySet.keyName.Keys)
            {
                keyName = bindingKey;
            }
        }
        else if (keySet.quickSlotKeyName.ContainsValue(getKey))
        {
            int keyNumber;
            KeyCode tempKey = getKey;
            foreach(int bindingKey in keySet.quickSlotKeyName.Keys)
            {
                keyNumber = bindingKey;
            }

            
        }
    }

}
