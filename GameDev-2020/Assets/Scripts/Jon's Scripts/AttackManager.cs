using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    private bool attackButtonDown = false;
    private bool grappleButtonDown = false;
    private bool downButtonPressed = false;
    private bool canAttack = true;
    private PlayerState playerState;
    private Attack basicMeleeAttack;
    private Attack diveAttack;
    private Attack caneGrapple;
    private Attack currentAttack = null;
    private Attack AirAttack;

    void Start()
    {
        attackButtonDown = false;
        canAttack = true;
        currentAttack = null;
        basicMeleeAttack = GetComponent<BasicAttack>();
        diveAttack = GetComponent<DiveAttack>();
        playerState = GetComponent<PlayerState>();
        caneGrapple = GetComponent<CaneGrapple>();
        AirAttack = GetComponent<AirAttack>();
    }

    void Update()
    {
        grappleButtonDown = Input.GetButtonDown("Grapple") && !playerState.IsInteracting()
                            && !playerState.IsKnockback();
        attackButtonDown = Input.GetButtonDown("Attack") && !playerState.IsInteracting()
                            && !playerState.IsKnockback(); ;
        downButtonPressed = Input.GetAxisRaw("Vertical") < 0.0f;

        if (!playerState.IsAttacking())
        {
            currentAttack = null;
        }

        if (canAttack)
        {
            Attack selectedAttack = SelectPossibleAttack();

            if (selectedAttack != null)
            {
                if (currentAttack != null)
                {
                    currentAttack.Break();
                }

                currentAttack = selectedAttack;
                currentAttack.StartAttack();
            }
        }
        
    }

    private Attack SelectPossibleAttack()
    {
        Attack selectedAttack = null;

        if (!playerState.IsAttacking() || (currentAttack != null && currentAttack.CanAttackBreak()))
        {
            if (attackButtonDown)
            {
                if (downButtonPressed)
                {
                    if (diveAttack.CanAttack())
                    {
                        selectedAttack = diveAttack;
                    }
                }
                else if (basicMeleeAttack.CanAttack())
                {
                    selectedAttack = basicMeleeAttack;
                }
                else if(AirAttack.CanAttack())
                {
                    selectedAttack = AirAttack;
                }
            }
            else if (grappleButtonDown) 
            {
                if (caneGrapple.CanAttack()) 
                {
                    Debug.Log("Grapple");
                    selectedAttack = caneGrapple;
                }
            }
        }

        return selectedAttack;
    }

    public void StopAttacking() 
    {
        if (currentAttack != null) 
        {
            currentAttack.Break();
            currentAttack = null;
        }

        canAttack = false;
    }

    public void StartAttacking() 
    {
        canAttack = true;
    }

    public void StopCurrentAttack() 
    {
        if (currentAttack != null) 
        {
            currentAttack.Break();
            currentAttack = null;
        }
    }
}
