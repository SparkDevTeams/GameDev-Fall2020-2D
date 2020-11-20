using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    private bool grounded = false;
    private bool jumping = false;
    [SerializeField] private bool dashing = false;
    private bool interacting = false;
    private bool attacking = false;
    private bool grappleBouncing = false;
    private bool knockback = false;

    public bool IsGrounded() 
    {
        return grounded;
    }

    public void SetGrounded(bool newState) 
    {
        grounded = newState;
    }

    public bool IsJumping()
    {
        return jumping;
    }

    public void SetJumping(bool newState)
    {
        jumping = newState;
    }

    public bool IsDashing()
    {
        return dashing;
    }

    public void SetDashing(bool newState)
    {
        dashing = newState;
    }

    public bool IsInteracting()
    {
        return interacting;
    }

    public void SetInteracting(bool newState)
    {
        interacting = newState;
    }

    public bool IsAttacking()
    {
        return attacking;
    }

    public void SetAttacking(bool newState)
    {
        attacking = newState;
    }

    public bool IsGrappleBouncing() 
    {
        return grappleBouncing;
    }

    public void SetGrappleBouncing(bool newState) 
    {
        grappleBouncing = newState;
    }

    public bool IsKnockback()
    {
        return knockback;
    }

    public void SetKnockback(bool newState)
    {
        knockback = newState;
    }
}
