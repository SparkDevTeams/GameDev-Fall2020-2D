using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float dashSpeed = 6.0f;
    [SerializeField] private float dashTime = 0.15f;
    //[SerializeField] private float dashBufferTime = 0.1f;
    [SerializeField] private int totalAirDashes = 2;
    [SerializeField] private int totalDashes = 2;

    private Rigidbody2D rb;
    private PlayerJump jump;
    private PlayerState playerState;
    private DimensionManager dimension;
    private DashDirection direction = DashDirection.None;
    private float dashTimer = 0.0f;
    //private float dashBuffer = 0.0f;
    private float normalGravity;
    private int airDashes;
    //private int stockedDashes;
    private bool leftButtonPressed = false;
    private bool rightButtonPressed = false;
    private bool dashButtonDown = false;
    private bool inputEnabled = true;

    enum DashDirection { None = 0, Left = 1, Right = 2 }

    void Start()
    {
        dashTimer = 0.0f;
        //dashBuffer = 0.0f;
        airDashes = totalAirDashes;
        //stockedDashes = totalDashes;
        leftButtonPressed = false;
        rightButtonPressed = false;
        dashButtonDown = false;
        inputEnabled = true;
        rb = GetComponent<Rigidbody2D>();
        jump = GetComponent<PlayerJump>();
        playerState = GetComponent<PlayerState>();
        dimension = GetComponent<DimensionManager>();
        normalGravity = rb.gravityScale;
    }

    void Update()
    {
        leftButtonPressed = Input.GetAxisRaw("Horizontal") < 0.0f && inputEnabled;
        rightButtonPressed = Input.GetAxisRaw("Horizontal") > 0.0f && inputEnabled;
        dashButtonDown = Input.GetButtonDown("Dash") && inputEnabled;

        if (dashButtonDown) 
        {
            Debug.Log("Dash down");
        }
    }

    void FixedUpdate()
    {
        if (playerState.IsGrounded() && dashTimer <= 0.0f) 
        {
            ResetAirDash();
        }

        /*if (dashBuffer > 0.0f) 
        {
            dashBuffer -= Time.deltaTime;

            if (dashBuffer <= 0.0f) 
            {
                dashBuffer = 0.0f;
                ResetDash();
            }
        }*/

        if (inputEnabled && ((leftButtonPressed && rightButtonPressed)
            || (!leftButtonPressed && !rightButtonPressed)))
        {
            StayStationary();
        }
        else if (leftButtonPressed)
        {
            MoveLeft();
        }
        else if (rightButtonPressed)
        {
            MoveRight();
        }

        if (dashButtonDown && !playerState.IsDashing()/* && stockedDashes > 0*/
            && (playerState.IsGrounded() || (!playerState.IsGrounded() && airDashes > 0)))
        {
            DisableJump();

            if (!playerState.IsGrounded())
            { 
                airDashes--;
            }

            dashTimer = dashTime;
            //stockedDashes--;
            playerState.SetDashing(true);

            if (!IsFacingLeft())
            {
                direction = DashDirection.Right;
            }
            else
            {
                direction = DashDirection.Left;
            }
        }


        if (playerState.IsDashing()) 
        {
            if (direction == DashDirection.Left)
            {
                DashLeft();
            }
            else if (direction == DashDirection.Right)
            {
                DashRight();
            }

            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0.0f)
            {
                StopDash();
                EnableJump();
                //dashBuffer = dashBufferTime;
            }
            else 
            {
                dimension.executeShift(dashTime);
            }
        }
    }

    public void DisableMovement()
    {
        inputEnabled = false;
        StopDash();
        DisableJump();
        StayStationary();
    }

    public void EnableMovement() 
    {
        inputEnabled = true;
        EnableJump();
    }

    /*public void StopAndResetDash()
    {
        StopDash();
        dashBuffer = 0.0f;
        ResetAirDash();
        ResetDash();
    }*/

    public void ResetGravityDefault() 
    {
        rb.gravityScale = normalGravity;
    }

    private void MoveRight()
    {
        rb.velocity = new Vector2(walkSpeed, rb.velocity.y);
        FaceRight();
    }

    private void MoveLeft()
    {
        rb.velocity = new Vector2(-walkSpeed, rb.velocity.y);
        FaceLeft();
    }

    private void StayStationary() 
    {
        rb.velocity = new Vector2(0.0f, rb.velocity.y);
    }

    private void DashLeft() 
    {
        rb.gravityScale = 0.0f;
        rb.velocity = new Vector2(-dashSpeed, 0.0f);
        FaceLeft();
    }

    private void DashRight()
    {
        rb.gravityScale = 0.0f;
        rb.velocity = new Vector2(dashSpeed, 0.0f);
        FaceRight();
    }

    public void ResetAirDash() 
    {
        airDashes = totalAirDashes;
    }

    /*private void ResetDash()
    {
        stockedDashes = totalDashes;
    }*/

    private void StopDash() 
    {
        playerState.SetDashing(false);
        dashTimer = 0.0f;
        direction = DashDirection.None;
        ResetGravityDefault();
    }

    private void DisableJump() 
    {
        jump.StopJumping();
        jump.DisableInput();
    }

    private void EnableJump() 
    {
        jump.EnableInput();
    }

    private void FaceRight() 
    {
        transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
    }

    private void FaceLeft() 
    {
        transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
    }

    private bool IsFacingLeft() 
    {
        return transform.forward.x < 0.0f;
    }
}
