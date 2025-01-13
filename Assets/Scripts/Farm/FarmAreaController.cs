using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FarmAreaController : MonoBehaviour
{
    Grid myGrid;
    GameObject preViewObj;
    List<GameObject> poolObjs = new List<GameObject>();
    Camera mainCamera;
    [SerializeField]
    LayerMask layer;
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
    }

    void OBJFollow()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = mainCamera.nearClipPlane;
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit, 100, layer))
        {
            
            preViewObj.transform.localPosition= myGrid.WorldToCell(hit.point);
            Debug.Log(hit.point);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            preViewObj.SetActive(true);
            EventManager.instance.FarmAreaEvents.AddListener(OBJFollow);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            preViewObj.SetActive(false);
            EventManager.instance.FarmAreaEvents.RemoveListener(OBJFollow);
        }
    }
}
