using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;

    [SerializeField] private float jumpLength = 10f;
    [SerializeField] private float jumpHeight = 15f;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Collider2D coll;
    private bool facingLeft = true;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(transform.position.x < leftCap)
        {
            facingLeft = false;
            spriteRenderer.flipX = true;
        } else if(transform.position.x > rightCap)
        {
            facingLeft = true;
            spriteRenderer.flipX = false;
        }

        if(coll.IsTouchingLayers(ground))
        {
            if(facingLeft)
            {
                rb2d.velocity = new Vector2(-jumpLength, jumpHeight);
            }
            else
            {
                rb2d.velocity = new Vector2(jumpLength, jumpHeight);
            }
        }
    }
}
