using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Reflection;


public class DataClass
{
    public Dictionary<int, Dictionary<string, object>> datas = new Dictionary<int, Dictionary<string, object>>();
}

public class StringKeyDatas
{
    public Dictionary<string, object> datas = new Dictionary<string, object>();
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

public class DataManager : MonoBehaviour
{
    public string dataPath { get; private set; } = "DataTables\\";

    [SerializeField, ReadOnly] private string stringDataPath;
    public Dictionary<string, Dictionary<string, object>> stringDatas = new Dictionary<string, Dictionary<string, object>>();

    [SerializeField, ReadOnly] private string fileNamePath;
    public List<Dictionary<string, string>> fileNames = new List<Dictionary<string, string>>();

    [SerializeField, ReadOnly] private string itemDataPath;
    public DataClass itemDatas = new DataClass();
    public Dictionary<int, ItemBase> items = new Dictionary<int, ItemBase>();

    [SerializeField, ReadOnly] private string effectDataPath;
    public Dictionary<int, Dictionary<string, object>> effectData = new Dictionary<int, Dictionary<string, object>>();


    [SerializeField, ReadOnly] private string equipentsStatTableDataPath;
    public Dictionary<int, List<StringKeyDatas>> equipStatDatas = new Dictionary<int, List<StringKeyDatas>>();
    public Dictionary<int, Dictionary<string, int>> equipStat = new Dictionary<int, Dictionary<string, int>>();

    [SerializeField, ReadOnly] private string gameConfigDataPath;
    public string GameConfigDataPath
    {
        get => $"{dataPath}{gameConfigDataPath}";
        set => gameConfigDataPath = value.Replace(dataPath, "");
    }

    public Dictionary<string, Dictionary<string, string>> gameConfigData = new Dictionary<string, Dictionary<string, string>>();
    [SerializeField, ReadOnly] private string consumItemDataPath;
    public Dictionary<int, Dictionary<string, object>> consumItemData = new Dictionary<int, Dictionary<string, object>>();

    [SerializeField, ReadOnly] private string equipItemDataPath;
    public Dictionary<int, Dictionary<string, object>> equipItemData = new Dictionary<int, Dictionary<string, object>>();

    [SerializeField, ReadOnly] private string farmingItemDataPath;
    public Dictionary<int, Dictionary<string, object>> farmingItemData = new Dictionary<int, Dictionary<string, object>>();
    /*
    [SerializeField, ReadOnly] private string materialItemDataPath;
    public Dictionary<int, Dictionary<string, string>> materialItemData = new Dictionary<int, Dictionary<string, string>>();
    */
    [SerializeField, ReadOnly] string toolItemDataPath;
    public Dictionary<int, Dictionary<string, object>> toolItemData = new Dictionary<int, Dictionary<string, object>>();

    [SerializeField, ReadOnly] string harvestDataPath;
    public Dictionary<string, Dictionary<string, StringKeyDatas>> harvestData = new Dictionary<string, Dictionary<string, StringKeyDatas>>();
    public Dictionary<string, Dictionary<string, StringKeyDatas>> harvestToLevel = new Dictionary<string, Dictionary<string, StringKeyDatas>>();

    [SerializeField, ReadOnly] private string shopDataPath;
    public string ShopDataPath
    {
        get => $"{dataPath}{shopDataPath}";
        set => shopDataPath = value.Replace(dataPath, "");
    }

    public Dictionary<int, List<StringKeyDatas>> shopData = new Dictionary<int, List<StringKeyDatas>>();
    public Dictionary<int, Dictionary<string, int>> shops = new Dictionary<int, Dictionary<string, int>>();

    [SerializeField, ReadOnly] private string soilItemDataPath;
    public string SoilItemDataPath
    {
        get => $"{dataPath}{soilItemDataPath}";
        set => soilItemDataPath = value.Replace(dataPath, "");
    }

