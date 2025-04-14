using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Races { }
public enum MoveState
{
    walk,
    run
}

public class LivingEntity : MonoBehaviour
{
    public string entityName { get; set; }
    public string gender { get; set; }
    public int age { get; set; }
    public Races race { get; set; }
    public string prefab { get; set; }//외형 관련 정보
    public string faceSprite { get; set; }//얼굴 이미지

    public int level { get; set; }
    public int attack { get; set; }
    public int deffense { get; set; }
    public int healthPoint { get; set; }
    public int maxHealthPoint { get; set; }
    public int steminaPoint { get; set; }
    [SerializeField]protected float moveSpeed { get; set; }
    protected Vector3 front;
    
    protected Rigidbody myRigid;
    [SerializeField]public MoveState moveState;

    protected AnimControll animControll;

    protected virtual void Start()
    {
        SetInValues();
        ResisterEvents();
    }
    protected virtual void ResisterEvents()
    {
        EventManager.instance.MoveOnUpDate.AddListener(MoveTo);
    }
    protected virtual void RemoveEvents()
    {
        EventManager.instance.MoveOnUpDate.RemoveListener(MoveTo);
    }


    public virtual void SetMoveState(MoveState inputState)
    {
        moveState = inputState;
        SetMoveSpeed();
    }
    public virtual void SetMoveSpeed()
    {

    }

    protected virtual void SetInValues()
    {
        myRigid = GetComponent<Rigidbody>();
        animControll = GetComponent<AnimControll>();

    }
    protected virtual void MoveTo()
    {
        float finalSpeed = Vector3.Magnitude(front * moveSpeed * Time.deltaTime);
        myRigid.MovePosition(myRigid.position + front * moveSpeed * Time.deltaTime);
    }

    protected virtual void MoveAnimChange()
    {

    }

    public void AddHealthPoint(int point)
    {
        if (point + healthPoint <= maxHealthPoint)
        {
            healthPoint = healthPoint + point;
        }
        else if (point + healthPoint > maxHealthPoint)
        {
            healthPoint = maxHealthPoint;
        }
    }
}
