using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoilLaunch : MonoBehaviour, IHitboxResponder
{
    [SerializeField] private Hitbox hitbox;
    [SerializeField] private DimensionManager dimension;
    [SerializeField] private Transform launchPoint;
    [SerializeField] private LayerMask positiveLayer;
    [SerializeField] private LayerMask negativeLayer;
    [SerializeField] private LayerMask nullLayer;
    [SerializeField] private int damage = 5;
    [SerializeField] private float launchingSpeed = 6.5f;
    [SerializeField] private float positioningSpeed = 15.0f;
    [SerializeField] private float minDistToPoint = 0.2f;
    [SerializeField] private float launchTime = 3.0f;
    private float launchTimer = 0.0f;
    private LayerMask currentLayer;
    private Vector3 launchDir;
    private bool launching = false;

    void FixedUpdate()
    {
        if (launching)
        {
            if (launchTimer > 0.0f)
            {
                launchTimer -= Time.deltaTime;

                if (launchTimer <= 0.0f)
                {
                    launchTimer = 0.0f;
                    launching = false;
                }
                else
                {
                    UpdateCurrentLayer();
                    transform.position += (launchDir * Time.deltaTime * launchingSpeed);
                    CheckHitbox();
                }
            }
            else 
            {
                transform.position = Vector3.Lerp(transform.position, launchPoint.position, Time.deltaTime * positioningSpeed);

                if (Vector2.Distance(transform.position, launchPoint.position) < minDistToPoint) 
                {
                    transform.position = launchPoint.position;
                    launchTimer = launchTime;
                }
            }
        }
    }

    public void CollideWith(Collider2D collision)
    {
        IHitable hitOptions = collision.GetComponentInParent<IHitable>();

        if (hitOptions != null)
        {
            hitOptions.Hit(damage);
        }

        launchTimer = 0.0f;
        launching = false;
    }

    public void StartLaunch(Vector3 direction) 
    {
        const int NORMALIZED_MAGNITUDE = 1;

        if (direction.magnitude == NORMALIZED_MAGNITUDE)
        {
            launchDir = direction;
        }
        else 
        {
            launchDir = direction.normalized;
        }

        launching = true;
    }

    public void StopLaunch() 
    {
        launchTimer = 0.0f;
        launchDir = Vector3.zero;
        launching = false;
    }

    public bool IsLaunching() 
    {
        return launching;
    }
            
    private void CheckHitbox() 
    {
        hitbox.SetResponder(this);
        hitbox.StartCheckingCollisions();
        hitbox.HitboxUpdate(currentLayer);
    }

    private void UpdateCurrentLayer()
    {
        int id = dimension.GetDimensionID();

        switch (id)
        {
            case 1:
                currentLayer = positiveLayer;
                break;
            case 2:
                currentLayer = negativeLayer;
                break;
            default:
                currentLayer = nullLayer;
                break;
        }
    }
}
