using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;
public class SetStringKey : MonoBehaviour,IObserver
{
    [SerializeField]
    string stringKey;
    [SerializeField]
    string stringText;
    [SerializeField]
    TextMeshProUGUI myText;
    StringKeyManager stringKeyManager;
    private void Awake()
    {
        OnAwake();
    }

    void OnAwake()
    {
        SetField();
        Attach();
    }

    private void Start()
    {
        Onstart();
    }
    void Onstart()
    {
        SetMyFont();


    }

    public string GetmyId()
    {
        if (stringKey != null)
        {
            return stringKey;
        }
        Debug.Log("string Key Null!!");
        return null;

    }
    public string SetStringText()
    {
        string text = "";
        if (stringKey != null)
        {
            text = stringKeyManager.GetStringData(stringKey);
        }
        else
        {
            Debug.Log("stringKey null in SetStringKey SetStringText Function");
        }

        return text;
    }
    public void SetMyText(string text)
    {
        myText.text = text;
    }
    public void SetMyFont()
    {
        if(myText.font != stringKeyManager.font)
        {
            myText.font = stringKeyManager.font;
            myText.UpdateFontAsset();
            Debug.Log("Font Set");
        }
    }

    void SetField()
    {
        if(myText == null)
        {
            myText = this.gameObject.transform.GetComponent<TextMeshProUGUI>();
        }
        if(stringKeyManager == null)
        {
            stringKeyManager = GameManager.instance.gameObject.transform.GetComponent<StringKeyManager>();
        }
    }

    void Attach()
    {
        stringKeyManager.Attach(this);
    }
    public void Invoke()
    {
        
    }
}
