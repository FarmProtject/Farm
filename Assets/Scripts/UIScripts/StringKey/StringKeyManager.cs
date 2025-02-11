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
        if (stringData != null)
        {

        }
        else
        {
            SetField();
        }

        text = stringData[key].ToString();
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


}