    [SerializeField, ReadOnly] string dropTablePath;
    public Dictionary<string, List<DropTable>> dropTable = new Dictionary<string, List<DropTable>>();

    public Dictionary<string, EffectBase> effectBases = new Dictionary<string, EffectBase>();

    public string texturePath;
    public Dictionary<int, Dictionary<string, object>> textureMap = new Dictionary<int, Dictionary<string, object>>();
    public string matarialPath;
    public string mashPath;
    private CSVReader csvReader = new CSVReader();

    
    private void Awake()
    {
        OnAwake();
    }

    private void OnAwake()
    {
        SetFileNames();
        //FileNameDebug();
        AllDataRead();
        StringKeyRead(stringDataPath);
        ReadStringData();
        ReadMultiKey();
        ResisteAllEffects();
        //StringKeyDebug();
        //DebugEffects();
        //DebugItemData();
    }

    void AllDataRead()
    {
        DataRead(itemDataPath, itemDatas);
        ReadGameConfig(gameConfigDataPath);
       
        /*
        ItemDataRead("EquipData", itemDatas);
        ItemDataRead("MaterialData", itemDatas);
        ItemDataRead("ConsumData", itemDatas);*/
    }
    void ReadStringData()
    {

        //DebugEffcetDict();
        //DebugEffcetDict();
        IntKeyReadToObject(effectData, effectDataPath);

        IntKeyReadToObject(consumItemData, consumItemDataPath);
        IntKeyReadToObject(equipItemData, equipItemDataPath);
        IntKeyReadToObject(toolItemData, toolItemDataPath);
        IntKeyReadToObject(farmingItemData, farmingItemDataPath);
        IntKeyReadToObject(textureMap, texturePath);
        //IntKeyReadToString(materialItemData, materialItemDataPath);
    }
    void ReadMultiKey()
    {
        LoadMulti(shopDataPath, shopData);
        LoadMulti(equipentsStatTableDataPath, equipStatDatas);
        TwoKeyLoad(harvestDataPath, harvestData);
        HarvestToLevel(harvestDataPath, harvestToLevel);
        //DebugStats();
        //HarvestDataDebug();
        //MultiObToInt(shopData, shops);
        //MultiObToInt(equipStatDatas, equipStat);
    }
    #region 이펙트 
    void ResisteAllEffects()
    {//이펙트 딕셔너리 초기화
        var effectTypes = Assembly.GetExecutingAssembly()
                                  .GetTypes()
                                  .Where(t => t.IsSubclassOf(typeof(EffectBase)) && t.GetCustomAttribute<AutoRegisterEffect>() != null)
                                  .ToList();
        foreach (var type in effectTypes)
        {
            string name = type.GetType().Name;
            var effect = (EffectBase)Activator.CreateInstance(type);
        }
    }
    public void AddItemEffectDatas(string key, EffectBase effect)
    {
        if (!effectBases.ContainsKey(key))
        {
            effectBases.Add(key, effect);
        }
    }
    #endregion
    #region debugs
    void DebugItemData()
    {
        foreach (int id in itemDatas.datas.Keys)
        {
            foreach (string key in itemDatas.datas[id].Keys)
            {
                Debug.Log($"item id {id}  itemdataKey {key}  itemdata {itemDatas.datas[id][key]}");
            }
        }
    }
    void DebugEffects()
    {
        foreach (string name in effectBases.Keys)
        {
            Debug.Log($"effectName : {name}");
        }
    }
    void DebugStats()
    {
        foreach (int index in equipStatDatas.Keys)
        {
            for (int i = 0; i < equipStatDatas[index].Count; i++)
            {
                foreach (string key in equipStatDatas[index][i].datas.Keys)
                {
                    Debug.Log($"StatType : {key}  Value : {equipStatDatas[index][i].datas[key]}");

                }

            }
        }
    }
    void DebugEffcetDict()
    {
        List<Dictionary<string, object>> temp = csvReader.Read(effectDataPath);
        for (int i = 0; i < effectData.Count; i++)
        {
            foreach (string key in temp[i].Keys)
            {
                Debug.Log($"Key : {key}   value : {temp[i][key]}");
            }
        }

    }
    
