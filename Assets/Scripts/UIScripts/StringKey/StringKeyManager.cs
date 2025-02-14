using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;
enum languges
{
    kr,
    en,
    none
}

public class StringKeyManager : MonoBehaviour
{
    List<SetStringKey> observerList = new List<SetStringKey>();
    List<SetStringKey> shopText = new List<SetStringKey>();
    DataManager dataManager;
    Dictionary<string, Dictionary<string, object>> stringData = new Dictionary<string, Dictionary<string, object>>();
    [SerializeField]languges lng;
    [SerializeField]
    public TMP_FontAsset font;
    private void Awake()
    {
        OnAwake();
    }
    void OnAwake()
    {
        SetField();
    }
    private void Start()
    {
        OnStart();
    }
    void OnStart()
    {
        Notyfy();
    }
    public string GetStringData(string key)
    {

        string text;
        if(lng == languges.none)
        {
            lng = languges.en;
        }
        string lngKey = lng.ToString();
        if (stringData != null)
        {

        }
        else
        {
            SetField();
        }

        text = stringData[key][lngKey].ToString();
        return text;
    }
    void SetField()
    {
        if(dataManager == null)
        {
            dataManager = GameManager.instance.dataManager;
        }
        stringData = dataManager.stringDatas;
    }


    public void Attach(SetStringKey observer)
    {
        if (!observerList.Contains(observer))
        {
            observerList.Add(observer);
        }
        
    }

    public void Detach(SetStringKey observer)
    {
        if (observerList.Contains(observer))
        {
            observerList.Remove(observer);
        }
    }

    public void NotyfyAll()
    {
        Notyfy();
        NotyfyShop();
    }

    public void Notyfy()
    {
        if(lng == languges.none)
        {
            lng = languges.en;
        }
        string lngKey = lng.ToString();

        for(int i = 0; i < observerList.Count; i++)
        {
            string key = observerList[i].GetmyId();
            string value = stringData[key][lngKey].ToString();
            observerList[i].SetMyText(value);
            observerList[i].SetMyFont();
        }
    }

    public void NotyfyShop()
    {
        if(lng == languges.none)
        {
            lng = languges.en;
        }
        string lngKey = lng.ToString();

        for(int i = 0; i<shopText.Count; i++)
        {
            string key = shopText[i].GetmyId();
            string value = stringData[key][lngKey].ToString();
            shopText[i].SetMyText(value);
            shopText[i].SetMyFont();

        }


    }

}
