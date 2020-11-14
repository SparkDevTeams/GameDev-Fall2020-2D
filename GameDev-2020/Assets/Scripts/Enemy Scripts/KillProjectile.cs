using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillProjectile : MonoBehaviour
{
    [HideInInspector] public float deathTimer;
    private DimensionManager dimensionManager;

    private const int positiveLayer = 8;
    private const int negativeLayer = 9;
    private const int projectileLayer = 14;

    private void Start() {
        dimensionManager = FindObjectOfType<DimensionManager>();
    }

    private void Update()
    {
        checkDimension();
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

    public void checkDimension()
    {
        switch (dimensionManager.getDimensionID())
        {
            case 1:
                Physics2D.IgnoreLayerCollision(negativeLayer, projectileLayer, true);
                Physics2D.IgnoreLayerCollision(positiveLayer, projectileLayer, false);
                break;

            case 2:
                Physics2D.IgnoreLayerCollision(projectileLayer, negativeLayer, false);
                Physics2D.IgnoreLayerCollision(projectileLayer, positiveLayer, true);
                break;

            default:
                Physics2D.IgnoreLayerCollision(projectileLayer, negativeLayer, true);
                Physics2D.IgnoreLayerCollision(projectileLayer, positiveLayer, true);
                break;
        }
    }
}