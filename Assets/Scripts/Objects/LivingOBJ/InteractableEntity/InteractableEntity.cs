using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableEntity : LivingEntity,IInteractable
{
    [SerializeField]protected GameObject dialoguePanel;
    private void Awake()
    {
        dialoguePanel = GameObject.Find("DialoguePanel");
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
        if (other.tag == "Player")
        {
            PlayerEntity playerEntity = other.transform.GetComponent<PlayerEntity>();
            playerEntity.interactOBJ.Add(this.gameObject);
        }
    }
    protected void OnTriggerExit(Collider other)
    {
        TriggerOut(other);
    }

}
