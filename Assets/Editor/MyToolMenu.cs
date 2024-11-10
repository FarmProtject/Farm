
using UnityEditor;
using System.IO;
using UnityEngine;
public class MyToolMenu
{
    [MenuItem("Mytool/AssetBundle Build")]

    public static void AssetBundleBuild()
    {
        string directory = "./AssetBundle";
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        BuildPipeline.BuildAssetBundles(directory, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);

        EditorUtility.DisplayDialog("에섯번들 빌드", "에셋 번들 빌드 완료했습니다", "완료");
    }
    [MenuItem("Mytool/TileMap")]

    public static void CerateTileMap()
    {
        
    }

}