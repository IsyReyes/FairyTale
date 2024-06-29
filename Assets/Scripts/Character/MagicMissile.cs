using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMissile : MonoBehaviour
{
    public float speed = 10f;
    public Vector2 direction;
    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        Debug.Log("MagicMissile Start method called");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found!");
            return;
        }

        if (animator == null)
        {
            Debug.LogError("Animator component not found!");
            return;
        }

        rb.velocity = direction * speed;
        Debug.Log("Setting velocity: " + rb.velocity);
        SetAnimationDirection();
    }

    void SetAnimationDirection()
    {
        Debug.Log("SetAnimationDirection method called");
        if (animator == null)
        {
            Debug.LogError("Animator component not found!");
            return;
        }

        Debug.Log($"Setting Animation Direction: DirectionX={direction.x}, DirectionY={direction.y}");
        animator.SetFloat("DirectionX", direction.x);
        animator.SetFloat("DirectionY", direction.y);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("MagicMissile collided with: " + collision.gameObject.name);
        Destroy(gameObject);
    }

    public static  void SpawnProjectile(GameObject projectilePrefab, Transform projectileSpawnPoint, Vector2 lastMoveDirection)
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
