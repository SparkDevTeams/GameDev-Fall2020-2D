﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveAttack : Attack, IHitboxResponder
{
    [SerializeField] Hitbox diveHitbox;
    [SerializeField] Hitbox landingHitbox;
    [SerializeField] private int damage = 8;
    [SerializeField] private int chargedDamage = 8;
    [SerializeField] private float diveTopSpeed = 25.0f;
    [SerializeField] private float chargedTopSpeed = 35.0f;
    [SerializeField] private float diveAcceleration = 500.0f;
    [SerializeField] private float chargedAcceleration = 600.0f;
    [SerializeField] private float chargeTime = 1.2f;
    private float diveSpeed = 0.0f;
    private float chargeTimer = 0.0f;
    private PlayerState playerState;
    private PlayerMovement movement;
    private Rigidbody2D rb;
    private bool isDiving = false;
    private bool hasLanded = false;
    private bool landing = false;
    private bool attackButtonPressed = false;
    private bool attackButtonUp = false;
    private bool chargedDive = false;

    void Start()
    {
        playerState = GetComponent<PlayerState>();
        movement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        attackButtonPressed = Input.GetButton("Attack");
        attackButtonUp = Input.GetButtonUp("Attack");

        if (isDiving)
        {
            diveHitbox.HitboxUpdate();
        }
        else if (landing) 
        {
            landingHitbox.HitboxUpdate();
            landing = false;
            hasLanded = true;
        }
    }

    private void FixedUpdate()
    {
        if (attackInit) 
        {
            if(!isDiving && chargeTimer > 0.0f) 
            {
                rb.velocity = new Vector2(0.0f, 0.0f);

                if (attackButtonPressed)
                {
                    chargeTimer -= Time.deltaTime;

                    if (chargeTimer <= 0.0f)
                    {
                        chargeTimer = 0.0f;
                        isDiving = true;
                        chargedDive = true;
                        diveHitbox.SetResponder(this);
                        diveHitbox.StartCheckingCollisions();
                    }
                }

                if (attackButtonUp && !isDiving) 
                {
                    chargeTimer = 0.0f;
                    isDiving = true;
                    chargedDive = false;
                    diveHitbox.SetResponder(this);
                    diveHitbox.StartCheckingCollisions();
                }
            }
            
            if(isDiving && playerState.IsGrounded())
            {
                attackInit = false;
                isDiving = false;
                diveHitbox.StopCheckingCollisions();
                landing = true;
                landingHitbox.SetResponder(this);
                landingHitbox.StartCheckingCollisions();
            }
            else if(isDiving)
            {
                Dive();
            }
        }
        else if(hasLanded)
        {
            landingHitbox.StopCheckingCollisions();
            movement.ResetGravityDefault();
            movement.EnableMovement();
            playerState.SetAttacking(false);
            hasLanded = false;
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
        landing = false;
        chargedDive = false;
        playerState.SetAttacking(true);
        movement.DisableMovement();
        rb.gravityScale = 0.0f;
        diveSpeed = 0.0f;
        chargeTimer = chargeTime;

        if(chargeTime <= 0.0f) 
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
        landing = false;
        chargedDive = false;
        movement.ResetGravityDefault();
        movement.EnableMovement();
        playerState.SetAttacking(false);
        chargeTimer = 0.0f;
        diveSpeed = 0.0f;
    }

    private void Dive() 
    {
        float topSpeed = 0.0f;
        float acceleration = 0.0f;

        if (chargedDive)
        {
            topSpeed = chargedTopSpeed;
            acceleration = chargedAcceleration;
        }
        else 
        {
            topSpeed = diveTopSpeed;
            acceleration = diveAcceleration;
        }

        diveSpeed -= acceleration * Time.deltaTime;

        if(diveSpeed < -topSpeed) 
        {
            diveSpeed = -topSpeed;
        }

        Debug.Log("Dive Speed: " + diveSpeed);

        rb.velocity = new Vector2(0.0f, diveSpeed);
    }

    public void CollideWith(Collider2D collision) 
    {
        IHitable hitOptions = collision.GetComponentInParent<IHitable>();

        if (hitOptions != null)
        {
            if (chargedDive)
            {
                hitOptions.Hit(chargedDamage);
            }
            else
            {
                hitOptions.Hit(damage);
            }
        }
    }
}
