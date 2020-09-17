using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour
{
    private PlayerStatuses status;

    private float kickBack = 10;
    private float hangDelay = 1;

    public bool wallJumped;
    public bool canHang;
    // Start is called before the first frame update
    void Start()
    {
        status = FindObjectOfType<PlayerStatuses>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && status.getHanging())
        {
            wallJumped = true;
        }
    }

    void FixedUpdate()
    {
        if (wallJumped)
        {
            if (transform.localScale.x == -1f)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(kickBack, GetComponent<Rigidbody2D>().velocity.y);
                transform.localScale = new Vector3(1f, 1f, 1f);
                StartCoroutine("stopHanging");
            }
            else if (transform.localScale.x == 1f)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-kickBack, GetComponent<Rigidbody2D>().velocity.y);
                transform.localScale = new Vector3(-1f, 1f, 1f);
                StartCoroutine("stopHanging");
            }
            wallJumped = false;
        }
    }

    public IEnumerator stopHanging()
    {
        canHang = false;
        //hangingOffWall = false;
        yield return new WaitForSeconds(hangDelay);
        canHang = true;

    }

}
