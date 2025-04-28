using UnityEngine;
using System;
using System.Collections.Generic;
public class TextureManager : MonoBehaviour
{
    Dictionary<string, object> matarialDatas = new Dictionary<string, object>();

    Dictionary<string, object> TextureDatas = new Dictionary<string, object>();

    [SerializeField] string bundlePath;

    private void Awake()
    {

    }

    private void Start()
    {
        
    }

    
    void OnAwake()
    {
        bundlePath = Application.streamingAssetsPath + "/AssetBundle";
        
    }
    void OnStart()
    {

    }
    

}
