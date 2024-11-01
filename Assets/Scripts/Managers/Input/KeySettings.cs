using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySettings 
{
    Dictionary<string, GameObject> keyObj = new Dictionary<string, GameObject>();
    Dictionary<KeyCode, string> keyName = new Dictionary<KeyCode, string>();


    public void OpenUIOnKey(KeyCode key)
    {
        if (keyName.ContainsKey(key))
        {
            string objName = keyName[key];
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
}
