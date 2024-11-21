using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TownInput : InputBase
{

    GameObject player;
    Rigidbody playerRigid;
    Transform cameraTr;
    Animator playerAnim;

    float horiInput;
    float vertInput;

    Vector3 front;
    Vector3 cameraForward;
    Vector3 cameraRight;


    public PlayerEntity playerEntity;
    public override void OnPlayerInput()
    {
        if(cameraTr == null)
        {
            cameraTr = GameObject.Find("Main Camera").transform;
        }
        OnAxisInput();
        OnRunKey();
        if(front.magnitude == 0)
        {
            Debug.Log("MAGNITYDE = 0");
        }

    }

    public void PlayerInfoIn()
    {
        if(player == null)
        {
            player = GameObject.Find("Player");
        }
        if(playerEntity == null)
        {
            playerEntity = player.GetComponent<PlayerEntity>();
        }
        if(playerRigid == null)
        {
            playerRigid = player.transform.GetComponent<Rigidbody>();
        }
        if(playerAnim == null)
        {
            playerAnim = player.GetComponentInChildren<Animator>();
        }
    }
#region BasicMove

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

        playerEntity.OnAxisInput(front);
    }
    #endregion
    #region MoveStateChange

    void OnRunKey()
    {
        if (Input.GetKeyUp(gameManager.keySettings.keyName["run"]) && playerEntity.moveState == MoveState.walk)
        {
            playerEntity.SetMoveState(MoveState.run);
            return;
        }
        else if(Input.GetKeyUp(gameManager.keySettings.keyName["run"]) && playerEntity.moveState == MoveState.run)
        {
            playerEntity.SetMoveState(MoveState.walk);
            return;
        }
    }
    #endregion
    void OnESCKey()
    {
        UIManager uiManager = GameManager.instance.UIManager;
        if (uiManager.openUIObj.Count > 1)
        {
            uiManager.openUIObj.RemoveAt(uiManager.openUIObj.Count - 1);
        }
    }

    #region Animation
    void PlayerAnimChange()
    {

    }

    #endregion
}
