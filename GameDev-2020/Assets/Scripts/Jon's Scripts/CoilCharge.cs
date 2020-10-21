using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoilCharge : MonoBehaviour, IHitboxResponder
{
    [SerializeField] private float speed = 18.0f;
    [SerializeField] private float minDist = 0.2f;
    [SerializeField] private int damage = 5;
    private bool attacking = false;
    private bool attacked = false;
    [SerializeField] private Hitbox hitbox;
    private Transform target;

    void Start()
    {
        attacking = false;
        attacked = false;
    }

    void FixedUpdate()
    {
        if (attacking)
        {
            if (target != null)
            {
                transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);

                if (Vector3.Distance(transform.position, target.position) < minDist)
                {
                    hitbox.SetResponder(this);
                    hitbox.StartCheckingCollisions();
                    hitbox.HitboxUpdate();
                    StopCharge();
                    attacked = true;
                }
            }
            else
            {
                StopCharge();
            }
        }
        else if (attacked) 
        {
            hitbox.StopCheckingCollisions();
            attacked = false;
        }
    }

    public void CollideWith(Collider2D collision)
    {
        IHitable hitOptions = collision.GetComponentInParent<IHitable>();

        if (hitOptions != null)
        {
            hitOptions.Hit(damage);
        }
    }

    public void StartCharge(Transform newTarget) 
    {
        target = newTarget;
        attacking = true;
        attacked = false;
    }

    public void StopCharge() 
    {
        target = null;
        attacking = false;
    }

    public bool IsCharging() 
    {
        return attacking;
    }
}
