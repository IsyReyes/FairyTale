using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableList : MonoBehaviour
{

    public void ExecuteInteraction(GameObject obj)
    {
        switch (obj.tag)
        {
            
            case "NPCFairy":
                Debug.Log("Interacting with NPC Fairy");
                break;
            case "Chest":
                Debug.Log("Interacting with Chest");
                break;
            case "Door":
                Debug.Log("Interacting with Door");
                break;
            default:
            Debug.Log("Interacting with " + obj.name);
            break;
        }
    }

}
