
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

        EditorUtility.DisplayDialog("�������� ����", "���� ���� ���� �Ϸ��߽��ϴ�", "�Ϸ�");
    }
    [MenuItem("Mytool/TileMap")]

    public static void CerateTileMap()
    {
        
    }

}