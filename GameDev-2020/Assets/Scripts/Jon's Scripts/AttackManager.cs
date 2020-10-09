using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    private bool attackButtonDown = false;
    private bool downButtonPressed = false;
    private bool canAttack = true;
    private PlayerState playerState;
    private Attack basicMeleeAttack;
    private Attack diveAttack;
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
        AirAttack = GetComponent<AirAttack>();
    }

    void Update()
    {
        attackButtonDown = Input.GetButtonDown("Attack");
        downButtonPressed = Input.GetAxis("Vertical") < 0.0f;

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
                        Debug.Log("Dive");
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
        }

        return selectedAttack;
    }

    public void StopAttacking() 
    {
        canAttack = false;
    }

    public void StartAttacking() 
    {
        canAttack = true;
    }
}
