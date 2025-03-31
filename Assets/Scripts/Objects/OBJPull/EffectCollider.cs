using UnityEngine;
using System;
using System.Collections.Generic;
public class EffectCollider : MonoBehaviour
{
    List<GameObject> inCollOBJ = new List<GameObject>();

    private void OnDisable()
    {
        inCollOBJ.Clear();
    }

    public List<GameObject> GetObjList()
    {
        return inCollOBJ;
    }


    private void OnTriggerEnter(Collider other)
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
            inCollOBJ.Remove(other.gameObject);
        }
    }
}
