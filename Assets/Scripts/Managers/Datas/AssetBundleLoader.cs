using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class AssetBundleLoader : MonoBehaviour
{


    IEnumerator LoadAsset(string path,string name)
    {

        // 로컬 파일 경로
        string bundlePath = Application.streamingAssetsPath + "/Assets/Textures"+path;

        // 비동기로 AssetBundle 로드
        AssetBundleCreateRequest bundleRequest = AssetBundle.LoadFromFileAsync(bundlePath);
        yield return bundleRequest;

        AssetBundle bundle = bundleRequest.assetBundle;

        if (bundle == null)
        {
            Debug.LogError("Failed to load AssetBundle!");
            yield break;
        }

        // 에셋 로드
        AssetBundleRequest assetRequest = bundle.LoadAssetAsync<GameObject>("MyPrefabName"); // 에셋 이름 정확히 입력
        yield return assetRequest;

        GameObject prefab = assetRequest.asset as GameObject;
        if (prefab != null)
        {
            Instantiate(prefab);
        }

        // 다 썼으면 해제
        bundle.Unload(false); // false면 메모리에 로드한 에셋은 남아있음

    }
}
