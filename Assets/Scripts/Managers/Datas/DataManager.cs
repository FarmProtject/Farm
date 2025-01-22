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
    CSVReader csvReader = new CSVReader();

    public DataClass itemDatas = new DataClass();

    public Dictionary<int, ItemBase> items = new Dictionary<int, ItemBase>();

    private void Awake()
    {
        OnAwake();
        foreach(int i in itemDatas.datas.Keys)
        {
            Debug.Log($"Key is { i} value {itemDatas.datas[i]}");
        }
    }

    private void OnAwake()
    {
        AllItemDataRead();
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
}
