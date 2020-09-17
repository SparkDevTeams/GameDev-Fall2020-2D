using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour
{
    private float fallMultiplier = 5f;
    private float lowJumpMultiplier = 12f;
    private float jumpHeight = 19f;

    private float doubleJumpHeight = 16f;

    private bool jumped;
    private bool doubleJumped;
    private int numberOfJumps = 0;

    private PlayerStatuses status;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        status = FindObjectOfType<PlayerStatuses>();

    }

    // Update is called once per frame
    void Update()
    {
        //Checks for the jump button press
        if (Input.GetButtonDown("Jump") && (status.getGrounded() || status.getHanging()))
        {
            jumped = true;
        }

        if (Input.GetButtonDown("Jump") && !(status.getGrounded() || status.getHanging()) && numberOfJumps > 0)
        {
            doubleJumped = true;
        }

        if (status.getGrounded() || status.getHanging())
        {
            numberOfJumps = 1;
            doubleJumped = false;
        }


    }

    private void FixedUpdate()
    {

        //Checks if the player pressed the jump button before applying the jump
        if (jumped || doubleJumped)
        {
            jump();
            numberOfJumps--;
            jumped = false;

            if (doubleJumped)
            {
                doubleJumped = false;
            }
        }

        if (status.getTechyBounce())
        {
            techyBounce();
            status.setTechyBounce(false);
        }

        //Increases gravity when player is falling. Also accounts for when the player only holds jump for a short time
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void jump()
    {
        if (jumped)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
        else if (doubleJumped)
        {
            rb.velocity = new Vector2(rb.velocity.x, doubleJumpHeight);
        }

    }

    void techyBounce()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight * 3);
        //status.setPounding(false);
    }


}