    void ItemDataDebug()
    {
        foreach (int index in itemDatas.datas.Keys)
        {
            foreach (string key in itemDatas.datas[index].Keys)
            {
                Debug.Log(itemDatas.datas[index][key]);
            }

        }
    }
    void StringKeyDebug()
    {
        foreach (string key in stringDatas.Keys)
        {
            foreach (string lg in stringDatas[key].Keys)
            {
                Debug.Log($" key : {key}   lng : {lg}    valuy : {stringDatas[key][lg]}");
            }


        }
    }
    void DebugTwoKeyCropData()
    {
        Debug.Log($"TwoKeyDebug    {harvestData.Count}");

        foreach (string groupid in harvestData.Keys)
        {
            Debug.Log($"Groupid : {groupid}");
            foreach (string id in harvestData[groupid].Keys)
            {
                foreach (string key in harvestData[groupid][id].datas.Keys)
                {
                    Debug.Log($"GroupId {groupid} ,  ID {id} , Key : {key}  ,Value {harvestData[groupid][id].datas[key].ToString()}");
                }

            }
        }
    }
    #endregion
    void ReadDropTable()
    {
        DropTableRead(dropTablePath, dropTable);
    }
    #region 파일이름세팅
    void SetFileNames()
    {
        DataNameRead();
        SetFileNameField();
    }
    void FileNameDebug()
    {
        for (int i = 0; i < fileNames.Count; i++)
        {
            foreach (string key in fileNames[i].Keys)
            {

                Debug.Log($"key : {key} value : {fileNames[i][key]}");


            }
        }
    }

    void KeyStringDataRead(Dictionary<string, Dictionary<string, string>> dataDict, string dataPath)
    {
        List<Dictionary<string, object>> temp = csvReader.Read(dataPath);

        for (int i = 0; i < temp.Count; i++)
        {
            Dictionary<string, object> tempDict = temp[i];

            foreach (string key in tempDict.Keys)
            {
                string data = tempDict[key].ToString();
                string dataKey = temp[i]["key"].ToString();

            }

        }


    }

    void DataNameRead()
    {

        fileNamePath = "FilePath";

        List<Dictionary<string, object>> temp = csvReader.Read(fileNamePath);
        for (int i = 0; i < temp.Count; i++)
        {
            Dictionary<string, string> tempDict = new Dictionary<string, string>();
            foreach (string keys in temp[i].Keys)
            {

                if (keys == "#주석#")
                {
                    continue;
                }
                tempDict.Add(keys, temp[i][keys].ToString());
            }
            fileNames.Add(tempDict);
        }
    }

