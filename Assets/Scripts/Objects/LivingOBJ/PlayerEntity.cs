using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : LivingEntity
{
    Transform cameraTr;

    public float runSpeed=4f;
    public float walkSpeed=2f;

    [SerializeField]public List<GameObject> interactOBJ = new List<GameObject>();
    public GameObject nowInteract;


    private void Awake()
    {
        
    }
    protected override void Start()
    {
        base.Start();
        SetMoveState(MoveState.walk);
        if (GameManager.instance.playerEntity == null)
        {
            GameManager.instance.playerEntity = this;
        }
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
        SetMoveSpeed();
        MoveAnimCahnge();
    }
    public override void SetMoveSpeed()
    {
        switch (moveState)
        {
            case MoveState.walk:
                moveSpeed = walkSpeed;
                break;
            case MoveState.run:
                moveSpeed = runSpeed;
                break;
            default:
                break;
        }
    }
    #endregion
    #region �÷��̾��� �̵��� �̵� �ִϸ��̼� ����
    protected override void MoveTo()
    {
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
