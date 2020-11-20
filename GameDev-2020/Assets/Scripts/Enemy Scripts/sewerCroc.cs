using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sewerCroc : MonoBehaviour, IHitable, IHitboxResponder
{
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float range;
    [SerializeField] private Hitbox hitbox;
    [SerializeField] private GameObject playerOb;
    [SerializeField] private int hp;
    [SerializeField] private int maxhp;
    [SerializeField] private int damage = 10;
    [SerializeField] private float attackBuffer = 0.1f;
    private float bufferTimer;
    private Collider2D hitRange;
    private RaycastHit2D hit;
    private Transform player;
    private Rigidbody2D rb;
    private BoxCollider2D enemyCollider;
    private BoxCollider2D playerCollider;
    private bool inRange;
    private bool turn = false;
    private bool wandering;
    private bool inSight;
    private bool lunging;
    private int layerMaskPlayer;
    private int layerMaskGround;
    private float distToGround;
    private float lungeTimer = 0f;
    private float attackTimer = 0f;
    private float turnTimer = 0f;
    private float playerDistance;
    private float minDist;


    void Start()
    {
        wandering = true;
        layerMaskGround = (LayerMask.GetMask("Ground"));
        layerMaskPlayer = (LayerMask.GetMask("Player"));
        rb = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<BoxCollider2D>();
        playerCollider = playerOb.GetComponent<BoxCollider2D>();
        distToGround = enemyCollider.bounds.extents.y;
        minDist = (playerCollider.bounds.extents.x) + (enemyCollider.bounds.extents.x) + .01f;
        hp = maxhp;
    }

    void Update()
    {
        //If the player is not currently being tracked begin checking the overlap circle
        if(player == null)
            hitRange = Physics2D.OverlapCircle(transform.position, range, layerMaskPlayer);

        //Begins tracking the player's position if within the set range of the overlap circle
        if(hitRange)
        {
            player = hitRange.GetComponentInParent<Transform>();
            inRange = true;
            playerDistance = Vector3.Distance(player.position, transform.position);
        }

        //If the player is within range of the gator and it is able to attack then the gator begins attacking and opening hitboxes
        if(hitRange && playerDistance < 1.45 && attackTimer <= 0f)
        {
            Bite();
            attackTimer = 1f;
            bufferTimer = attackBuffer;
        }
        if (bufferTimer > 0.0f)
        {
            bufferTimer -= Time.deltaTime;

            if (bufferTimer <= 0.0f)
            {
                bufferTimer = 0.0f;
                hitbox.StopCheckingCollisions();
            }
        }

        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }

        if(turnTimer > 0)
        {
            turnTimer -= Time.deltaTime;
        }

        //Creates raycast to determine if the player is within line of sight
        if (inRange)
            hit = Physics2D.Raycast(transform.position, (player.transform.position - transform.position).normalized, playerDistance, layerMaskGround);

        //If the raycast is not colliding with a wall and the player is within range then the player is in sight
        inSight = (!hit && inRange) ? true : false;

        //Decreases the gator's lungetimer
        if(lungeTimer > 0)
        {
            lungeTimer -= Time.deltaTime;
        }
        //If the player is in range and in sight then the gator will begin chasing, otherwise the gator will wander
        if (inSight && inRange)
        {
            wandering = false;
        }
        else if (!inRange)
        {
            wandering = true;
        }
    }

    private void FixedUpdate()
    {
        if (hp <= 0)
            Destroy(gameObject);

        if (isGrounded() && lungeTimer < 2f)
        {
            lunging = false;
        }
        //Lunges at a player within some range
        if ((playerDistance < 4 && playerDistance > 2) && (Random.Range(0,50) < 10) && lungeTimer <= 0 && isGrounded())
        {
            lunge();
        }
        
        

        //When the gator is not wandering it chases the player
        if (!wandering)
        {
            
            
            if ((transform.position.x < player.transform.position.x) && isGrounded() && !lunging && xDistance() > minDist)
            {
                rb.velocity = new Vector2(walkSpeed, rb.velocity.y);
                faceLeft();
            }
            else if ((transform.position.x > player.transform.position.x) && isGrounded() && !lunging && xDistance() > minDist)
            {
                rb.velocity = new Vector2(-walkSpeed, rb.velocity.y);
                faceRight();
            }
            else if(xDistance() <= minDist)
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }
        }
        //Gator slowly moves left and right when wandering
        else if (wandering)
        {
            if(turnTimer <= 0f)
            {
                turnTimer = 4f;
                if(turn)
                {
                    rb.velocity = new Vector2(-1.5f, rb.velocity.y);
                    faceRight();
                    turn = false;
                }
                else
                {
                    rb.velocity = new Vector2(1.5f, rb.velocity.y);
                    faceLeft();
                    turn = true;
                }
            }
        }
    }

    private void faceLeft()
    {
        transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
    }

    private void faceRight()
    {
        transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
    }

    private bool facingLeft()
    {
        return transform.eulerAngles.y == 180f;
    }
    private void lunge()
    {
        if(facingLeft())
            rb.velocity = new Vector2(10f, 2.5f);
        else
            rb.velocity = new Vector2(-10f, 2.5f);
        lungeTimer = 2.5f;
        lunging = true;
    }

    private bool isGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, (float)((float) distToGround + 0.1), layerMaskGround);
    }
    private float xDistance()
    {
        return Mathf.Abs(transform.position.x - player.transform.position.x);
    }
    public void Hit(int damage)
    {
        hp -= damage;
        Debug.Log("Damage Taken");
        if (hp <= 0)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
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
    private void Bite()
    { 
        hitbox.SetResponder(this);
        hitbox.StartCheckingCollisions();
        hitbox.HitboxUpdate();
    }

    //TODO: add gator animations
}

