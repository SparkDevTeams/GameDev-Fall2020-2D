using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rollingEnemy : MonoBehaviour, IHitable, IHitboxResponder
{
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float range;
    [SerializeField] private Hitbox hitbox;
    [SerializeField] private int hp = 200;
    [SerializeField] private int damage = 10;
    [SerializeField] private float attackBuffer = 0.1f;
    [SerializeField] private GameObject playerOb;
    [SerializeField] private float maxStunTime;
    private float bufferTimer;
    private Collider2D hitRange;
    private RaycastHit2D hit;
    private Transform player;
    private Rigidbody2D rb;
    private BoxCollider2D playerCollider;
    private BoxCollider2D enemyCollider;
    private bool inRange;
    private bool turn = false;
    private bool wandering;
    private bool inSight;
    private bool stunned;
    private bool isRolling;
    private float rollMaxSpeed;
    private float rollIncrement;
    private float rollSpeed;
    private float minDist;
    private int layerMaskPlayer;
    private int layerMaskGround;
    private float distToGround;
    private float attackTimer = 0f;
    private float turnTimer = 0f;
    private float stunTimer;
    private float playerDistance;


    void Start()
    {
        stunned = false;
        isRolling = false;
        wandering = true;
        layerMaskGround = (LayerMask.GetMask("Ground"));
        layerMaskPlayer = (LayerMask.GetMask("Player"));
        rb = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<BoxCollider2D>();
        playerCollider = playerOb.GetComponent<BoxCollider2D>();
        distToGround = enemyCollider.bounds.extents.y;
        rollMaxSpeed = 15;
        rollIncrement = 2f;
        minDist = (playerCollider.bounds.extents.x) + (enemyCollider.bounds.extents.x) + .01f;
    }

    void Update()
    {
        //If the player is not currently being tracked begin checking the overlap circle
        if (player == null)
            hitRange = Physics2D.OverlapCircle(transform.position, range, layerMaskPlayer);

        //Begins tracking the player's position if within the set range of the overlap circle
        if (hitRange)
        {
            player = hitRange.GetComponentInParent<Transform>();
            inRange = true;
            playerDistance = Vector3.Distance(player.position, transform.position);
        }

        //If the player is within range of the gator and it is able to attack then the gator begins attacking and opening hitboxes
        if (hitRange && playerDistance < 1.45 && attackTimer <= 0f)
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

        if (turnTimer > 0)
        {
            turnTimer -= Time.deltaTime;
        }

        if(stunTimer > 0)
        {
            stunTimer -= Time.deltaTime;
            if(stunTimer <= 0)
            {
                stunTimer = 0f;
                stunned = false;
            }
        }

        //Creates raycast to determine if the player is within line of sight
        if (inRange)
            hit = Physics2D.Raycast(transform.position, (player.transform.position - transform.position).normalized, playerDistance, layerMaskGround);

        //If the raycast is not colliding with a wall and the player is within range then the player is in sight
        inSight = (!hit && inRange) ? true : false;

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
        
        if (!wandering)
        {
            //rolls in direction of player
            if (isRolling)
            {
                
                //Debug.Log(transform.eulerAngles.y);
                if (facingLeft())
                {
                    //Debug.Log("ROLL LEFT");
                    rb.velocity = new Vector2(rollSpeed, rb.velocity.y);
                    if (rollSpeed < rollMaxSpeed)
                        rollSpeed += (rollIncrement * Time.deltaTime);
                }
                else
                {
                    //Debug.Log("ROLL RIGHT");
                    rb.velocity = new Vector2(-rollSpeed, rb.velocity.y);
                    if (rollSpeed < rollMaxSpeed)
                        rollSpeed += (rollIncrement * Time.deltaTime);
                }
            }
            if (!stunned)
            {
                //Initiates roll
                if ((transform.position.x < player.transform.position.x) && isGrounded() /*&& !lunging */&& xDistance() > (range / 2) && !isRolling)
                {
                    //rollSpeed = 0f;
                    isRolling = true;
                    faceLeft();
                }
                else if ((transform.position.x > player.transform.position.x) && isGrounded() /*&& !lunging */&& xDistance() > (range / 2) && !isRolling)
                {
                    //rollSpeed = 0f;
                    isRolling = true;
                    faceRight();
                }
                //Walk towards player
                if ((transform.position.x < player.transform.position.x) && isGrounded() /*&& !lunging */&& xDistance() > minDist && xDistance() < (range / 2) && !isRolling)
                {
                    rb.velocity = new Vector2(walkSpeed, rb.velocity.y);
                    faceLeft();
                }
                else if ((transform.position.x > player.transform.position.x) && isGrounded() /*&& !lunging */&& xDistance() > minDist && xDistance() < (range / 2) && !isRolling)
                {
                    rb.velocity = new Vector2(-walkSpeed, rb.velocity.y);
                    faceRight();
                }
            }
            
  
            if (playerCollide() || wallCollide())
            {
                if(isRolling)
                {
                    Bite();
                    stunTimer = maxStunTime;
                    stunned = true;
                }
                rb.velocity = new Vector2(0f, rb.velocity.y);
                isRolling = false;
                rollSpeed = 0f;
            }
            
        }
       
        else if (wandering && !stunned)
        {
            if (turnTimer <= 0f)
            {
                turnTimer = 4f;
                if (turn)
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
    
    private bool isGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, (float)(distToGround + 0.001f), layerMaskGround);
    }

    private bool wallCollide()
    {
        bool hitwall = false;

        if (facingLeft())
        {
            //Debug.Log(Physics2D.Raycast(transform.position, Vector2.left, (float)(collider.bounds.extents.x + .05), layerMaskGround));
            hitwall = Physics2D.Raycast(transform.position, Vector2.right, (float)(enemyCollider.bounds.extents.x + .05), layerMaskGround);
        }
        else
        {
            //Debug.Log(Physics2D.Raycast(transform.position, Vector2.right, (float)(collider.bounds.extents.x + .05), layerMaskGround));
            hitwall = Physics2D.Raycast(transform.position, Vector2.left, (float)(enemyCollider.bounds.extents.x + .05), layerMaskGround);
        }
        //Debug.Log("Hitting wall: " + hitwall);
        return hitwall;
    }

    private bool playerCollide()
    {
        bool hitPlayer = false;

        if (facingLeft())
            hitPlayer = Physics2D.Raycast(transform.position, Vector2.right, (float)(enemyCollider.bounds.extents.x + .05), layerMaskPlayer);
        else
            hitPlayer = Physics2D.Raycast(transform.position, Vector2.left, (float)(enemyCollider.bounds.extents.x + .05), layerMaskPlayer);
        //Debug.Log("Hitting Player: " + hitPlayer);
        return hitPlayer;
    }

    private float xDistance()
    {
        return Mathf.Abs(transform.position.x - player.transform.position.x);
    }
    public void Hit(int damage)
    {
        hp -= damage;
        Debug.Log("Took " + damage + " damage HP: " + hp);

        if (hp <= 0)
        {
            gameObject.SetActive(false);
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
}