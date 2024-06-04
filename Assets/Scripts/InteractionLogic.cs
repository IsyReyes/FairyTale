using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionLogic : MonoBehaviour
{
    InteractableList interactableList;

    void Start(){
        interactableList = new InteractableList();
    }


    public void Interact()
    {
        float interactionDistance = 2f; // Adjust as needed
        Collider2D[] nearbyColliders = Physics2D.OverlapCircleAll(transform.position, interactionDistance);

        foreach (Collider2D collider in nearbyColliders){
            if (collider.CompareTag("Interactable")){
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < interactionDistance){
                    interactableList.ExecuteInteraction(collider.gameObject);
                }
            }
        }
    }

}
