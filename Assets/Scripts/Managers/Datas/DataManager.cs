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

    public PairData(string key , object value)
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
        if(Int32.TryParse(value.ToString(),out val))
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
    string equipentsStatTableDataPath = "equipmentStatTable";
    public Dictionary<int, Dictionary<string, object>> equipStatDatas = new Dictionary<int, Dictionary<string, object>>();

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
    string shpoDataPath = "Shop";
    [SerializeField]
    string soilItemDataPath = "soilItemData";
    [SerializeField]
    string toolDataPath = "ToolData";
    CSVReader csvReader = new CSVReader();


    private void Awake()
    {
        OnAwake();
    }

    private void OnAwake()
    {
        AllItemDataRead();
        StringKeyRead(dataPath+stringDataPath);
        KeyDebug();
    }

    void AllItemDataRead()
    {
        DataRead(dataPath+itemDataPath, itemDatas);
        /*
        ItemDataRead("EquipData", itemDatas);
        ItemDataRead("MaterialData", itemDatas);
        ItemDataRead("ConsumData", itemDatas);*/
    }

    void LoadMultiKey(string path, Dictionary<int, Dictionary<string, object>> newData)
    {
        List<Dictionary<string, object>> tempList = new List<Dictionary<string, object>>();
        tempList = csvReader.Read(path);

        for(int i = 0; i < tempList.Count; i++)
        {
            Dictionary<string, object> temp = tempList[i];
            int index;
            if (!temp.ContainsKey("id"))
            {
                Debug.Log("Table don't have id");
                return;
            }
            if(!temp.ContainsKey("groupId") && int.TryParse(temp["id"].ToString(),out index))
            {
                foreach(string key in temp.Keys)
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
            else if(temp.ContainsKey("groupId")&& int.TryParse(temp["groupId"].ToString(), out index))
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

    void DropTableRead(string path,Dictionary<string,List<DropTable>> dropTable)
    {
        List<Dictionary<string, object>> temp = csvReader.Read(path);
        for(int i = 0; i < temp.Count; i++)
        {
            DropTable tempTable = new DropTable();
            int index;
            if (int.TryParse(temp[i]["id"].ToString(),out index))
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
    /*
    void TableRead(string path, DataTable newData)
    {
        List<Dictionary<string, object>> tempDataList = new List<Dictionary<string, object>>();
        tempDataList = csvReader.Read(path);
        for(int i = 0; i < tempDataList.Count; i++)
        {
            Dictionary<string, object> temp = tempDataList[i];
            int index;
            if(Int32.TryParse(temp["id"].ToString(),out index))
            {
                if (newData.tables.ContainsKey(index))
                {
                    newData.tables[index].Add(temp);
                }
                else
                {
                    List<Dictionary<string, object>> tempTable = new List<Dictionary<string, object>>();
                    tempTable.Add(temp);
                    newData.tables.Add(index, tempTable);
                }
            }
        }
    }
    */
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
