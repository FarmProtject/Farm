using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public class DataClass
{
    public Dictionary<int, Dictionary<string, object>> datas = new Dictionary<int, Dictionary<string, object>>();
}

public class DataTable
{
    public Dictionary<int, List<PairData>> table = new Dictionary<int, List<PairData>>();
}
public class DropTable
{
    public int id;
    public string key;
    public int dropItem;
    public int dropRation;
    public int minDropCount;
    public int maxDropCount;
}
public class PairData
{
    string key;
    object value;

    public PairData(string key, object value)
    {
        this.key = key;
        this.value = value;
    }
    public string GetKey()
    {
        return key;
    }
    public string GetStringValue()
    {

        return value.ToString();
    }
    public int GetIntegerValue()
    {
        int val;
        if (Int32.TryParse(value.ToString(), out val))
        {
            return val;
        }
        Debug.Log("Can't Parse Integer");
        return val;
    }
}
public class DataManager : MonoBehaviour
{
    public string dataPath = "DataTables\\";
    [SerializeField]
    string stringDataPath = "String";
    public Dictionary<string, Dictionary<string, object>> stringDatas = new Dictionary<string, Dictionary<string, object>>();

    [SerializeField]
    string itemDataPath = "itemData";
    public DataClass itemDatas = new DataClass();
    public Dictionary<int, ItemBase> items = new Dictionary<int, ItemBase>();

    [SerializeField]
    string effectDataPath = "effect";

    [SerializeField]
    string consumItemDataPath = "consumableItemData";

    [SerializeField]
    string equipItemDataPath = "EquipItemData";

    [SerializeField]
    string equipentsStatTableDataPath = "equipmentStatTable"; //멀티키 형태, LoadMultiKey 실행
    public Dictionary<int, Dictionary<string, object>> equipStatDatas = new Dictionary<int, Dictionary<string, object>>();
    public Dictionary<int, Dictionary<string, int>> equipStat = new Dictionary<int, Dictionary<string, int>>();

    [SerializeField]
    string gameConfigDataPath = "GameConfig";

    [SerializeField]
    string harvestDataPath = "harvestData";
    public Dictionary<string, Dictionary<string, object>> harvestData = new Dictionary<string, Dictionary<string, object>>();

    [SerializeField]
    string harvestItemDataPath = "harvestItemData";

    [SerializeField]
    string materialDataPath = "MaterialData";

    [SerializeField]
    string readySoilItemDataPath = "readySoilItemData";

    [SerializeField]
    string shpoDataPath = "Shop"; //멀티키형태 , LoadMultiKey 실행
    public Dictionary<int, Dictionary<string, object>> shopData;
    public Dictionary<int, Dictionary<string, int>> shops;

    [SerializeField]
    string soilItemDataPath = "soilItemData";

    [SerializeField]
    string toolDataPath = "ToolData";

    [SerializeField]
    string dropTablePath = "dropItemGroupTable";//전용함수 사용 DropTableRead
    Dictionary<string, List<DropTable>> dropTable = new Dictionary<string, List<DropTable>>();

    CSVReader csvReader = new CSVReader();


    private void Awake()
    {
        OnAwake();
    }

    private void OnAwake()
    {
        AllItemDataRead();
        StringKeyRead(dataPath + stringDataPath);
        KeyDebug();
    }

    void AllItemDataRead()
    {
        DataRead(dataPath + itemDataPath, itemDatas);
        /*
        ItemDataRead("EquipData", itemDatas);
        ItemDataRead("MaterialData", itemDatas);
        ItemDataRead("ConsumData", itemDatas);*/
    }
    void ReadMultiKey()
    {
        LoadMultiKey(dataPath + shpoDataPath,shopData);
        LoadMultiKey(dataPath + equipItemDataPath,equipStatDatas);
        MultiObToInt(shopData,shops);
        MultiObToInt(equipStatDatas,equipStat);
    }
    void ReadDropTable()
    {
        DropTableRead(dropTablePath,dropTable);
    }

