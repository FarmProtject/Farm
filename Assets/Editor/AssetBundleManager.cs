
using UnityEditor;
using System.IO;

public class AssetBundleBuildManager
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

}