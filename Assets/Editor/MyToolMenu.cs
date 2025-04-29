using UnityEditor;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class MyToolMenu
{
    [MenuItem("MyTool/Build AssetBundles")]
    public static void BuildAllAssetBundles()
    {
        string outputPath = "Assets/AssetBundle";

        if (!Directory.Exists(outputPath))
        {
            Directory.CreateDirectory(outputPath);
        }

        List<AssetBundleBuild> bundleBuilds = new List<AssetBundleBuild>();

        // 1. Material 번들
        string[] materialGuids = AssetDatabase.FindAssets("t:Material", new[] { "Assets/Textures/Materials" });
        List<string> materialPaths = new List<string>();

        foreach (string guid in materialGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            materialPaths.Add(path);
        }

        bundleBuilds.Add(new AssetBundleBuild
        {
            assetBundleName = "materials.bundle",
            assetNames = materialPaths.ToArray()
        });

        // 2. Mesh 번들
        string[] meshGuids = AssetDatabase.FindAssets("t:Mesh", new[] { "Assets/Textures/Meshs" });
        List<string> meshPaths = new List<string>();

        foreach (string guid in meshGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            meshPaths.Add(path);
        }

        bundleBuilds.Add(new AssetBundleBuild
        {
            assetBundleName = "meshes.bundle",
            assetNames = meshPaths.ToArray()
        });

        // 번들 빌드
        BuildPipeline.BuildAssetBundles(outputPath, bundleBuilds.ToArray(), BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);

        EditorUtility.DisplayDialog("AssetBundle 빌드", "모든 번들 빌드 완료!", "확인");
    }
}