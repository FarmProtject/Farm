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
    string statValue;
    StringKeyManager stringKeyManager;
    private void Awake()
    {
        OnAwake();
        SetMyFont();
    }

    void OnAwake()
    {
        SetField();
        Attach();
    }

    private void OnEnable()
    {
        //EnableFuction();
    }

    private void Start()
    {
        Onstart();
    }
    void Onstart()
    {
        SetMyFont();


    }
    public void EnableFuction()
    {
        if (stringKey != null)
        {
            myText.text = SetStringText();
        }
        
    }
    public void TextToStringText()
    {
        myText.text = stringText;
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
        if(stringKeyManager == null)
        {
            SetField();
        }
        if (stringKey != null && (stringKey.Contains("stat_")))
        {
            stringText = stringKeyManager.GetStringData(stringKey) + "  " + statValue;
            Debug.Log(stringText);
            Debug.Log("EquipMents Text ");
        }
        else if (stringKey != null)
        {
            Debug.Log($"  String Key : {stringKey} ");
            stringText = stringKeyManager.GetStringData(stringKey);
            Debug.Log(stringText);
        }
        else
        {
            Debug.Log("stringKey null in SetStringKey SetStringText Function");
        }

        return stringText;
    }
    public void UpdateMyText()
    {
        SetStringText();
        EnableFuction();
    }
    public void SetItemKey(string key) 
    {
        stringKey = "item_name_"+key;
    }
    public void SetItemDiscKey(string key)
    {
        stringKey = "item_desc_" + key;
    }
    public void SetNotiCommonKey(string key)
    {
        stringKey = "noti_common_" + key;
    }
    public void SetNotiShopKey(string key)
    {
        stringKey = "noti_shop_" + key;
    }
    
    public void SetTypeKey(string key)
    {
        stringKey = "Itemtype_" + key;
    }
    public void SetCommonKey(string key)
    {
        stringKey = "common_" + key;

    }
    
    public void SetEffectKey(string key)
    {
        stringKey = "useeffect_desc_" + key;
    }
    public void SetStatKey(string key,string value)
    {
        stringKey = "stat_" + key;
        statValue = value;
    }

    public void SetMyText(string text)
    {
        stringText = text;
        Debug.Log(stringText);
        myText.text = stringText;
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
    public string ValueTextReplace(string replace)
    {
        string outPut = stringText.Replace("{0}", replace);
        stringText = outPut;
        Debug.Log($"OutPut {outPut}");
        return outPut;
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
