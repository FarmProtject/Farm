using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySettings 
{
    Dictionary<string, GameObject> keyObj = new Dictionary<string, GameObject>();
    public Dictionary<string, KeyCode> keyName = new Dictionary<string, KeyCode>();
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
        Debug.Log(keyName["run"]);
        keyName.Add("interact", KeyCode.F);
        Debug.Log(keyName["interact"]);
        keyName.Add("itemTest", KeyCode.T);
        Debug.Log(keyName["itemTest"]+"Item Test Key in");
        keyName.Add("inventory", KeyCode.I);
        Debug.Log(keyName["inventory"]);
    }
}
