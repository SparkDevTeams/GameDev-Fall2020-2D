using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MattyPlayerMovement : MonoBehaviour
{
    private float playerVelocity;
    public float testSpeed;
    //public float maxSpeed;
    private float speedMulti = 1.2f;
    private float drag = 0.8f;

    private float dragLimit = 1f;

    private float faceRight = 1;
    private float faceLeft = -1;

    private float mattyFactor;

    private PlayerStatuses status;

    public PhysicsMaterial2D material;


    // Start is called before the first frame update
    void Start()
    {

        status = FindObjectOfType<PlayerStatuses>();
        //testSpeed = GetComponent<Rigidbody2D>().velocity.x;
        testSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        playerVelocity = Input.GetAxisRaw("Horizontal") * speedMulti;



    }

    void FixedUpdate()
    {

        if (playerVelocity > 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x + playerVelocity, GetComponent<Rigidbody2D>().velocity.y);
            transform.localScale = new Vector3(faceRight, 1f, 1f);
            // if (!status.getFacingRight())
            // {
            //     Flip();
            // }

            mattyFactor = 1.0f;
        }

        if (playerVelocity < 0 && status.getFacingRight())
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x + playerVelocity, GetComponent<Rigidbody2D>().velocity.y);
            transform.localScale = new Vector3(faceLeft, 1f, 1f);
            // if (status.getFacingRight())
            // {
            //     Flip();
            // }

            mattyFactor = -1.0f;
        }

        if (playerVelocity == 0 && !status.getHanging() && status.getGrounded() && GetComponent<Rigidbody2D>().velocity.x != 0)
        {
            //changeFriction(0.4f);
            if (GetComponent<Rigidbody2D>().velocity.x < 0)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x + drag, GetComponent<Rigidbody2D>().velocity.y);
            }
            else if (GetComponent<Rigidbody2D>().velocity.x > 0)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x - drag, GetComponent<Rigidbody2D>().velocity.y);
            }

            if (GetComponent<Rigidbody2D>().velocity.x > -dragLimit && GetComponent<Rigidbody2D>().velocity.x < dragLimit)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
            }

        }




        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > status.getMaxSpeed() && !status.getKnocked())
        {
            //Subtracts player velocity when over max speed, keeping the net gain at 0
            //GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x - playerVelocity, GetComponent<Rigidbody2D>().velocity.y);

            if (GetComponent<Rigidbody2D>().velocity.x > 0)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(status.getMaxSpeed(), GetComponent<Rigidbody2D>().velocity.y);
            }
            else if (GetComponent<Rigidbody2D>().velocity.x < 0)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-status.getMaxSpeed(), GetComponent<Rigidbody2D>().velocity.y);
            }

        }

        testSpeed = GetComponent<Rigidbody2D>().velocity.x;
    }

    void Flip()
    {
        status.setFacingRight(!status.getFacingRight());
        transform.Rotate(0f, 180f, 0f);
    }

    void changeFriction(float frictionValue)
    {
        //material.friction = frictionValue;
        //GetComponent<Rigidbody2D>().sharedMaterial.friction = frictionValue;

        //GetComponent<Collider>().material.dynamicFriction = frictionValue;

        //GetComponent<Collider>().material.staticFriction = 0.4f;
        //collider.material.dynamicFriction = 0;
        // collider2D.material.dynamicFriction = 0.4;
        // collider2D.material.staticFriction = 0.4;
    }
}
