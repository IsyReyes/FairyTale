using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteraction : MonoBehaviour
{
    private PlayerController playerController;
    public Dialogue dialogue;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    public void InteractDialogue()
    {
        playerController.LockMovement();
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
