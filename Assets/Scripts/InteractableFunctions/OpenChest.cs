using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{

    private PlayerController playerController;


    void Start(){
        Debug.Log(gameObject.activeInHierarchy);
        playerController = FindObjectOfType<PlayerController>(); // Find the PlayerController in the scene
    }

    public void InteractChest(){
        Debug.Log($"{gameObject.name} interaction initiated");

        if (gameObject.activeInHierarchy){
            Debug.Log($"{gameObject.name} is active!");
            playerController.LockMovement();
            StartCoroutine(OpenChestCoroutine());
        }else{
            Debug.Log($"{gameObject.name} is inactive");
        }
    }

    private IEnumerator OpenChestCoroutine(){

        Debug.Log("Opening chest!");

        yield return new WaitForSeconds(0.5f);

        Debug.Log("Destroying chest.");
        Destroy(transform.parent.gameObject);

        playerController.UnlockMovement();
    }

}
