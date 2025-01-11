using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FarmField : MonoBehaviour
{
    public Grid myGrid;
    public GameObject prefab;
    public GameObject mainCamera;

    void Start()
    {
        myGrid = transform.GetComponent<Grid>();
        mainCamera = GameObject.Find("CameraOBJ");
    }

    void Update()
    {
        OnClick();
    }

    void PlaceObject(Vector3Int gridPos)
    {
        Vector3 worldPos = myGrid.CellToWorld(gridPos);
        Instantiate(prefab, worldPos, Quaternion.identity);
        Debug.Log("TestOBj Instaniate");
    }
    void OnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                
                Vector3Int vector = Vector3Int.FloorToInt(hit.point);
                vector = new Vector3Int(vector.x, 0, vector.z);
                PlaceObject(vector);
                Debug.Log(vector);
            }
        }
    }

}
