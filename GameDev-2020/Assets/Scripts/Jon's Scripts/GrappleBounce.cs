using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleBounce : MonoBehaviour
{
    [SerializeField] private float wallBounceSpeed = 10.0f;
    [SerializeField] private float ceilingBounceSpeed = 12.5f;
    [SerializeField] private float floorBounceSpeed = 10.0f;
    [SerializeField] private float wallBounceAngle = 110.0f;
    [SerializeField] private float ceilingBounceAngle = 315.0f;
    [SerializeField] private float floorBounceAngle = 40.0f;
    [SerializeField] private float wallBounceTime = 0.2f;
    [SerializeField] private float floorBounceTime = 0.2f;
    private bool floorBouncing = false;
    private bool ceilingBouncing = false;
    private bool wallBouncing = false;
    private float bounceTimer = 0.0f;
    private PlayerState playerState;
    private PlayerMovement movement;
    private Rigidbody2D rb;

    void Start()
    {
        playerState = GetComponent<PlayerState>();
        movement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (playerState.IsGrappleBouncing()) 
        {
            if (wallBouncing)
            {
                if (bounceTimer > 0.0f)
                {
                    if (!IsFacingLeft())
                    {
                        Bounce(wallBounceAngle, wallBounceSpeed);
                    }
                    else 
                    {
                        Bounce(HorizontalFlipAngle(wallBounceAngle), wallBounceSpeed);
                    }

                    bounceTimer -= Time.deltaTime;

                    if (bounceTimer <= 0.0f) 
                    {
                        bounceTimer = 0.0f;
                        wallBouncing = false;
                        playerState.SetGrappleBouncing(false);
                        movement.ResetGravityDefault();
                        movement.EnableMovement();
                    }
                }
            }
            else if (ceilingBouncing)
            {
                if (!playerState.IsGrounded())
                {
                    if (!IsFacingLeft())
                    {
                        Bounce(ceilingBounceAngle, ceilingBounceSpeed);
                    }
                    else
                    {
                        Bounce(HorizontalFlipAngle(ceilingBounceAngle), ceilingBounceSpeed);
                    }
                }
                else 
                {
                    ceilingBouncing = false;
                    playerState.SetGrappleBouncing(false);
                    movement.ResetGravityDefault();
                    movement.EnableMovement();
                }
            }
            else if (floorBouncing) 
            {
                if (bounceTimer > 0.0f)
                {
                    if (!IsFacingLeft())
                    {
                        Bounce(floorBounceAngle, floorBounceSpeed);
                    }
                    else
                    {
                        Bounce(HorizontalFlipAngle(floorBounceAngle), floorBounceSpeed);
                    }

                    bounceTimer -= Time.deltaTime;

                    if (bounceTimer <= 0.0f)
                    {
                        bounceTimer = 0.0f;
                        floorBouncing = false;
                        playerState.SetGrappleBouncing(false);
                        movement.ResetGravityDefault();
                        movement.EnableMovement();
                    }
                }
            }
        }
    }

    public void StopBouncing() 
    {
        playerState.SetGrappleBouncing(false);
        movement.EnableMovement();
        movement.ResetGravityDefault();
        wallBouncing = false;
        ceilingBouncing = false;
        floorBouncing = false;
        bounceTimer = 0.0f;
    }

    public void StartWallBounce() 
    {
        movement.DisableMovement();
        rb.gravityScale = 0.0f;
        playerState.SetGrappleBouncing(true);
        wallBouncing = true;
        ceilingBouncing = false;
        floorBouncing = false;
        bounceTimer = wallBounceTime;
    }

    public void StartCeilingBounce() 
    {
        movement.DisableMovement();
        rb.gravityScale = 0.0f;
        playerState.SetGrappleBouncing(true);
        wallBouncing = false;
        ceilingBouncing = true;
        floorBouncing = false;
    }

    public void StartFloorBounce() 
    {
        movement.DisableMovement();
        rb.gravityScale = 0.0f;
        playerState.SetGrappleBouncing(true);
        wallBouncing = false;
        ceilingBouncing = false;
        floorBouncing = true;
        bounceTimer = floorBounceTime;
    }

    private void Bounce(float angle, float speed) 
    {
        Debug.Log("Angle: " + angle);
        Vector2 newVelocity = new Vector2(speed * Mathf.Cos(Mathf.Deg2Rad * angle), speed * Mathf.Sin(Mathf.Deg2Rad * angle));
        rb.velocity = newVelocity;
    }

    private float HorizontalFlipAngle(float angle) 
    {
        const int HALF_CIRCLE = 180;
        const int FULL_CIRCLE = 360;

        if (angle > 180)
        {
            return HALF_CIRCLE + FULL_CIRCLE - angle;
        }
        else 
        {
            return HALF_CIRCLE - angle;
        }
    }

    private bool IsFacingLeft() 
    {
        return transform.forward.x < 0.0f;
    }
}