    void LoadMultiKey(string path, Dictionary<int, Dictionary<string, object>> newData)
    {
        List<Dictionary<string, object>> tempList = new List<Dictionary<string, object>>();
        tempList = csvReader.Read(path);

        for (int i = 0; i < tempList.Count; i++)
        {
            Dictionary<string, object> temp = tempList[i];
            int index;
            if (!temp.ContainsKey("id"))
            {
                Debug.Log("Table don't have id");
                return;
            }
            if (!temp.ContainsKey("groupId") && int.TryParse(temp["id"].ToString(), out index))
            {
                foreach (string key in temp.Keys)
                {
                    if (newData.ContainsKey(index))
                    {
                        newData[index].Add(key, int.Parse(temp[key].ToString()));
                    }
                    else
                    {
                        Dictionary<string, object> tempDict = new Dictionary<string, object>();
                        tempDict.Add(key, temp[key]);
                        newData.Add(index, tempDict);
                    }
                }
            }
            else if (temp.ContainsKey("groupId") && int.TryParse(temp["groupId"].ToString(), out index))
            {

                foreach (string key in temp.Keys)
                {
                    if (newData.ContainsKey(index))
                    {
                        newData[index].Add(key, int.Parse(temp[key].ToString()));
                    }
                    else
                    {
                        Dictionary<string, object> tempDict = new Dictionary<string, object>();
                        tempDict.Add(key, temp[key]);
                        newData.Add(index, tempDict);
                    }
                }
            }
        }


    }

    void DropTableRead(string path, Dictionary<string, List<DropTable>> dropTable)
    {
        List<Dictionary<string, object>> temp = csvReader.Read(path);
        for (int i = 0; i < temp.Count; i++)
        {
            DropTable tempTable = new DropTable();
            int index;
            if (int.TryParse(temp[i]["id"].ToString(), out index))
            {
                tempTable.id = index;
            }
            if (temp[i].ContainsKey("keys"))
            {
                tempTable.key = temp[i]["keys"].ToString();
            }
            if (int.TryParse(temp[i]["dropItem"].ToString(), out index))
            {
                tempTable.dropItem = index;
            }
            if (int.TryParse(temp[i]["dropRation"].ToString(), out index))
            {
                tempTable.dropRation = index;
            }
            if (int.TryParse(temp[i]["minDropCount"].ToString(), out index))
            {
                tempTable.minDropCount = index;
            }
            if (int.TryParse(temp[i]["maxDropCount"].ToString(), out index))
            {
                tempTable.maxDropCount = index;
            }
        }
    }
    void MultiObToInt(Dictionary<int,Dictionary<string,object>> origin,Dictionary<int,Dictionary<string,int>> newData)
    {//장비 스탯데이터 읽어 온 후 int형식으로 변환
        Dictionary<int, Dictionary<string, object>> data = origin;
        Dictionary<string, int> temp = new Dictionary<string, int>();
        foreach(int index in equipStatDatas.Keys)
        {

            foreach(string key in data[index].Keys)
            {
                int stat;
                if(!temp.ContainsKey(key)&& int.TryParse(data[index][key].ToString(), out stat))
                {
                    temp.Add(key, stat);
                }
            }
            newData.Add(index, temp);

        }
    }
    void DataRead(string path, DataClass newData)
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
        for (int i = 0; i < tempDatas.Count; i++)
        {
            Dictionary<string, object> temp = tempDatas[i];
            string key;
            key = temp["id"].ToString();
            stringDatas.Add(key, tempDatas[i]);
        }
    }
    void KeyDebug()
    {
        foreach (var key in stringDatas.Keys)
        {
            foreach (var lng in stringDatas[key].Keys)
            {
                Debug.Log($"key : {key} lng : {lng} value : {stringDatas[key][lng]}");
            }



        }
    }

    

}
