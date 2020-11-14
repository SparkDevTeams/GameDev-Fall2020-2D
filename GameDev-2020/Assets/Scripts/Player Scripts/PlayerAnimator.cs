using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private PlayerState playerState;
    private Rigidbody2D rb;
    private bool horMove = false;
    private string currentState;
    const string Player_Falling = "Player_Falling";
    const string Player_Idle = "Player_Idle";
    const string Player_Running = "Player_Running";
    const string Player_Jumping = "Player_Jumping";
    const string Player_Dash = "Player_Dash";
    const string Player_AirAttack = "Player_AirAttack";
    const string Player_BasicAttack = "Player_BasicAttack";
    const string Player_Grapple = "Player_Grapple";
    const string Player_DiveAttack = "Player_DiveAttack";
    const string Player_DiveAttackLanding = "Player_DiveAttackLanding";



    private void Start()
    {
        playerState = GetComponent<PlayerState>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //Set the player's first/default animation to idling
        animator.Play(Player_Idle);
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);
        currentState = newState;
    }
    void Update()
    {
        horMove = Input.GetAxisRaw("Horizontal") != 0.0f;
        Debug.Log(horMove);
        //Checks for player idling and plays idle animation
        if (playerState.IsGrounded() && (!horMove) && !playerState.IsAttacking())
        {
            Debug.Log("idle");
            ChangeAnimationState(Player_Idle);
        }
        //Checks for player moving and plays running animation
        else if (playerState.IsGrounded() && (horMove) && !playerState.IsAttacking() && !playerState.IsDashing())
        {
            Debug.Log("running");
            ChangeAnimationState(Player_Running);
        }
        //Checks if player is jumping and plays jumping animation
        else if (!playerState.IsGrounded() && !playerState.IsDashing() && !playerState.IsAttacking())
        {
            if (rb.velocity.y >= 0)
            {
                Debug.Log("Jumping");
                ChangeAnimationState(Player_Jumping);
            }
            else if (rb.velocity.y < 0)
            {
                Debug.Log("falling");
                ChangeAnimationState(Player_Falling);
            }
        }
        //Checks if player is dashing and plays dash animation
        else if (playerState.IsDashing() && !playerState.IsAttacking())
        {
            Debug.Log("Dashing");
            ChangeAnimationState(Player_Dash);
        }
    }

    public void basicAttackAnimation()
    {
        Debug.Log("BasicAttack");
        ChangeAnimationState(Player_BasicAttack);
    }
    public void airAttackAnimation()
    {
        Debug.Log("AirAttack");
        ChangeAnimationState(Player_AirAttack);
    }
    public void GrappleAnimation()
    {
        Debug.Log("GrappleAttack");
        ChangeAnimationState(Player_Grapple);
    }
    public void diveAttackAnimation()
    {
        Debug.Log("diveAttack");
        ChangeAnimationState(Player_DiveAttack);
    }
    public void diveAttackLandingAnimation()
    {
        Debug.Log("diveAttackLanding");
        ChangeAnimationState(Player_DiveAttackLanding);
    }
}
