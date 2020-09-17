using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MattyPlayerJump : MonoBehaviour
{

    public float jumpHeight;
    public float kickBack;
    public float faceLeft;
    public float faceRight;

    public bool wallJumped;
    public bool hasJumped;

    public int mattyFactor;

    public Transform groundcheck;

    public LayerMask whatIsGround;

    private PlayerStatuses status;
    // Start is called before the first frame update
    void Start()
    {
        status = FindObjectOfType<PlayerStatuses>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if(Input.GetButtonDown("Jump")){
            hasJumped = true;
        }

        if(status.grounded){
            hasJumped = false;
        }
    }

    private void FixedUpdate() {
        
    }
}
