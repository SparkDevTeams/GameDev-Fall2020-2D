using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillProjectile : MonoBehaviour
{
    [HideInInspector] public float deathTimer;

    private void Update()
    {
        deathTimer -= Time.deltaTime;
        if (deathTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D()
    {
        Destroy(gameObject);
    }
}