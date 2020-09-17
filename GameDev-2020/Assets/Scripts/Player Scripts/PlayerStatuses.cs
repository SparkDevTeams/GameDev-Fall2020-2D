using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatuses : MonoBehaviour
{
    //Terrain transforms
    public Transform groundcheck;
    public Transform wallCheck;
    public Transform cornerCheck;
    public Transform ceilingCheck;

    //Layer masks
    public LayerMask whatIsGround;
    public LayerMask whatIsWall;

    //Terrain check booleans
    public bool grounded;
    public bool hangingOffWall;
    public bool onCorner;
    public bool onCeiling;
    public bool knocked;
    public bool shootingDown;
    public bool techyBounce;
    public bool pounding;
    public bool facingRight = true;

    //Movement speed values
    private float currentMaxSpeed;
    private float sprintSpeed = 14f;
    private float normalSpeed = 7f;

    //Terrain check radii
    private float groundCheckRadius = 0.4f;

    private float wallCheckRadius = 0.1f;

    //private float boxSizeX = 0.819356f;
    //private float boxSizeY = 0.1286554f;

    //private float boxSizeX = groundcheck.GetComponent<BoxCollider2D>().size.x;
    //private float boxSizeY = groundcheck.GetComponent<BoxCollider2D>().size.y;

    //Status options
    public bool isAlive = true;



    // Start is called before the first frame update
    void Start()
    {
        currentMaxSpeed = normalSpeed;
        techyBounce = false;
        pounding = false;
    }

    // Update is called once per frame
    void Update()
    {
        //grounded = Physics2D.OverlapCircle(groundcheck.position, groundCheckRadius, whatIsGround);
        grounded = Physics2D.OverlapBox(groundcheck.position, groundcheck.GetComponent<BoxCollider2D>().size, 0, whatIsGround);
        hangingOffWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);
    }

    public bool getGrounded()
    {
        return grounded;
    }

    public bool getHanging()
    {
        return hangingOffWall;
    }

    public bool getOnCeiling()
    {
        return onCeiling;
    }

    public bool getOnCorner()
    {
        return onCorner;
    }

    public float getMaxSpeed()
    {
        return currentMaxSpeed;
    }

    public bool getKnocked()
    {
        return knocked;
    }

    public bool getShootDown()
    {
        return shootingDown;
    }

    public bool getFacingRight()
    {
        return facingRight;
    }

    public void setGrounded(bool ground)
    {
        this.grounded = ground;
    }

    public void setShootDown(bool shootingDown)
    {
        this.shootingDown = shootingDown;
    }

    public void setKnocked(bool knocked)
    {
        this.knocked = knocked;
    }

    public void setSprintSpeed()
    {
        currentMaxSpeed = sprintSpeed;
    }

    public void setNormalSpeed()
    {
        currentMaxSpeed = normalSpeed;
    }

    public float getLocalScale()
    {
        return transform.localScale.x;
    }

    public bool getTechyBounce()
    {
        return techyBounce;
    }

    public void setTechyBounce(bool bounce)
    {
        techyBounce = bounce;
    }

    public bool getPounding()
    {
        return pounding;
    }

    public void setPounding(bool pound)
    {
        pounding = pound;
    }

    public void setFacingRight(bool right)
    {
        this.facingRight = right;
    }

}
