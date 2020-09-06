using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;

    bool isGrounded;

    [SerializeField]
    Transform groundCheck;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if(Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            isGrounded = true;
        } else
        {
            isGrounded = true;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rb2d.velocity = new Vector2(2, rb2d.velocity.y);
            animator.Play("Player_run");
            spriteRenderer.flipX = false;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rb2d.velocity = new Vector2(-2, rb2d.velocity.y);
            animator.Play("Player_run");
            spriteRenderer.flipX = true;
        }
        else
        {
            animator.Play("Player_idle");
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 3);
            animator.Play("Player_jump");
        }
    }
}
