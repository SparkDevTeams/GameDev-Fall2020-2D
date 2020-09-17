using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveAttack : Attack
{
    [SerializeField] private int damage = 8;
    [SerializeField] private float diveTopSpeed = 25.0f;
    [SerializeField] private float diveAcceleration = 500.0f;
    [SerializeField] private float startPauseTime = 0.0f;
    private float diveSpeed = 0.0f;
    private float startPauseTimer = 0.0f;
    private PlayerState playerState;
    private PlayerMovement movement;
    private Rigidbody2D rb;
    private bool isDiving = false;
    private bool hasLanded = false;

    void Start()
    {
        playerState = GetComponent<PlayerState>();
        movement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDiving)
        {

        }
        else if (hasLanded) 
        {
            hasLanded = false;
        }
    }

    private void FixedUpdate()
    {
        if (attackInit) 
        {
            if(startPauseTimer > 0.0f) 
            {
                rb.velocity = new Vector2(0.0f, 0.0f);
                startPauseTimer -= Time.deltaTime;

                if(startPauseTimer <= 0.0f) 
                {
                    startPauseTimer = 0.0f;
                    isDiving = true;
                }
            }
            else if(isDiving && playerState.IsGrounded())
            {
                attackInit = false;
                isDiving = false;
                hasLanded = true;
            }
            else if(isDiving)
            {
                Dive();
            }
        }
        else 
        {
            movement.ResetGravityDefault();
            movement.EnableMovement();
            playerState.SetAttacking(false);
        }
    }

    public override bool CanAttack()
    {
        return !playerState.IsDashing() && !playerState.IsGrounded();
    }

    public override void StartAttack() 
    {
        attackInit = true;
        hasLanded = false;
        playerState.SetAttacking(true);
        movement.DisableMovement();
        rb.gravityScale = 0.0f;
        diveSpeed = 0.0f;
        startPauseTimer = startPauseTime;

        if(startPauseTime <= 0.0f) 
        {
            isDiving = true;
        }
        else 
        {
            isDiving = false;
        }
    }

    public override void Break()
    {
        attackInit = false;
        isDiving = false;
        hasLanded = false;
        movement.ResetGravityDefault();
        movement.EnableMovement();
        playerState.SetAttacking(false);
        startPauseTimer = 0.0f;
        diveSpeed = 0.0f;
    }

    private void Dive() 
    {
        diveSpeed -= diveAcceleration * Time.deltaTime;

        if(diveSpeed < -diveTopSpeed) 
        {
            diveSpeed = -diveTopSpeed;
        }

        Debug.Log("Dive Speed: " + diveSpeed);

        rb.velocity = new Vector2(0.0f, diveSpeed);
    }
}
