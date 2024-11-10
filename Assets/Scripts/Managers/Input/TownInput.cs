using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownInput : InputBase
{
    GameObject player;
    Rigidbody playerRigid;
    Transform cameraTr;
    float horiInput;
    float vertInput;
    Vector3 front;
    Vector3 cameraForward;
    Vector3 cameraRight;
    public float moveSpeed=3;
    public override void OnPlayerInput()
    {
        if(cameraTr == null)
        {
            cameraTr = GameObject.Find("Main Camera").transform;
        }
        OnAxisInput();

    }

    public void PlayerInfoIn()
    {
        if(player == null)
        {
            player = GameObject.Find("Player");
            
        }
        if(playerRigid == null)
        {
            playerRigid = player.transform.GetComponent<Rigidbody>();
        }
    }

    public void OnAxisInput()
    {
        if (player == null)
        {
            PlayerInfoIn();
            return;
        }
        cameraForward = cameraTr.forward;
        cameraForward.y = 0;
        cameraForward = cameraForward.normalized;
        cameraRight = cameraTr.right;
        cameraRight.y = 0;
        cameraRight = cameraRight.normalized;

        horiInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");

        front = cameraForward*vertInput+cameraRight*horiInput;
        front = front.normalized;
        if (front != Vector3.zero) // 앞 방향이 0이 아닐 때만 회전
        {
            player.transform.rotation = Quaternion.LookRotation(front);
        }

        // 이동
        playerRigid.MovePosition(playerRigid.position + front * moveSpeed * Time.deltaTime);
        //player.transform.Translate(front * moveSpeed * Time.deltaTime);
    }

    void OnESCKey()
    {
        UIManager uiManager = GameManager.instance.UIManager;
        if (uiManager.openUIObj.Count > 1)
        {
            uiManager.openUIObj.RemoveAt(uiManager.openUIObj.Count - 1);
        }
    }
}
