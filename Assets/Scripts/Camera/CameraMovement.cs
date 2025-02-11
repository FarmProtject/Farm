using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CameraWheel : IClickAction
{
    private readonly CameraMovement cameraMove;

    public CameraWheel(CameraMovement cameraMove)
    {
        this.cameraMove = cameraMove;
    }
    public void Invoke()
    {
        cameraMove.OnMouseWheel();
    }
}

public class CameraRightClick : IClickAction
{
    private readonly CameraMovement cameraMove;

    public CameraRightClick(CameraMovement cameraMove)
    {
        this.cameraMove = cameraMove;
    }
    public void Invoke()
    {

        cameraMove.OnRightClick();
    }
}
public class CameraLeftClick : IClickAction
{
    private readonly CameraMovement cameraMove;

    public CameraLeftClick(CameraMovement cameraMove)
    {
        this.cameraMove = cameraMove;
    }
    public void Invoke()
    {
        cameraMove.OnLeftClick();
    }
}
public class CameraMovement : MonoBehaviour
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

    CameraWheel cameraWheel; // 각각 클릭 실행함수를 제어하기 위해 분리
    CameraLeftClick cameraLeftClick;
    CameraRightClick cameraRightClick;

    private void Awake()
    {
        gameManager = GameManager.instance;
        if(gameManager.camearaMove == null)
        {
            gameManager.camearaMove = this;
        }
        cameraWheel = new CameraWheel(this);
        cameraLeftClick = new CameraLeftClick(this);
        cameraRightClick = new CameraRightClick(this);
    }
    void Start()
    {
        myTr = this.transform;
        ChangeAllMouseInput();
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
        lookAngle -= Input.GetAxis("Mouse Y") * rotationSpeed;
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
        
    }

    public void OnRightClick()
    {
        if (Input.GetMouseButton(1))
        {
            rotateAngle += Input.GetAxis("Mouse X") * rotationSpeed;
            //LookDistanceChange();
            VertRotate();
            HoriRotate();
        }
        else
        {
            LookDistanceChange();
        }
    }

    public void OnMouseWheel()
    {
        LookDistanceChange();
        if (distance!=Vector3.Distance(followTr.position,transform.position))
        {
            HoriRotate();
        }
    }

    public void ChangeAllMouseInput()
    {
        AddLeftClick();
        AddRightClick();
        AddWheel();
    }
    public void AddLeftClick()
    {
        if(gameManager == null)
        {
            Debug.Log("gm null");
            return;
        }
        gameManager.mouseManager.leftClick.Push(cameraLeftClick);
    }
    public void AddRightClick()
    {
        if (gameManager == null)
        {
            Debug.Log("gm null");
            return;
        }
        gameManager.mouseManager.rightClick.Push(cameraRightClick);
    }
    public void AddWheel()
    {
        if (gameManager == null)
        {
            Debug.Log("gm null");
            return;
        }
        gameManager.mouseManager.wheelAction.Push(cameraWheel);
    }
}
