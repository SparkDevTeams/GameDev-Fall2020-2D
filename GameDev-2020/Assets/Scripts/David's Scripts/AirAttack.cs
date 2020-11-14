using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirAttack : Attack, IHitboxResponder
{

    [SerializeField] private Hitbox[] hitboxes = new Hitbox[5];
    [SerializeField] private int damage = 3;
    [SerializeField] private float attackBuffer = 0.1f;
    [SerializeField] private LayerMask positiveLayer;
    [SerializeField] private LayerMask negativeLayer;
    [SerializeField] private LayerMask nullLayer;
    private LayerMask currentLayer;
    private DimensionManager dimension;
    private PlayerState playerState;
    private float bufferTimer = 0.0f;
    private bool hit = false;

    void Start()
    {
        attackInit = false;
        bufferTimer = 0.0f;
        playerState = GetComponent<PlayerState>();
        dimension = GetComponent<DimensionManager>();
    }

    void Update()
    {
        if(attackInit)
        {
            Swing();
            bufferTimer = attackBuffer;
            attackInit = false;
        }
        else if(bufferTimer > 0.0f)
        {
            bufferTimer -= Time.deltaTime;
            if(bufferTimer <= 0.0f)
            {
                bufferTimer = 0.0f;
                playerState.SetAttacking(false);
                hit = false;
            }
        }
    }
    public override bool CanAttack()
    {
        return !playerState.IsDashing() && !playerState.IsGrounded();
    }

    public override void StartAttack()
    {
        //Debug.Log("swing");
        playerState.SetAttacking(true);
        attackInit = true;
    }

    public override void Break()
    {
        attackInit = false;
        hit = false;
        bufferTimer = 0.0f;
        playerState.SetAttacking(false);
    }

    public void CollideWith(Collider2D collision)
    {
        IHitable hitOptions = collision.GetComponentInParent<IHitable>();

        if (hitOptions != null)
        {
            Debug.Log("AirAttackSwing");
            hitOptions.Hit(damage);
        }
    }

    private void Swing()
    {
        UpdateCurrentLayer();

        for (int i = 0; i < 5; i++)
        {
            hitboxes[i].SetResponder(this);
            hitboxes[i].StartCheckingCollisions();
            hitboxes[i].HitboxUpdate(currentLayer);
        }
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
