using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionLockEnemy : MonoBehaviour, IHitboxResponder
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private int damage;
    [SerializeField] private Hitbox grabBox;
    [SerializeField] private Hitbox visionBox;
    [SerializeField] private Transform heightCheckPoint;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask primaryLayer;
    [SerializeField] private LayerMask secondaryLayer;
    private Transform player = null;
    private Rigidbody2D rb;
    private bool scanningPlayer = false;
    private bool retreating = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameObject.layer = primaryLayer.value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CollideWith(Collider2D hit) 
    {
        if (scanningPlayer)
        {
            player = hit.GetComponentInParent<Transform>();
        }
        else 
        {
            IHitable hitOptions = hit.GetComponent<IHitable>();

            if (hitOptions != null) 
            {
                hitOptions.Hit(damage);
            }
        }
    }

    private bool IsFacingLeft()
    {
        return transform.forward.x < 0.0f;
    }

    private void MoveLeft() 
    {
        rb.velocity = new Vector2(-speed, rb.velocity.y);
    }

    private void MoveRight() 
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    private void Grab() 
    {
        grabBox.SetResponder(this);
        grabBox.StartCheckingCollisions();
        grabBox.HitboxUpdate();
    }
}
