using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController2D : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    private Collider2D collider;

    private bool isGrounded;
    private bool jump;
    private enum State { idle, running, jumping, falling, hurt }
    private State state = State.idle;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Text cherryText;
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private int cherries = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (collider.IsTouchingLayers(ground))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jump = true;
        }

        AnimationState();
        animator.SetInteger("state", (int) state);
    }

    private void FixedUpdate()
    {
        float hDirection = Input.GetAxis("Horizontal");

        if (hDirection < 0)
        {
            rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
            spriteRenderer.flipX = true;
        }
        else if (hDirection > 0)
        {
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
            spriteRenderer.flipX = false;
        }
        else
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }

        if (jump)
        {
            jump = false;
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            state = State.jumping;
        }
    }

    private void AnimationState()
    {

        if (state == State.jumping)
        {
            if(rb2d.velocity.y < 0.1f)
            {
                state = State.falling;
            }
        }
        else if(state == State.falling)
        {
            if(isGrounded)
            {
                state = State.idle;
            }
        }
        else if(Mathf.Abs(rb2d.velocity.x) > Mathf.Epsilon)
        {
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Collectible")
        {
            Destroy(collision.gameObject);
            cherries++;
            cherryText.text = cherries.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.otherCollider.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }
}
