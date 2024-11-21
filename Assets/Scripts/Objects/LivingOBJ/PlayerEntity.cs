using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : LivingEntity
{
    Transform cameraTr;

    public float runSpeed=4f;
    public float walkSpeed=2f;

    protected override void Start()
    {
        base.Start();
        SetMoveState(MoveState.walk);

    }

    #region �÷��̾��� ���� �ʱ�ȭ
    protected override void SetInValues()
    {
        base.SetInValues();

        if (cameraTr == null)
        {
            cameraTr = GameObject.Find("Main Camera").transform;
        }
        if(myRigid == null)
        {
            myRigid = GetComponent<Rigidbody>();
        }
        runSpeed = 4f;
        walkSpeed = 2f;
    }
    #endregion
    #region MoveStateChange �̵����¿� �ִϸ��̼��� �����Ѵ�
    public override void SetMoveState(MoveState inputState)
    {
        moveState = inputState;
        Debug.Log(moveState);
        SetMoveSpeed();
        MoveAnimCahnge();
    }
    public override void SetMoveSpeed()
    {
        switch (moveState)
        {
            case MoveState.walk:
                moveSpeed = walkSpeed;
                Debug.Log("walkSpeed");
                break;
            case MoveState.run:
                moveSpeed = runSpeed;
                Debug.Log("runSpeed");
                break;
            default:
                break;
        }
    }
    #endregion
    #region �÷��̾��� �̵��� �̵� �ִϸ��̼� ����
    protected override void MoveTo()
    {
        Debug.Log("MoveTo!");
        if(front.magnitude != 0)
        {
            myRigid.MovePosition(myRigid.position + front * moveSpeed * Time.deltaTime);
        }
        animControll.ObjMove(front.magnitude);
    }

    public void OnAxisInput(Vector3 frontVector)
    {
        if (front != Vector3.zero) // �� ������ 0�� �ƴ� ���� ȸ��
        {
            transform.rotation = Quaternion.LookRotation(front);
        }
        front = frontVector;
    }

    public void MoveAnimCahnge()
    {
        animControll.MoveStateChange(moveState);
    }
    #endregion
    



}
