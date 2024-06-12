using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 lastMoveDirection;
    private bool isAttacking = false;

    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;

    private InputInterface moveCommand;
    private InputInterface attackCommand;
    private InputInterface interactCommand;

    private InteractionLogic currentInteractable;

    private void Start()
    {
        Debug.Log("Initiating character controllers");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GetInputs();
        Animate();
    }

    void Animate()
    {
        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);
        animator.SetFloat("MovementMagnitude", movement.magnitude);
        animator.SetFloat("LastMoveX", lastMoveDirection.x);
        animator.SetFloat("LastMoveY", lastMoveDirection.y);
        animator.SetBool("IsAttacking", isAttacking);
    }

    void GetInputs()
    {
        if (!isAttacking)
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            if (moveX != 0 || moveY != 0)
            {
                movement = new Vector2(moveX, moveY).normalized;
                lastMoveDirection = movement;
                moveCommand = new MoveCommand(this, movement);
                moveCommand.ExecuteInputRequest();
            }
            else
            {
                movement = Vector2.zero;
            }

            if (Input.GetButtonDown("Fire1"))
            {
                attackCommand = new AttackCommand(this);
                attackCommand.ExecuteInputRequest();
            }

            if (Input.GetButtonDown("Interact")){
                interactCommand = new InteractCommand(this);
                interactCommand.ExecuteInputRequest();
            }
        }
    }


    private void FixedUpdate()
    {
        if (!isAttacking)
        {
            Move(movement);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void Move( Vector2 direction)
    {
        rb.velocity = direction * movementSpeed;
    }

    public void Interact(){

        Debug.Log("Attempting to interact");
        if (currentInteractable != null && currentInteractable.isInRange){
            Debug.Log("Interacting with: " + currentInteractable.gameObject.name);
            currentInteractable.InteractTriggered();
        }else{
            Debug.Log("Unable to interact. currentInteractable is " + (currentInteractable == null ? "null" : currentInteractable.gameObject.name) + ", isInRange: " + (currentInteractable != null && currentInteractable.isInRange));
        }
    }

    public void SetCurrentInteractable(InteractionLogic interactable)
    {
        Debug.Log("SetCurrentInteractable called with: " + (interactable != null ? interactable.gameObject.name : "null"));
        currentInteractable = interactable;
    }

    public void Attack(){
        isAttacking = true;
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        Debug.Log("Spawning Projectile");
        MagicMissile.SpawnProjectile(projectilePrefab, projectileSpawnPoint, lastMoveDirection);
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
        animator.SetBool("IsAttacking", false);
    }



}
