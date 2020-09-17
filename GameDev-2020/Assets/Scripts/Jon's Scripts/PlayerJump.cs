using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerJump : MonoBehaviour
{
    private const int OVERLAP_RESULTS_SIZE = 10;

    [Header("Jump Constants")]
    [SerializeField] private float jumpTime = 0.15f;
    [SerializeField] private float jumpSpeed = 4.5f;
    [SerializeField] private float jumpForgiveness = 0.1f;
    [SerializeField] private float groundedForgiveness = 0.1f;
    [SerializeField] private float fallSpeed = 60.0f;

    private float jumpTimer = 0.0f;
    private float jumpInputTimer = 0.0f;
    private float groundedTimer = 0.0f;

    private Rigidbody2D rb;
    private PlayerState playerState;
    private DimensionManager dimension;
    private Collider2D[] overlapResults = new Collider2D[OVERLAP_RESULTS_SIZE];
    private bool inputEnabled = true;
    private bool jumpButtonHeld = false;
    private bool jumpButtonUp = false;

    public Transform groundCheck;
    public Transform roofCheck;
    public LayerMask absoluteLayer;
    public LayerMask negativeLayer;
    public LayerMask positiveLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerState = GetComponent<PlayerState>();
        dimension = GetComponent<DimensionManager>();
        jumpTimer = 0.0f;
        jumpInputTimer = 0.0f;
        groundedTimer = 0.0f;
        inputEnabled = true;
        jumpButtonHeld = false;
        jumpButtonUp = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && inputEnabled) 
        {
            jumpInputTimer = jumpForgiveness;
        }
        jumpButtonHeld = Input.GetButton("Jump") && inputEnabled;
        jumpButtonUp = Input.GetButtonUp("Jump") && inputEnabled;
    }

    void FixedUpdate()
    {
        if (jumpInputTimer > 0.0f) 
        {
            jumpInputTimer -= Time.deltaTime;
        }

        if (HasTouchedGround())
        {
            playerState.SetGrounded(true);
            groundedTimer = groundedForgiveness;
        }
        else 
        {
            if (playerState.IsGrounded()) 
            {
                groundedTimer -= Time.deltaTime;

                if (groundedTimer <= 0.0f) 
                {
                    playerState.SetGrounded(false);
                    groundedTimer = 0.0f;
                }
            }
        }

        if (jumpInputTimer > 0.0f && playerState.IsGrounded() && !playerState.IsJumping()) 
        {
            jumpInputTimer = 0.0f;
            StartJumping();
            Jump();
        }

        if (jumpButtonHeld && playerState.IsJumping()) 
        {
            if (jumpTimer > 0.0f && !HasTouchedRoof())
            {
                Jump();
                jumpTimer -= Time.deltaTime;

                if (jumpTimer <= 0.0f)
                {
                    StopJumping();
                }
            }
            else 
            {
                StopJumping();
            }
        }

        if (jumpButtonUp) 
        {
            StopJumping();
        }

        if (rb.velocity.y < -fallSpeed) 
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -fallSpeed, Mathf.Infinity));
        }
    }

    public void StopJumping()
    {
        jumpTimer = 0.0f;
        playerState.SetJumping(false);
    }

    public void EnableInput() 
    {
        inputEnabled = true;
    }

    public void DisableInput() 
    {
        inputEnabled = false;
    }

    private void Jump() 
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
    }

    private void StartJumping()
    {
        playerState.SetJumping(true);
        jumpTimer = jumpTime;
    }

    private bool HasTouchedRoof()
    {
        LayerMask currentLayer = GetCurrentLayer();

        int roofHits = Physics2D.OverlapBoxNonAlloc(roofCheck.position, roofCheck.lossyScale
                         , roofCheck.eulerAngles.z, overlapResults, currentLayer);
        return roofHits > 0;
    }

    private bool HasTouchedGround()
    {
        LayerMask currentLayer = GetCurrentLayer();

        int groundHits = Physics2D.OverlapBoxNonAlloc(groundCheck.position, groundCheck.lossyScale
                         , groundCheck.eulerAngles.z, overlapResults, currentLayer);

        return groundHits > 0;

    }

    private LayerMask GetCurrentLayer() 
    {
        int id = dimension.GetDimensionID();

        switch (id) 
        {
            case 1:
                return positiveLayer;
            case 2:
                return negativeLayer;
            default:
                return absoluteLayer;
        }
    }
}
