using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{

    private Animator animator;

    public bool hasWand = true;



    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("HasWand", hasWand);
    }

}
