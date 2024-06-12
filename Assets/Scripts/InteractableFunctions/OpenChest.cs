using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{

    void Start(){
        Debug.Log(gameObject.activeInHierarchy);
    }

    public void InteractChest(){
        Debug.Log($"{gameObject.name} interaction initiated");

        if (gameObject.activeInHierarchy){
            Debug.Log($"{gameObject.name} is active!");
        }else{
            Debug.Log($"{gameObject.name} is inactive");
        }

        StartCoroutine(OpenChestCoroutine());

    }

    private IEnumerator OpenChestCoroutine(){

        Debug.Log("Copening chest!");

        yield return new WaitForSeconds(0.5f);

        Debug.Log("Destroying chest.");
        Destroy(transform.parent.gameObject);
    }

}
