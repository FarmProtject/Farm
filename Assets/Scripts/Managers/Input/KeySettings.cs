using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySettings 
{
    Dictionary<string, GameObject> keyObj = new Dictionary<string, GameObject>();
    public Dictionary<string, KeyCode> keyName = new Dictionary<string, KeyCode>();

    public Dictionary<int, KeyCode> quickSlotKeyName = new Dictionary<int, KeyCode>();
    public Dictionary<KeyCode, int> quickSlotKeys = new Dictionary<KeyCode,int>();
    public Dictionary<int, QuickSlot> quickSlots = new Dictionary<int, QuickSlot>();
    
    public void OpenUIOnKey(string key)
    {
        if (keyName.ContainsKey(key))
        {
            string objName = key;
            if (keyObj.ContainsKey(objName))
            {
                GameObject targetUI = keyObj[objName];

                if (targetUI.activeSelf)
                {
                    targetUI.SetActive(false);
                    if (GameManager.instance.UIManager.openUIObj.Contains(targetUI))
                    {
                        GameManager.instance.UIManager.openUIObj.Remove(targetUI);
                    }
                }

                else
                {
                    targetUI.SetActive(true);
                    if (!GameManager.instance.UIManager.openUIObj.Contains(targetUI))
                    {
                        GameManager.instance.UIManager.openUIObj.Add(targetUI);
                    }
                }
            }
            else
            {
                Debug.Log("Obj Didn't Mapiing");
            }
            
        }
        else
        {
            Debug.Log($"Key Didn't Mapping Key : {key.ToString()}");
        }
    }

    public void DefaultKeyBinding()
    {
        keyName.Add("run", KeyCode.LeftShift);
        keyName.Add("interact", KeyCode.F);
        keyName.Add("itemTest", KeyCode.T);
        keyName.Add("inventory", KeyCode.I);
        quickSlotKeyName.Add(1,KeyCode.Alpha1);
        quickSlotKeyName.Add(2, KeyCode.Alpha2);
        quickSlotKeyName.Add(3, KeyCode.Alpha3);
        quickSlotKeyName.Add(4, KeyCode.Alpha4);
        quickSlotKeyName.Add(5, KeyCode.Alpha5);
        quickSlotKeyName.Add(6, KeyCode.Alpha6);
        quickSlotKeyName.Add(7, KeyCode.Alpha7);
        quickSlotKeyName.Add(8, KeyCode.Alpha8);
        foreach(int key in quickSlotKeyName.Keys)
        {
            quickSlotKeys.Add(quickSlotKeyName[key], key);
        }
    }

    bool TryGetPressedKey(out KeyCode key)
    {
        foreach (KeyCode k in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(k))
            {
                key = k;
                return true;
            }
        }
        key = KeyCode.None;
        return false;
    }
}
