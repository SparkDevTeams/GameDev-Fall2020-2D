using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int shieldHealth;
    [SerializeField] private bool isHit;

    [SerializeField] private float invincibilityTime;
    [SerializeField] private float currentTimer;
    // Start is called before the first frame update
    void Start()
    {
        currentTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (shieldHealth <= 0)
        {
            shieldHealth = 0;
        }

        if (isHit)
        {
            currentTimer += Time.deltaTime;

            if (currentTimer >= invincibilityTime)
            {
                isHit = false;
                currentTimer = 0;
            }
        }

    }

    public void damagePlayer(int incomingDamage)
    {
        if (!isHit)
        {
            shieldHealth -= incomingDamage;
            isHit = true;
        }

    }
}
