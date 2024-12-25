using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CameraMovement : MonoBehaviour,IMouseInput
{
    Transform myTr;
    GameManager gameManager;

    public Transform followTr;
    public float posx;
    public float posy;
    
    public float lookAngle = 30;
    public float minAngle = -30;
    public float maxAngle = 90;

    public float distance = 3;
    public float minDistance=0.5f;
    public float maxDistance = 8;
    public float wheelSpeed = 4f;

    public float rotationSpeed = 2;
    public float rotateAngle;
    public float smoothSpeed = 0.125f;

    private void Awake()
    {
        gameManager = GameManager.instance;
    }
    void Start()
    {
        myTr = this.transform;
        AddMouseInput();
    }
   
    void Update()
    {
        
        
    }
    void LookDistanceChange()
    {
        distance -= Input.GetAxis("Mouse ScrollWheel") * wheelSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
        
    }

    void VertRotate()
    {
        lookAngle += Input.GetAxis("Mouse Y") * rotationSpeed;
        lookAngle = Mathf.Clamp(lookAngle, minAngle, maxAngle);
    }

    void HoriRotate()
    {
        float y = distance * Mathf.Sin(Mathf.Deg2Rad * lookAngle);
        float r = distance * Mathf.Cos(Mathf.Deg2Rad * lookAngle);
        float x = r * Mathf.Cos(Mathf.Deg2Rad * rotateAngle);
        float z = r * Mathf.Sin(Mathf.Deg2Rad * rotateAngle);
        Vector3 targetPos = new Vector3(followTr.position.x + x, followTr.position.y + y, followTr.position.z + z);

        transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed);
        transform.LookAt(followTr);
    }

    
    public void OnMouseInput()
    {
        OnLeftClick();
        OnRightClick();
        OnMouseWheel();
    }

    public void OnLeftClick()
    {
        Debug.Log("OnLeftClick");
    }

    public void OnRightClick()
    {
        if (Input.GetMouseButton(1))
        {
            rotateAngle += Input.GetAxis("Mouse X") * rotationSpeed;
            //LookDistanceChange();
            VertRotate();
            HoriRotate();
            Debug.Log("CameraRightClick");
        }
        else
        {
            LookDistanceChange();
            HoriRotate();
        }
    }

    public void OnMouseWheel()
    {
        LookDistanceChange();
        if (distance!=Vector3.Distance(followTr.position,transform.position))
        {
            HoriRotate();
            Debug.Log("CameraWehel");
        }
    }

    public void AddMouseInput()
    {
        gameManager.mouseManager.mouseInput = this;
    }
}
