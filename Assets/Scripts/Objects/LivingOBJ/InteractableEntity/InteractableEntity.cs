using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableEntity : LivingEntity,IInteractable
{
    [SerializeField]protected string dialogue;
    protected UIManager uiManager;
    private void Awake()
    {
        OnAwake();
    }
    protected virtual void OnAwake()
    {
        uiManager = GameManager.instance.UIManager;
    }
    public void Interact()
    {
        NPCInteract();
    }
    protected virtual void NPCInteract()
    {

    }

    protected virtual void TriggerOut(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerEntity playerEntity = other.transform.GetComponent<PlayerEntity>();
            playerEntity.interactOBJ.Remove(this.gameObject);
        }
    }

    
    protected void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Object In to {this.gameObject.name}! Name : {other.gameObject.name} tag : {other.tag}");
        
        if (other.tag == "Player")
        {
            PlayerEntity playerEntity = other.transform.GetComponent<PlayerEntity>();
            playerEntity.interactOBJ.Add(this.gameObject);
            Debug.Log($"{this.gameObject.name} in");
        }
    }
    protected void OnTriggerExit(Collider other)
    {
        TriggerOut(other);
    }

}
