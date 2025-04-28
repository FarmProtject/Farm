using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class AssetBundleLoader : MonoBehaviour
{


    IEnumerator LoadAsset(string path,string name)
    {

        // ���� ���� ���
        string bundlePath = Application.streamingAssetsPath + "/Assets/Textures"+path;

        // �񵿱�� AssetBundle �ε�
        AssetBundleCreateRequest bundleRequest = AssetBundle.LoadFromFileAsync(bundlePath);
        yield return bundleRequest;

        AssetBundle bundle = bundleRequest.assetBundle;

        if (bundle == null)
        {
            Debug.LogError("Failed to load AssetBundle!");
            yield break;
        }

        // ���� �ε�
        AssetBundleRequest assetRequest = bundle.LoadAssetAsync<GameObject>("MyPrefabName"); // ���� �̸� ��Ȯ�� �Է�
        yield return assetRequest;

        GameObject prefab = assetRequest.asset as GameObject;
        if (prefab != null)
        {
            Instantiate(prefab);
        }

        // �� ������ ����
        bundle.Unload(false); // false�� �޸𸮿� �ε��� ������ ��������

    }
}
