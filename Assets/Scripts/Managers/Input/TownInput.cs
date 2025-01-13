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
    public QuickSlotPanel quickSlot;
    
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
        OnInteractKey();

        OnInventoryKey();
        OnItemTest();
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
        if(quickSlot == null)
        {
            GameObject.Find("QuickSlotPanel").transform.GetComponent<QuickSlotPanel>();
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
    #region InteractKey
    void OnInteractKey()
    {
        if(Input.GetKeyDown(gameManager.keySettings.keyName["interact"]) && playerEntity.interactOBJ.Count>=1)
        {
            GameObject interactObj = playerEntity.interactOBJ[0];
            GameObject playerOBJ = playerEntity.gameObject;
            float distance = Vector3.Distance(playerOBJ.transform.position, interactObj.transform.position);
            for(int i = 0; i < playerEntity.interactOBJ.Count; i++)
            {
                GameObject tempOBJ = playerEntity.interactOBJ[i];
                float tempDistance = Vector3.Distance(playerOBJ.transform.position,tempOBJ.transform.position);
                if (tempDistance < distance)
                {
                    interactObj = tempOBJ;
                }
            }

            
            IInteractable interact = interactObj.transform.GetComponent<IInteractable>();

            playerEntity.nowInteract = interactObj;
            interact.Interact();


        }

        else if (playerEntity.interactOBJ.Count==0)
        {
            Debug.Log("InteractOBJ NULL");
        }
    }
    void InteractDistCheck()
    {
        //원형 레이캐스트로 해당 오브젝트 있는지 판단해야함
    }
    #endregion

    void OnInventoryKey()
    {
        if (Input.GetKeyDown(gameManager.keySettings.keyName["inventory"]))
        {
            if (GameManager.instance.UIManager.inventoryPanel.activeSelf)
            {
                GameManager.instance.UIManager.inventoryPanel.SetActive(false);
            }
            else
            {
                GameManager.instance.UIManager.inventoryPanel.SetActive(true);
            }
        }
    }
    void OnItemTest()
    {
        if (Input.GetKeyDown(gameManager.keySettings.keyName["itemTest"]))
        {
            GameObject go = GameObject.Instantiate(GameManager.instance.testItemObj);
            go.transform.position = player.transform.position + new Vector3(2, 2, 2);
        }
    }

    void OnQuickSlotKey()
    {
        foreach(var key in gameManager.keySettings.quickSlotKeys.Keys)
        {
            int slotNumber = gameManager.keySettings.quickSlotKeys[key];
            if (Input.GetKeyDown(key))
            {
                quickSlot.quickUse.SetItem(gameManager.keySettings.quickSlots[slotNumber].item);
                
            }
        }
    }
}
