using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float defaultSpeed = 6.0f;
    [SerializeField] private float defaultAngle = 70.0f;
    [SerializeField] private float defaultKnockbackTime = 0.8f;
    private float speedMultiplier = 1.0f;
    private float knockbackTimer = 0.0f;
    private Vector2 knockbackDir;
    private Rigidbody2D rb;
    private PlayerState playerState;
    private PlayerMovement movement;
    private AttackManager attacking;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerState = GetComponent<PlayerState>();
        movement = GetComponent<PlayerMovement>();
        attacking = GetComponent<AttackManager>();
    }

    void Update()
    {
        if (knockbackTimer > 0.0f) 
        {
            knockbackTimer -= Time.deltaTime;

            if (knockbackTimer <= 0.0f)
            {
                knockbackTimer = 0.0f;
                EnablePlayer();
            }
            else 
            {
                KnockbackPlayer();
            }
        }
    }

    public void RelativeKnockback(float multiplier, Vector2 attackLocation, float customLeftAngle, float customTime) 
    {
        speedMultiplier = multiplier;

        if (attackLocation.x <= transform.position.x) 
        {
            customLeftAngle = HorizontalFlipAngle(customLeftAngle);
        }

        knockbackDir = new Vector2(Mathf.Cos(Mathf.Deg2Rad * customLeftAngle)
                                    , Mathf.Sin(Mathf.Deg2Rad * customLeftAngle));
        knockbackTimer = customTime;
        DisablePlayer();
    }

    public void RelativeKnockback(float multiplier, Vector2 attackLocation, float customLeftAngle) 
    {
        RelativeKnockback(multiplier, attackLocation, customLeftAngle, defaultKnockbackTime);
    }

    public void RelativeKnockback(float multiplier, Vector2 attackLocation) 
    {
        RelativeKnockback(multiplier, attackLocation, defaultAngle, defaultKnockbackTime);
    }

    public void RelativeKnockback(Vector2 attackLocation) 
    {
        const float DEFAULT_MULT = 1.0f;
        RelativeKnockback(DEFAULT_MULT, attackLocation, defaultAngle, defaultKnockbackTime);
    }

    public void AbsoluteKnockback(float multiplier, float customAngle, float customTime) 
    {
        speedMultiplier = multiplier;
        knockbackDir = new Vector2(Mathf.Cos(Mathf.Deg2Rad * customAngle)
                                    , Mathf.Sin(Mathf.Deg2Rad * customAngle));
        knockbackTimer = customTime;
        DisablePlayer();
    }

    public void AbsoluteKnockback(float multiplier, float customAngle) 
    {
        AbsoluteKnockback(multiplier, customAngle, defaultKnockbackTime);
    }

    public void DefaultLeftKnockback()
    {
        const float DEFAULT_MULT = 1.0f;
        AbsoluteKnockback(DEFAULT_MULT, defaultAngle, defaultKnockbackTime);
    }

    public void DefaultRightKnockback() 
    {
        const float DEFAULT_MULT = 1.0f;
        AbsoluteKnockback(DEFAULT_MULT, HorizontalFlipAngle(defaultAngle), defaultKnockbackTime);
    }

    public void DefaultUpKnockback() 
    {
        const float DEFAULT_MULT = 1.0f;
        const float UP_ANGLE = 90.0f;

        AbsoluteKnockback(DEFAULT_MULT, UP_ANGLE, defaultKnockbackTime);
    }

    private void KnockbackPlayer() 
    {
        rb.velocity = knockbackDir * defaultSpeed * speedMultiplier;
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

    private void DisablePlayer() 
    {
        playerState.SetKnockback(true);
        attacking.StopCurrentAttack();
        rb.gravityScale = 0.0f;
        movement.DisableMovement();
    }

    private void EnablePlayer() 
    {
        movement.ResetGravityDefault();
        movement.EnableMovement();
        playerState.SetKnockback(false);
    }
}
