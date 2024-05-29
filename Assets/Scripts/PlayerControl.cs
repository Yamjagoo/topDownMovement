using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float movSpeed;
    float speedX, speedY;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;

    private RandomEncounter randomEncounter;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        randomEncounter = GetComponent<RandomEncounter>();
    }

    void Update()
    {
        speedX = Input.GetAxisRaw("Horizontal") * movSpeed;
        speedY = Input.GetAxisRaw("Vertical") * movSpeed;
        rb.velocity = new Vector2(speedX, speedY);

        animator.SetFloat("xVelocity", speedX);
        animator.SetFloat("yVelocity", speedY);

        UpdateCharacterDirection();

        if (speedX != 0 || speedY != 0)
        {
            randomEncounter.TryEncounter();
        }
    }

    void UpdateCharacterDirection()
    {
        // Flip sprite based on horizontal movement
        if (speedX < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (speedX > 0)
        {
            spriteRenderer.flipX = false;
        }
    }
}
