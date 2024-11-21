using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimControll : MonoBehaviour
{
    public Animator objAnim;
    private void Awake()
    {
        objAnim = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void ObjMove(float moveSpeed)
    {
        objAnim.SetFloat("moveSpeed",moveSpeed);
    }
    public void MoveStateChange(MoveState moveState)
    {
        switch (moveState)
        {
            case MoveState.walk:
                objAnim.SetInteger("moveState", (int)moveState);
                break;
            case MoveState.run:
                objAnim.SetInteger("moveState", (int)moveState);
                break;
            default:
                break;
        }
    }
}
