using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionLogic : MonoBehaviour
{
    public bool isInRange;
    public UnityEvent interactAction;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")){
            Debug.Log("Interactable in range");
            isInRange = true;
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null) {
                playerController.SetCurrentInteractable(this);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            isInRange = false;
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null) {
            playerController.SetCurrentInteractable(null);
            }
        }
    }
    
    public void InteractTriggered(){
        Debug.Log("InteractTriggered called on: " + gameObject.name);
        interactAction.Invoke();
    }
}
