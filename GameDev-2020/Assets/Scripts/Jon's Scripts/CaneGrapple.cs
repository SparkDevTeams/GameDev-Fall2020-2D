using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaneGrapple : Attack, IHitboxResponder
{
    [SerializeField] private int damage = 5;
    [SerializeField] private float maxRayCastDist = 60.0f;
    [SerializeField] private float grappleSpeed = 20.0f;
    [SerializeField] private float throwSpeed = 18.0f;
    [SerializeField] private float cooldownTime = 0.2f;
    [SerializeField] private float minRopeDistance = 0.2f;
    private float raycastDist = 0.0f;
    private float cooldownTimer = 0.0f;

    private bool locked = false;
    private bool wallHit = false;
    private bool enemyHit = false;
    private bool startingInAir = false;
    private Vector3 throwDirection;
    private Vector2 grapplePoint;
    private DimensionManager dimension;
    private PlayerState playerState;
    private PlayerMovement movement;
    private Rigidbody2D rb;
    private GrappleBounce bounce;
    private LayerMask currentLayer;
    [SerializeField] private Transform startPos;
    [SerializeField] private Hitbox sideCheck;
    [SerializeField] private Hitbox topCheck;
    [SerializeField] private Hitbox bottomCheck;
    [SerializeField] private LayerMask positiveLayer;
    [SerializeField] private LayerMask negativeLayer;
    [SerializeField] private LayerMask nullLayer;

    void Start()
    {
        dimension = GetComponent<DimensionManager>();
        playerState = GetComponent<PlayerState>();
        movement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        bounce = GetComponent<GrappleBounce>();
    }

    void Update()
    {
        if (cooldownTimer > 0.0f)
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer <= 0.0f) 
            {
                cooldownTimer = 0.0f;
            }
        }
    }

    void FixedUpdate() 
    {
        if (attackInit) 
        {
            UpdateCurrentLayer();

            if (!locked)
            {
                FireRayCast();
            }
            else if (locked) 
            {
                CheckSide();
                CheckBottom();
                CheckTop();

                if (!wallHit && !enemyHit && Vector2.Distance(startPos.position, grapplePoint) < minRopeDistance)
                {
                    Break();
                }
                else if (!wallHit && !enemyHit) 
                {
                    rb.velocity = CalculateVectorTowards(grapplePoint);
                }
            }
        }

        if (wallHit || enemyHit) 
        {
            playerState.SetAttacking(false);
            wallHit = false;
            enemyHit = false;
            cooldownTimer = cooldownTime;
            topCheck.StopCheckingCollisions();
            sideCheck.StopCheckingCollisions();
            bottomCheck.StopCheckingCollisions();
        }
    }

    public override bool CanAttack() 
    {
        return !playerState.IsDashing() && !playerState.IsInteracting() && (cooldownTimer <= 0.0f);
    }

    public override void StartAttack() 
    {
        playerState.SetAttacking(true);
        raycastDist = 0.0f;
        attackInit = true;
        locked = false;
        wallHit = false;
        enemyHit = false;
        startingInAir = !playerState.IsGrounded();
        movement.DisableMovement();
        rb.gravityScale = 0.0f;
        throwDirection = new Vector2(Input.GetAxisRaw("Horizontal")
                            , Input.GetAxisRaw("Vertical")).normalized;

        if (throwDirection.magnitude == 0) 
        {
            throwDirection = new Vector2(1.0f, 0.0f);
        }

        if(!startingInAir && throwDirection.y < 0.0f) 
        {
            throwDirection = new Vector2(throwDirection.x, 0.0f).normalized;
        }

        if (throwDirection.x < 0.0f)
        {
            FaceLeft();
        }
        else if (throwDirection.x > 0.0f) 
        {
            FaceRight();
        }

        Debug.Log("Throw Dir: " + throwDirection);
    }

    public override void Break()
    {
        playerState.SetAttacking(false);
        raycastDist = 0.0f;
        cooldownTimer = cooldownTime;
        attackInit = false;
        locked = false;
        wallHit = false;
        enemyHit = false;
        startingInAir = false;
        movement.ResetGravityDefault();
        movement.EnableMovement();
    }

    public void CollideWith(Collider2D collision) 
    {
        IHitable hitOptions = collision.GetComponentInParent<IHitable>();

        if (hitOptions != null)
        {
            hitOptions.Hit(damage);
            enemyHit = true;
        }
        else 
        {
            wallHit = true;
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

    private void FireRayCast() 
    {
        raycastDist += throwSpeed;

        if (raycastDist > maxRayCastDist) 
        {
            raycastDist = maxRayCastDist;
        }

        RaycastHit2D hit = Physics2D.Raycast(startPos.position, throwDirection, raycastDist, currentLayer);

        if (hit.collider != null)
        {
            grapplePoint = hit.point;
            locked = true;
            topCheck.SetResponder(this);
            topCheck.StartCheckingCollisions();
            sideCheck.SetResponder(this);
            sideCheck.StartCheckingCollisions();

            if (startingInAir)
            {
                bottomCheck.SetResponder(this);
                bottomCheck.StartCheckingCollisions();
            }

            Debug.DrawLine(startPos.position, startPos.position + (throwDirection * raycastDist), Color.green);
        }
        else 
        {
            if (raycastDist >= maxRayCastDist) 
            {
                Break();
            }

            Debug.DrawLine(startPos.position, startPos.position + (throwDirection * raycastDist), Color.red);
        }
    }

    private Vector2 CalculateVectorTowards(Vector2 point)
    {
        return (point - (Vector2)startPos.position).normalized * grappleSpeed;
    }

    private void CheckTop() 
    {
        if (!wallHit && !enemyHit) 
        {
            topCheck.HitboxUpdate(currentLayer);

            if (enemyHit)
            {
                locked = false;
                attackInit = false;
                bounce.StartCeilingBounce();
                Debug.Log("Enemy Top bounce!");
            }
            else if (wallHit)
            {
                locked = false;
                attackInit = false;
                bounce.StartCeilingBounce();
                Debug.Log("Wall Top bounce!");
            }
        }
    }

    private void CheckSide()
    {
        if (!wallHit && !enemyHit)
        {
            sideCheck.HitboxUpdate(currentLayer);

            if (enemyHit)
            {
                locked = false;
                attackInit = false;
                bounce.StartWallBounce();
                Debug.Log("Enemy Side bounce!");
            }
            else if (wallHit)
            {
                locked = false;
                attackInit = false;
                bounce.StartWallBounce();
                Debug.Log("Wall Side bounce!");
            }
        }
    }

    private void CheckBottom()
    {
        if (!wallHit && !enemyHit && startingInAir)
        {
            bottomCheck.HitboxUpdate(currentLayer);

            if (enemyHit)
            {
                locked = false;
                attackInit = false;
                bounce.StartFloorBounce();
                Debug.Log("Enemy Bottom bounce!");
            }
            else if (wallHit)
            {
                locked = false;
                attackInit = false;
                bounce.StartFloorBounce();
                Debug.Log("Wall Bottom bounce!");
            }
        }
    }

    private void FaceRight()
    {
        transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
    }

    private void FaceLeft()
    {
        transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
    }
}
