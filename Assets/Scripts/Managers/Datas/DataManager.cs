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
    public string dataPath { get; private set; } = "DataTables\\";

    [SerializeField] private string stringDataPath = "String";
    public string StringDataPath
    {
        get => $"{dataPath}{stringDataPath}";
        set => stringDataPath = value.Replace(dataPath, "");
    }

    public Dictionary<string, Dictionary<string, object>> stringDatas = new Dictionary<string, Dictionary<string, object>>();

    [SerializeField] private string fileNamePath = "FilePath";
    public string FileNamePath
    {
        get => $"{dataPath}{fileNamePath}";
        set => fileNamePath = value.Replace(dataPath, "");
    }

    public List<Dictionary<string, string>> fileNames = new List<Dictionary<string, string>>();

    [SerializeField] private string itemDataPath = "itemData";
    public string ItemDataPath
    {
        get => $"{dataPath}{itemDataPath}";
        set => itemDataPath = value.Replace(dataPath, "");
    }

    public DataClass itemDatas = new DataClass();
    public Dictionary<int, ItemBase> items = new Dictionary<int, ItemBase>();

    [SerializeField] private string effectDataPath = "effect";
    public string EffectDataPath
    {
        get => $"{dataPath}{effectDataPath}";
        set => effectDataPath = value.Replace(dataPath, "");
    }

    public Dictionary<int, Dictionary<string, string>> effectData = new Dictionary<int, Dictionary<string, string>>();

    [SerializeField] private string consumItemDataPath = "consumableItemData";
    public string ConsumItemDataPath
    {
        get => $"{dataPath}{consumItemDataPath}";
        set => consumItemDataPath = value.Replace(dataPath, "");
    }

    public Dictionary<int, Dictionary<string, string>> consumItemData = new Dictionary<int, Dictionary<string, string>>();

    [SerializeField] private string equipItemDataPath = "EquipItemData";
    public string EquipItemDataPath
    {
        get => $"{dataPath}{equipItemDataPath}";
        set => equipItemDataPath = value.Replace(dataPath, "");
    }

    public Dictionary<int, Dictionary<string, string>> equipItemData = new Dictionary<int, Dictionary<string, string>>();

    [SerializeField] private string equipentsStatTableDataPath = "equipmentStatTable";
    public string EquipmentsStatTableDataPath
    {
        get => $"{dataPath}{equipentsStatTableDataPath}";
        set => equipentsStatTableDataPath = value.Replace(dataPath, "");
    }

    public Dictionary<int, Dictionary<string, object>> equipStatDatas = new Dictionary<int, Dictionary<string, object>>();
    public Dictionary<int, Dictionary<string, int>> equipStat = new Dictionary<int, Dictionary<string, int>>();

    [SerializeField] private string gameConfigDataPath = "GameConfig";
    public string GameConfigDataPath
    {
        get => $"{dataPath}{gameConfigDataPath}";
        set => gameConfigDataPath = value.Replace(dataPath, "");
    }

    public Dictionary<string, Dictionary<string, string>> gameConfigData = new Dictionary<string, Dictionary<string, string>>();

    [SerializeField] private string harvestDataPath = "harvestData";
    public string HarvestDataPath
    {
        get => $"{dataPath}{harvestDataPath}";
        set => harvestDataPath = value.Replace(dataPath, "");
    }

    public Dictionary<int, Dictionary<string, string>> harvestData = new Dictionary<int, Dictionary<string, string>>();

    [SerializeField] private string harvestItemDataPath = "harvestItemData";
    public string HarvestItemDataPath
    {
        get => $"{dataPath}{harvestItemDataPath}";
        set => harvestItemDataPath = value.Replace(dataPath, "");
    }

    public Dictionary<int, Dictionary<string, string>> harvestItemData = new Dictionary<int, Dictionary<string, string>>();

    [SerializeField] private string materialDataPath = "MaterialData";
    public string MaterialDataPath
    {
        get => $"{dataPath}{materialDataPath}";
        set => materialDataPath = value.Replace(dataPath, "");
    }

    public Dictionary<int, Dictionary<string, string>> materialItemData = new Dictionary<int, Dictionary<string, string>>();

    [SerializeField] private string readySoilItemDataPath = "readySoilItemData";
    public string ReadySoilItemDataPath
    {
        get => $"{dataPath}{readySoilItemDataPath}";
        set => readySoilItemDataPath = value.Replace(dataPath, "");
    }

    public Dictionary<int, Dictionary<string, string>> readySoilItemData = new Dictionary<int, Dictionary<string, string>>();

    [SerializeField] private string shopDataPath = "Shop";
    public string ShopDataPath
    {
        get => $"{dataPath}{shopDataPath}";
        set => shopDataPath = value.Replace(dataPath, "");
    }

    public Dictionary<int, Dictionary<string, object>> shopData = new Dictionary<int, Dictionary<string, object>>();
    public Dictionary<int, Dictionary<string, int>> shops = new Dictionary<int, Dictionary<string, int>>();

    [SerializeField] private string soilItemDataPath = "soilItemData";
    public string SoilItemDataPath
    {
        get => $"{dataPath}{soilItemDataPath}";
        set => soilItemDataPath = value.Replace(dataPath, "");
    }

    public Dictionary<int, Dictionary<string, string>> soilItemData = new Dictionary<int, Dictionary<string, string>>();

    [SerializeField] private string toolDataPath = "ToolData";
    public string ToolDataPath
    {
        get => $"{dataPath}{toolDataPath}";
        set => toolDataPath = value.Replace(dataPath, "");
    }

    public Dictionary<int, Dictionary<string, string>> toolData = new Dictionary<int, Dictionary<string, string>>();

    [SerializeField] private string dropTablePath = "dropItemGroupTable";
    public string DropTablePath
    {
        get => $"{dataPath}{dropTablePath}";
        set => dropTablePath = value.Replace(dataPath, "");
    }

    public Dictionary<string, List<DropTable>> dropTable = new Dictionary<string, List<DropTable>>();

    private CSVReader csvReader = new CSVReader();
    private void Awake()
    {
        OnAwake();
    }

    private void OnAwake()
    {
        SetFileNames();
        AllItemDataRead();
        StringKeyRead(stringDataPath);
        KeyDebug();
    }

    void AllItemDataRead()
    {
        //DataRead(itemDataPath, itemDatas);
        /*
        ItemDataRead("EquipData", itemDatas);
        ItemDataRead("MaterialData", itemDatas);
        ItemDataRead("ConsumData", itemDatas);*/
    }
    void ReadStringData()
    {
        IntKeyReadToString(effectData,effectDataPath);
        IntKeyReadToString(consumItemData, consumItemDataPath);
        IntKeyReadToString(equipItemData, equipItemDataPath);
        //ReadToString(gameConfigData, gameConfigDataPath);
        IntKeyReadToString(harvestData, harvestDataPath);
        IntKeyReadToString(harvestItemData,harvestItemDataPath);
        IntKeyReadToString(materialItemData, materialDataPath);
        IntKeyReadToString(readySoilItemData, readySoilItemDataPath);
        IntKeyReadToString(soilItemData, soilItemDataPath);
        IntKeyReadToString(toolData,toolDataPath);
    }
    void ReadMultiKey()
    {
        LoadMultiKey(shopDataPath,shopData);
        LoadMultiKey(equipItemDataPath,equipStatDatas);
        MultiObToInt(shopData,shops);
        MultiObToInt(equipStatDatas,equipStat);
    }
    void ReadDropTable()
    {
        DropTableRead(dropTablePath,dropTable);
    }
    #region 파일이름세팅
    void SetFileNames()
    {
        DataNameRead();
        SetFileNameField();
    }

    void KeyStringDataRead(Dictionary<string,Dictionary<string,string>> dataDict,string dataPath)
    {
        List<Dictionary<string, object>> temp = csvReader.Read(dataPath);

        for(int i = 0; i < temp.Count; i++)
        {
            Dictionary<string, object> tempDict = temp[i];

            foreach(string key in tempDict.Keys)
            {
                string data = tempDict[key].ToString();
                string dataKey = temp[i]["key"].ToString();

            }

        }


    }

    void DataNameRead()
    {
        List<Dictionary<string, object>> temp = csvReader.Read(fileNamePath);
        for (int i = 0; i < temp.Count; i++)
        {
            Dictionary<string, string> tempDict = new Dictionary<string, string>();
            foreach (string keys in temp[i].Keys)
            {
                
                if (keys == "#주석#")
                {
                    return;
                }
                tempDict.Add(keys, temp[i][keys].ToString());
            }
            fileNames.Add(tempDict);
        }
    }
    void SetFileNameField()
    {
        if(dataPath == null)
        {
            dataPath = "DataTables\\"; 
        }
        for(int i = 0; i <fileNames.Count;i++)
        {
            if(fileNames[i]["fieldName"] == nameof(dropTablePath))
            {
                dropTablePath = fileNames[i]["folder"]+fileNames[i]["filename"];
            }
            if (fileNames[i]["fieldName"] == nameof(harvestItemDataPath))
            {
                harvestItemDataPath = fileNames[i]["folder"] + fileNames[i]["filename"];
            }
            if (fileNames[i]["fieldName"] == nameof(shopDataPath))
            {
                shopDataPath = fileNames[i]["folder"] + fileNames[i]["filename"];
            }
            if (fileNames[i]["fieldName"] == nameof(stringDataPath))
            {
                stringDataPath = fileNames[i]["folder"] + fileNames[i]["filename"];
            }
            if (fileNames[i]["fieldName"] == nameof(consumItemDataPath))
            {
                consumItemDataPath = fileNames[i]["folder"] + fileNames[i]["filename"];
            }
            if (fileNames[i]["fieldName"] == nameof(equipItemDataPath))
            {
                equipItemDataPath = fileNames[i]["folder"] + fileNames[i]["filename"];
            }
            if (fileNames[i]["fieldName"] == nameof(equipentsStatTableDataPath))
            {
                equipentsStatTableDataPath = fileNames[i]["folder"] + fileNames[i]["filename"];
            }
            if (fileNames[i]["fieldName"] == nameof(harvestDataPath))
            {
                harvestDataPath = fileNames[i]["folder"] + fileNames[i]["filename"];
            }
            if (fileNames[i]["fieldName"] == nameof(itemDataPath))
            {
                itemDataPath = fileNames[i]["folder"] + fileNames[i]["filename"];
            }
            if (fileNames[i]["fieldName"] == nameof(materialDataPath))
            {
                materialDataPath = fileNames[i]["folder"] + fileNames[i]["filename"];
            }
            if (fileNames[i]["fieldName"] == nameof(readySoilItemDataPath))
            {
                readySoilItemDataPath = fileNames[i]["folder"] + fileNames[i]["filename"];
            }
            if (fileNames[i]["fieldName"] == nameof(soilItemDataPath))
            {
                soilItemDataPath = fileNames[i]["folder"] + fileNames[i]["filename"];
            }
            if (fileNames[i]["fieldName"] == nameof(toolDataPath))
            {
                toolDataPath = fileNames[i]["folder"] + fileNames[i]["filename"];
            }


        }
    }

    #endregion
    #region 멀티키 리드
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
    #endregion
    #region 드롭테이블 리드
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
    #endregion
    #region 멀티키 인트형식 리드
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
    #endregion
    void DataRead(string path, DataClass newData)
    {
        List<Dictionary<string, object>> tempDataList = new List<Dictionary<string, object>>();
        if(path == null)
        {
            Debug.Log("data is Null");
        }
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
        if(tempDatas == null)
        {
            Debug.Log($"Data Error {path}");
            return;
        }
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
    void IntKeyReadToString(Dictionary<int,Dictionary<string,string>>data , string dataPath)
    {
        List<Dictionary<string, object>> temp = csvReader.Read(dataPath);

        for(int i = 0; i < temp.Count; i++)
        {
            int index;
            if (int.TryParse(temp[i]["id"].ToString(),out index) )
            {
                Dictionary<string, object> tempOrigin = temp[i];
                Dictionary<string, string> tempData = new Dictionary<string, string>();

                foreach(string key in temp[i].Keys)
                {
                    tempData.Add(key, temp[i][key].ToString());

                }
                data.Add(index, tempData);

            }
        }

    }
    void ReadGameConfig(string dataPath)
    {
        List<Dictionary<string, object>> temp = csvReader.Read(dataPath);

        for(int i = 0; i< temp.Count; i++)
        {
            Dictionary<string, string> tempData = new Dictionary<string, string>();
            string dataKey = null;
            foreach(string key in temp[i].Keys)
            {
                if(key == "key")
                {
                    dataKey = temp[i][key].ToString();
                }
                tempData.Add(key, temp[i][key].ToString());

            }
            if(dataKey != null)
            {
                gameConfigData.Add(dataKey, tempData);
            }
            else
            {
                Debug.Log("GameConfig Key Null");
            }

        }
    }

}
