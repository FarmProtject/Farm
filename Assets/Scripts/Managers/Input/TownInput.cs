using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownInput : InputBase
{
    GameObject player;
    float horiInput;
    float vertInput;
    Vector3 front;
    float moveSpeed;
    public override void OnPlayerInput()
    {
        throw new System.NotImplementedException();
    }


    void OnAxisInput()
    {
        if (player == null)
        {
            return;
        }
        horiInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");
        front = new Vector3(horiInput, vertInput).normalized;

        player.transform.Translate(front * moveSpeed * Time.deltaTime) ;

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
