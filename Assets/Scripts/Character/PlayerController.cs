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

    InteractionLogic interactionLogic;

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

    public void Attack(){
        isAttacking = true;
        StartCoroutine(AttackRoutine());
    }

    public void Interact(){
        interactionLogic = new InteractionLogic();
        interactionLogic.Interact();
    }

    IEnumerator AttackRoutine()
    {
        Debug.Log("Spawning Projectile");
        SpawnProjectile();
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
        animator.SetBool("IsAttacking", false);
    }

    void SpawnProjectile()
    {
        Debug.Log("Spawning projectile");

        if (projectilePrefab == null || projectileSpawnPoint == null)
        {
            Debug.LogError("Projectile Prefab or Spawn Point is not assigned!");
            return;
        }

        Vector2 spawnPosition = (Vector2)projectileSpawnPoint.position + lastMoveDirection * 0.5f; // Adjust the offset as needed
        GameObject magicMissile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
        MagicMissile magicMissileScript = magicMissile.GetComponent<MagicMissile>();

        if (magicMissileScript == null)
        {
            Debug.LogError("MagicMissile script not found on the instantiated projectile!");
            return;
        }

        magicMissileScript.direction = lastMoveDirection; // Pass direction to missile
        Debug.Log("Setting direction: " + lastMoveDirection);
        magicMissile.transform.parent = null; // Detach from the player
    }
}
