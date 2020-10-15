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
    private FollowPoint follow;

    void Start()
    {
        follow = GetComponent<FollowPoint>();
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
                    StopStrike();
                    attacked = true;
                }
            }
            else
            {
                StopStrike();
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

    public void StartStrike(Transform newTarget) 
    {
        follow.StopFollowing();
        target = newTarget;
        attacking = true;
        attacked = false;
    }

    public void StopStrike() 
    {
        target = null;
        attacking = false;
        follow.Follow();
    }
}
