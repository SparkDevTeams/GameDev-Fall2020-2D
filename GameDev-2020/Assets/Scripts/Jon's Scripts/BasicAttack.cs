using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : Attack, IHitboxResponder
{
    [SerializeField] private Hitbox hitbox;
    [SerializeField] private int damage = 5;
    [SerializeField] private float attackBuffer = 0.1f;
    [SerializeField] private LayerMask positiveLayer;
    [SerializeField] private LayerMask negativeLayer;
    [SerializeField] private LayerMask nullLayer;

    private LayerMask currentLayer;
    private DimensionManager dimension;
    private PlayerState playerState;
    private float bufferTimer = 0.0f;

    void Start()
    {
        attackInit = false;
        bufferTimer = 0.0f;
        playerState = GetComponent<PlayerState>();
        dimension = GetComponent<DimensionManager>();
    }

    void Update()
    {
        if (attackInit)
        {
            Swing();
            bufferTimer = attackBuffer;
            attackInit = false;
        }
        else if (bufferTimer > 0.0f) 
        {
            bufferTimer -= Time.deltaTime;

            if (bufferTimer <= 0.0f) 
            {
                bufferTimer = 0.0f;
                playerState.SetAttacking(false);
                hitbox.StopCheckingCollisions();
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
    }

    public override bool CanAttack()
    {
        return !playerState.IsDashing() && playerState.IsGrounded() && !playerState.IsJumping();
    }

    public override void StartAttack()
    {
        playerState.SetAttacking(true);
        attackInit = true;
    }

    public override void Break()
    {
        attackInit = false;
        bufferTimer = 0.0f;
        playerState.SetAttacking(false);

        if (hitbox != null) 
        {
            hitbox.StopCheckingCollisions();
        }
    }

    private void Swing() 
    {
        UpdateCurrentLayer();
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