    void SetFileNameField()
    {
        if (dataPath == null)
        {
            dataPath = "DataTables\\";
        }
        for (int i = 0; i < fileNames.Count; i++)
        {
            if (fileNames[i]["fieldName"] == nameof(dropTablePath))
            {
                dropTablePath = dataPath + fileNames[i]["folder"] + "\\" + fileNames[i]["filename"];
            }
            if (fileNames[i]["fieldName"] == nameof(shopDataPath))
            {
                shopDataPath = dataPath + fileNames[i]["folder"] + "\\" + fileNames[i]["filename"];
            }
            if (fileNames[i]["fieldName"] == nameof(stringDataPath))
            {
                stringDataPath = dataPath + fileNames[i]["folder"] + "\\" + fileNames[i]["filename"];
            }

            if (fileNames[i]["fieldName"] == nameof(equipentsStatTableDataPath))
            {
                equipentsStatTableDataPath = dataPath + fileNames[i]["folder"] + "\\" + fileNames[i]["filename"];
            }
            if (fileNames[i]["fieldName"] == nameof(harvestDataPath))
            {
                harvestDataPath = dataPath + fileNames[i]["folder"] + "\\" + fileNames[i]["filename"];
            }
            if (fileNames[i]["fieldName"] == nameof(itemDataPath))
            {
                itemDataPath = dataPath + fileNames[i]["folder"] + "\\" + fileNames[i]["filename"];
            }

            if (fileNames[i]["fieldName"] == nameof(farmingItemDataPath))
            {
                farmingItemDataPath = dataPath + fileNames[i]["folder"] + "\\" + fileNames[i]["filename"];
            }
            if (fileNames[i]["fieldName"] == nameof(equipItemDataPath))
            {
                equipItemDataPath = dataPath + fileNames[i]["folder"] + "\\" + fileNames[i]["filename"];
            }
            if (fileNames[i]["fieldName"] == nameof(consumItemDataPath))
            {
                consumItemDataPath = dataPath + fileNames[i]["folder"] + "\\" + fileNames[i]["filename"];
            }
            if (fileNames[i]["fieldName"] == nameof(toolItemDataPath))
            {
                toolItemDataPath = dataPath + fileNames[i]["folder"] + "\\" + fileNames[i]["filename"];
            }
            if(fileNames[i]["fieldName"] == nameof(texturePath))
            {
                texturePath = dataPath + fileNames[i]["folder"] + "\\" + fileNames[i]["filename"];
            }
            /*
            if (fileNames[i]["fieldName"] == nameof(materialItemDataPath))
            {
                materialItemDataPath = dataPath + fileNames[i]["folder"] + "\\" + fileNames[i]["filename"];
            }
            */


            if (fileNames[i]["fieldName"] == nameof(gameConfigDataPath))
            {
                gameConfigDataPath = dataPath + fileNames[i]["folder"] + "\\" + fileNames[i]["filename"];
            }
            if (fileNames[i]["fieldName"] == nameof(effectDataPath))
            {
                effectDataPath = dataPath + fileNames[i]["folder"] + "\\" + fileNames[i]["filename"];
            }

        }
    }

    #endregion
    #region 멀티키 리드

