using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;
public class SetStringKey : MonoBehaviour,IObserver
{
    [SerializeField]
    string stringKey;
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
        
    }
    void Onstart()
    {

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

    public void SetMyText(string text)
    {


        myText.text = text;
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
