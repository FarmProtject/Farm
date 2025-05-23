using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FarmAreaController : MonoBehaviour
{

    DataManager dataManager;

    Grid myGrid;
    [SerializeField]GameObject preViewObj;
    [SerializeField]GameObject soilPrefab;
    List<GameObject> poolObjs = new List<GameObject>();
    Camera mainCamera;
    
    public bool canBuild;
    [SerializeField]
    public LayerMask rayLayer;
    Dictionary<Vector3, GameObject> farmObjs = new Dictionary<Vector3, GameObject>();
    List<Vector3> farmPosList = new List<Vector3>();
    void Start()
    {
        SetUp();
    }

    void Update()
    {
        
    }

    void SetUp()
    {
        myGrid = transform.GetComponent<Grid>();
        preViewObj = GameObject.Find("FarmObjPreView");
        preViewObj.SetActive(false);
        mainCamera = Camera.main;
        if(dataManager == null)
        {
            dataManager = GameManager.instance.dataManager;
        }
        SetGridObjects();
    }
    #region 그리드
    public Vector3 GetGridPosition(LayerMask layer)
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = mainCamera.nearClipPlane;
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 30, layer))
        {
            mousePos = myGrid.WorldToCell(hit.point);
            canBuild = true;
            return mousePos;
        }
        else
        {
            canBuild = false;
            return mousePos;
        }
        
    }
    private void SetGridObjects()
    {
        if (soilPrefab == null)
        {
            Debug.Log("soilPrefab is Null");
            return;
        }

        Renderer rend = GetComponent<Renderer>();
        Vector3 planeSize = rend.bounds.size; // FarmArea의 크기 (world 기준)
        Vector3 planeOrigin = transform.position - new Vector3(planeSize.x / 2, 0, planeSize.z / 2); // Plane의 시작 지점

        // Soil prefab의 크기
        Vector3 prefabSize = soilPrefab.GetComponent<Renderer>().bounds.size;

        int countX = Mathf.FloorToInt(planeSize.x / prefabSize.x);
        int countZ = Mathf.FloorToInt(planeSize.z / prefabSize.z);

        for (int x = 0; x < countX; x++)
        {
            for (int z = 0; z < countZ; z++)
            {
                GameObject go = Instantiate(soilPrefab);
                go.transform.SetParent(this.transform);

                // 정확한 위치 계산
                float posX = planeOrigin.x + x * prefabSize.x + prefabSize.x / 2f;
                float posZ = planeOrigin.z + z * prefabSize.z + prefabSize.z / 2f;

                go.transform.position = new Vector3(posX, transform.position.y, posZ);
                farmObjs.Add(go.transform.position, go);
                Vector3 farmMap = new Vector3(go.transform.position.x, 0, go.transform.position.z);
                farmPosList.Add(farmMap);
            }
        }
        foreach(Vector3 key in farmPosList)
        {
            //Debug.Log(key);
        }
    }
    #endregion
    #region 팜데이터생성


    
    #endregion



    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {

    }
}
