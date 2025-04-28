
using UnityEditor;
using System.IO;
using UnityEngine;
public class MyToolMenu
{
    [MenuItem("Mytool/AssetBundle Build")]

    public static void BuildSelectedAssetBundles()
    {
        string directory = "./Assets/AssetBundle";
        string materialFolder = Path.Combine(directory, "Materials");
        string meshFolder = Path.Combine(directory, "Meshes");
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        if (!Directory.Exists(materialFolder))
        {
            Directory.CreateDirectory(materialFolder);
        }
        if (!Directory.Exists(meshFolder))
        {
            Directory.CreateDirectory(meshFolder);
        }
        // 1. 매터리얼 번들 빌드

        string[] materialAssets = AssetDatabase.FindAssets("t:Material", new[] { "Assets/Textures/Materials" });

        foreach (string guid in materialAssets)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);

            AssetBundleBuild build = new AssetBundleBuild
            {
                assetBundleName = Path.GetFileNameWithoutExtension(path).ToLower() + ".bundle",
                assetNames = new[] { path }
            };
            BuildPipeline.BuildAssetBundles(materialFolder, new[] { build }, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
        }

        string[] meshAssets = AssetDatabase.FindAssets("t:Mesh", new[] { "Assets/Textures/Meshs" });
        foreach (string guid in meshAssets)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Mesh mesh = AssetDatabase.LoadAssetAtPath<Mesh>(path);

            AssetBundleBuild build = new AssetBundleBuild
            {
                assetBundleName = Path.GetFileNameWithoutExtension(path).ToLower() + ".bundle",
                assetNames = new[] { path }
            };

            BuildPipeline.BuildAssetBundles(meshFolder, new[] { build }, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
        }

        EditorUtility.DisplayDialog("에섯번들 빌드", "에셋 번들 빌드 완료했습니다", "완료");
    }
}
