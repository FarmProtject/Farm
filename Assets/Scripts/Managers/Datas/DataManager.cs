using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public class DataManager : MonoBehaviour
{
    public string dataPath;
    CSVReader csvReader;
    Dictionary<int, Dictionary<string, object>> itemDatas;




    void AllItemDataRead()
    {
        List<Dictionary<string,object>> tempData = csvReader.Read(dataPath);


        for(int i = 0; i < tempData.Count;i++)
        {
            int index = Convert.ToInt32(tempData[i]["index"]);
            if (itemDatas.ContainsKey(index))
            {
                Debug.Log($"Already Contain key : {index}");
            }
            else
            {
                itemDatas.Add(index, tempData[i]);
            }
        }


    }
    
}
