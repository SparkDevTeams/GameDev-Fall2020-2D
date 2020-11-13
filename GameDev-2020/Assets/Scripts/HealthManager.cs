using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int shieldHealth;
    [SerializeField] private bool isHit;
    [SerializeField] private int maxHealth;

    [SerializeField] private float tempInvincibilityTime;
    [SerializeField] private float currentTimer;
    private bool enemyBodyInvincibility = false;
    private bool totalInvincibility = false;
    // Start is called before the first frame update
    void Start()
    {
        currentTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Sets the shield health to 0 if the value goes under 0
        if (shieldHealth <= 0)
        {
            shieldHealth = 0;
        }

        //If the player was hit, start a timer in which the player will be immune to all other types of damage within the set timeframe
        //Stops timer and resets it to 0 if the alloted time is reached
        if (isHit)
        {
            currentTimer += Time.deltaTime;

            if (currentTimer >= tempInvincibilityTime)
            {
                isHit = false;
                currentTimer = 0;
            }
        }

    }

    //Function that handles damaging the player based on the incoming damage value if they have not been hit recently (Depending on the invincibility time)
    public void damagePlayer(int incomingDamage)
    {
        if (!isHit)
        {
            shieldHealth -= incomingDamage;
            isHit = true;
        }

    }

    public void bodyBoxDamagePlayer(int incomingDamage) 
    {
        if (!isHit && !enemyBodyInvincibility)
        {
            shieldHealth -= incomingDamage;
            isHit = true;
        }
    }

    public int getShieldHealth()
    {
        return shieldHealth;
    }

    public void resetHealth()
    {
        shieldHealth = maxHealth;
    }

    public void enableEnemyBodyInvincibility() 
    {
        enemyBodyInvincibility = true;
    }

    public void disableEnemyBodyInvincibility() 
    {
        enemyBodyInvincibility = false;
    }

    public void enableTotalInvincibility() 
    {
        totalInvincibility = true;
    }

    public void disableTotalInvincibility()
    {
        totalInvincibility = false;
    }
}
