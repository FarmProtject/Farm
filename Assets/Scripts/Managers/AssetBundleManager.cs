using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
public class AssetBundleManager : MonoBehaviour
{

    public string meshPath;
    public string materialPath;

    private AssetBundle meshBundle;
    private AssetBundle materialBundle;

    private Dictionary<string, Mesh> meshCache = new Dictionary<string, Mesh>();
    private Dictionary<string, Material> materialCache = new Dictionary<string, Material>();

    private void Awake()
    {
        OnAwake();
    }

    void OnAwake()
    {
        string path = "AssetBundle/Meshes";
        meshPath = Path.Combine(Application.streamingAssetsPath, path);
        Debug.Log(meshPath);
        path = "AssetBundle/Materials";
        materialPath = Path.Combine(Application.streamingAssetsPath, path);
        Debug.Log(meshPath);
    }


    public Mesh LoadMeshBundle(int id)
    {
        DataManager dm = GameManager.instance.dataManager;
        string name = null;
        if (dm.textureMap.ContainsKey(id))
        {
            name = dm.textureMap[id]["model"].ToString();
        }
        if (name == null)
        {
            Debug.Log("name is null");
            return null;
        }
       
        if (!meshCache.ContainsKey(name))
        {
            string path = meshPath;// = Path.Combine(meshPath, name);
            meshBundle = AssetBundle.LoadFromFile(path);/*".bundle"*/

            Mesh loadedMesh = meshBundle.LoadAsset<Mesh>(name);
            meshCache.Add(name, loadedMesh);
        }
        return meshCache[name];
    }

    public Material LoadMaterial(int id)
    {
        DataManager dm = GameManager.instance.dataManager;
        string name = null;
        if (dm.textureMap.ContainsKey(id))
        {
            name = dm.textureMap[id]["material"].ToString();
            
        }
        if(name == null)
        {
            Debug.Log("name is null");
            return null;
        }
        if (!materialCache.ContainsKey(name))
        {
            string path = materialPath;//Path.Combine(materialPath, name);
            materialBundle = AssetBundle.LoadFromFile(path); // + ".bundle");
            Material loadMate = materialBundle.LoadAsset<Material>(name);
            materialCache.Add(name, loadMate);
        }
        return materialCache[name];
    }

}
