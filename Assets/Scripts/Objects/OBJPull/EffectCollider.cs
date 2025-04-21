using UnityEngine;
using System;
using System.Collections.Generic;
public class EffectCollider : MonoBehaviour
{
    List<GameObject> inCollOBJ = new List<GameObject>();
    [SerializeField]EffectPull effectPuller;
    Vector3 targetPos;
    Vector3 centerPos;
    Vector3 boxSize;
    private void OnDisable()
    {
        inCollOBJ.Clear();
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log(this.gameObject.transform.position);
        }
        
    }
    #region 콜라이더 생성

    
    #endregion
    void CalculateCenterPos()
    {

    }
    public void ReSetCollObjs()
    {
        inCollOBJ.Clear();
    }
    public List<GameObject> GetObjList()
    {
        Debug.Log($"GetList {inCollOBJ.Count}");
        return inCollOBJ;
    }
    public EffectPull GetEffectPuller()
    {
        return effectPuller;
    }

    public void ObjectCjeck(GameObject go)
    {
        if (!inCollOBJ.Contains(go))
        {
            inCollOBJ.Remove(go);
        }
    }
    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (!inCollOBJ.Contains(collision.gameObject))
        {
            Debug.Log($"Added {collision.gameObject.name}");
            inCollOBJ.Add(collision.gameObject);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (inCollOBJ.Contains(collision.gameObject))
        {
            Debug.Log($"Removed{collision.gameObject.name}");
            inCollOBJ.Remove(collision.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($" Triiger Enter {other.gameObject.name}");
        if (!inCollOBJ.Contains(other.gameObject))
        {
            Debug.Log($"Added {other.gameObject.name}");
            inCollOBJ.Add(other.gameObject);
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (!inCollOBJ.Contains(other.gameObject))
        {
            inCollOBJ.Add(other.gameObject);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (inCollOBJ.Contains(other.gameObject))
        {
            Debug.Log($"Removed{other.gameObject.name}");
            inCollOBJ.Remove(other.gameObject);
        }
    }
    */
}
