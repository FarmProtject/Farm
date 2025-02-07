using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public class DataClass
{
    public Dictionary<int, Dictionary<string, object>> datas = new Dictionary<int, Dictionary<string, object>>();
}
public class DataManager : MonoBehaviour
{
    public string dataPath;

    string stringDataPath = "StringData";
    CSVReader csvReader = new CSVReader();

    public DataClass itemDatas = new DataClass();

    public Dictionary<int, ItemBase> items = new Dictionary<int, ItemBase>();

    public Dictionary<string, Dictionary<string,object>> stringDatas = new Dictionary<string, Dictionary<string,object>>();

    
    private void Awake()
    {
        OnAwake();
    }

    private void OnAwake()
    {
        AllItemDataRead();
        StringKeyRead(stringDataPath);
        KeyDebug();
    }

    void AllItemDataRead()
    {
        ItemDataRead("ToolData", itemDatas);
        ItemDataRead("EquipData", itemDatas);
        ItemDataRead("MaterialData", itemDatas);
        ItemDataRead("ConsumData", itemDatas);
    }
    void ItemDataRead(string path, DataClass newData)
    {
        List<Dictionary<string, object>> tempDataList = new List<Dictionary<string, object>>();
        tempDataList = csvReader.Read(path);
        for (int i = 0; i < tempDataList.Count; i++)
        {
            Dictionary<string, object> temp = tempDataList[i];
            int index;
            if (Int32.TryParse(temp["id"].ToString(), out index))
            {
                newData.datas.Add(index, tempDataList[i]);
                /*
                DataClass newData = new DataClass();
                newData.datas = temp;
                dataMap.Add(index, temp);
                */
            }
            else
            {
                Debug.Log("Data ParseError!!");
            }
        }
    }
    void StringKeyRead(string path)
    {
        List<Dictionary<string, object>> tempDatas = new List<Dictionary<string, object>>();
        tempDatas = csvReader.Read(path);
        for(int i = 0; i < tempDatas.Count; i++)
        {
            Dictionary<string, object> temp = tempDatas[i];
            string key;
            key = temp["id"].ToString();
            stringDatas.Add(key, tempDatas[i]);
        }
    }
    void KeyDebug()
    {
        foreach(var key in stringDatas.Keys)
        {
            foreach(var lng in stringDatas[key].Keys)
            {


                Debug.Log($"key : {key} lng : {lng} value : {stringDatas[key][lng]}");


            }



        }
    }
}
