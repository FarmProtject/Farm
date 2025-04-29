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
        AssetLoad();

    }

    public void AssetLoad()
    {
        string bundlePath = Path.Combine(Application.dataPath, "AssetBundle");

        meshBundle = AssetBundle.LoadFromFile(Path.Combine(bundlePath, "meshes.bundle"));
        materialBundle = AssetBundle.LoadFromFile(Path.Combine(bundlePath, "materials.bundle"));

        Debug.Log($"Mesh bundle : {meshBundle}");
        Debug.Log($"MateBundle : {materialBundle}");
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
            Debug.Log(meshBundle.LoadAsset<Mesh>(name));
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
            Debug.Log(materialBundle.LoadAsset<Material>(name));
            Material loadMate = materialBundle.LoadAsset<Material>(name);
            materialCache.Add(name, loadMate);
        }
        return materialCache[name];
    }

}