    void LoadMulti(string path, Dictionary<int, List<StringKeyDatas>> newData)
    {
        List<Dictionary<string, object>> tempList = csvReader.Read(path);
        // 그룹ID를 인덱스로 가지는 데이터 리스트
        for (int i = 0; i < tempList.Count; i++)
        {
            List<StringKeyDatas> datas = new List<StringKeyDatas>();
            Dictionary<string, object> temp = tempList[i];

            int index;

            if (temp.ContainsKey("groupId") && int.TryParse(temp["groupId"].ToString(), out index))
            {
                StringKeyDatas keyData = new StringKeyDatas();
                keyData.datas = temp;
                if (newData.ContainsKey(index))
                {
                    newData[index].Add(keyData);
                }
                else
                {
                    List<StringKeyDatas> dataList = new List<StringKeyDatas>();
                    dataList.Add(keyData);
                    newData.Add(index, dataList);
                }
            }
            else if (temp.ContainsKey("id") && int.TryParse(temp["id"].ToString(), out index))
            {
                StringKeyDatas keyData = new StringKeyDatas();
                keyData.datas = temp;
                if (newData.ContainsKey(index))
                {
                    newData[index].Add(keyData);
                }
                else
                {
                    List<StringKeyDatas> dataList = new List<StringKeyDatas>();
                    dataList.Add(keyData);
                    newData.Add(index, dataList);
                }

            }
        }
    }
    void TwoKeyLoad(string path, Dictionary<string, Dictionary<string, StringKeyDatas>> dictData)
    {
        List<Dictionary<string, object>> tempList = new List<Dictionary<string, object>>();
        tempList = csvReader.Read(path);
        Debug.Log("TwoKeyLaoad");
        for (int i = 0; i < tempList.Count; i++)
        {
            Dictionary<string, object> temp = tempList[i];
            StringKeyDatas keyData = new StringKeyDatas();
            Dictionary<string, StringKeyDatas> idDict = new Dictionary<string, StringKeyDatas>();

            keyData.datas = temp;
            string groupid = temp["groupid"].ToString();
            string id = temp["id"].ToString();

            idDict.Add(id, keyData);

            if (!dictData.ContainsKey(groupid))
            {
                dictData.Add(groupid, idDict);
            }
            else if (dictData.ContainsKey(groupid) && !dictData[groupid].ContainsKey(id))
            {
                dictData[groupid].Add(id, keyData);
            }
            else
            {
                Debug.Log("key Error");
            }

        }
    }
    void HarvestToLevel(string path, Dictionary<string,Dictionary<string,StringKeyDatas>> dictData)
    {
        List<Dictionary<string, object>> tempList = new List<Dictionary<string, object>>();
        tempList = csvReader.Read(path);
        Debug.Log("TwoKeyLaoad");
        for (int i = 0; i < tempList.Count; i++)
        {
            Dictionary<string, object> temp = tempList[i];
            StringKeyDatas keyData = new StringKeyDatas();
            Dictionary<string, StringKeyDatas> idDict = new Dictionary<string, StringKeyDatas>();

            keyData.datas = temp;
            string groupid = temp["groupid"].ToString();
            string level = temp["level"].ToString();

            idDict.Add(level, keyData);

            if (!dictData.ContainsKey(groupid))
            {
                dictData.Add(groupid, idDict);
            }
            else if (dictData.ContainsKey(groupid) && !dictData[groupid].ContainsKey(level))
            {
                dictData[groupid].Add(level, keyData);
            }
            else
            {
                Debug.Log("key Error");
            }

        }
    }
    /*
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
                        if(key == "id")
                        {
                            continue;
                        }
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
    */
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
    void MultiObToInt(Dictionary<int, Dictionary<string, object>> origin, Dictionary<int, Dictionary<string, int>> newData)
    {//장비 스탯데이터 읽어 온 후 int형식으로 변환
        Dictionary<int, Dictionary<string, object>> data = origin;
        Dictionary<string, int> temp = new Dictionary<string, int>();
        foreach (int index in equipStatDatas.Keys)
        {

            foreach (string key in data[index].Keys)
            {
                int stat;
                if (!temp.ContainsKey(key) && int.TryParse(data[index][key].ToString(), out stat))
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
        if (path == null)
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
                if (newData.datas.ContainsKey(index))
                {
                    //Debug.Log($"{index}");
                    //ItemDataDebug();
                    continue;
                }
                newData.datas.Add(index, temp);

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
        if (tempDatas == null)
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
    void IntKeyReadToObject(Dictionary<int, Dictionary<string, object>> data, string dataPath)
    {
        List<Dictionary<string, object>> temp = csvReader.Read(dataPath);
        if(temp == null)
        {
            Debug.Log($" Data null {dataPath}");
            return;
        }
        for (int i = 0; i < temp.Count; i++)
        {
            int index;
            if (int.TryParse(temp[i]["id"].ToString(), out index))
            {
                Dictionary<string, object> tempOrigin = temp[i];
                //Dictionary<string, string> tempData = new Dictionary<string, string>();
                /*
                foreach (string key in temp[i].Keys)
                {
                    tempData.Add(key, temp[i][key].ToString());

                }*/
                data.Add(index, tempOrigin);

            }
        }

    }
    void ReadGameConfig(string dataPath)
    {
        List<Dictionary<string, object>> temp = csvReader.Read(dataPath);

        for (int i = 0; i < temp.Count; i++)
        {
            Dictionary<string, string> tempData = new Dictionary<string, string>();
            string dataKey = null;
            foreach (string key in temp[i].Keys)
            {
                if (key == "key")
                {
                    dataKey = temp[i][key].ToString();
                }
                tempData.Add(key, temp[i][key].ToString());

            }
            if (dataKey != null)
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
