using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    // float variables
    public float moveSpeed;
    public float wallCheckRadius;

    // boolean variables
    public bool moveRight;
    private bool hittingWall;
    private bool notatEdge;

    // Transform variables
    public Transform wallCheck;
    public Transform edgeCheck;

    // LayerMask variables
    public LayerMask whatIsWall;

    // Use for initialization
    void Start()
    {

    }

    void Update()
    {
        hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);

        notatEdge = Physics2D.OverlapCircle(edgeCheck.position, wallCheckRadius, whatIsWall);

        // Checks if the player is gonna hit a wall
        if (hittingWall || !notatEdge )
        {
            moveRight = !moveRight;
        }
        
        // Checks if the player is moving left or right
        if (moveRight)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }

    }

}

