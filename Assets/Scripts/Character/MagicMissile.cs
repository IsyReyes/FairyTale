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
        // Add collision logic here (e.g., damage to enemies)
        Debug.Log("MagicMissile collided with: " + collision.gameObject.name);
        Destroy(gameObject);
    }
}
