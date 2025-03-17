using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class ColliderManager : MonoBehaviour
{
    UnityEvent colliderEvent;
    GameObject colliderPrefab;
    List<GameObject> colliders;

    LayerMask interactLayer;


}
