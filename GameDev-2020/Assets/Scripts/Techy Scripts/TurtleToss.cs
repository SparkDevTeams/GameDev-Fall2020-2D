using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleToss : MonoBehaviour
{
    public bool tossed;
    public bool tossing;
    public bool canToss;
    public bool stationary;
    public bool reset;

    public Transform parent;

    public float tossVelocity;
    private float tossDistance;
    private float tossCooldown;
    private float tossTimer;

    public float currentTossDistance;

    private Rigidbody2D rb;

    public Transform restPoint;

    private Vector2 restSpot;

    private PlayerStatuses status;
    // Start is called before the first frame update
    void Start()
    {
        tossed = false;
        tossing = false;
        canToss = true;
        stationary = false;
        tossVelocity = 40;
        tossDistance = 7;
        tossTimer = 0;
        tossCooldown = 1;
        rb = GetComponent<Rigidbody2D>();
        status = FindObjectOfType<PlayerStatuses>();
    }

    // Update is called once per frame
    void Update()
    {
        if (reset)
        {
            tossTimer += Time.deltaTime;
            //transform.localScale = new Vector3(status.getLocalScale(), 1, 1);
        }


        if (tossTimer >= tossCooldown)
        {
            canToss = true;
            reset = false;
            tossTimer = 0;
        }

        if (Input.GetButtonDown("TurtleToss") && canToss)
        {
            tossed = true;

        }
        else if (Input.GetButtonDown("TurtleToss") && stationary)
        {
            resetTechy();

            //canToss = true;
        }


        //Debug.Log(status.getLocalScale());
    }

    void FixedUpdate()
    {
        if (tossed)
        {
            Debug.Log(transform.parent);
            tossing = true;
            tossed = false;
            transform.parent = null;
            distancePoints(restPoint.transform.position);

            switch (status.getLocalScale())
            {
                case 1:
                    rb.velocity = new Vector2(tossVelocity, rb.velocity.y);
                    Debug.Log("Local scale is 1");
                    break;
                case -1:
                    rb.velocity = new Vector2(-tossVelocity, rb.velocity.y);
                    Debug.Log("Local scale is -1");
                    break;
                default:
                    break;
            }

            //rb.velocity = new Vector2(tossVelocity, rb.velocity.y);
            canToss = false;
        }

        if (tossing)
        {
            currentTossDistance = Vector2.Distance(restSpot, transform.position);
        }


        if (currentTossDistance > tossDistance && tossing)
        {
            //tossed = false;
            stationary = true;
            tossing = false;
            rb.velocity = new Vector2(0, rb.velocity.y);
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    void distancePoints(Vector2 rest)
    {
        restSpot = rest;
    }

    void resetTechy()
    {

        transform.position = restPoint.transform.position;
        //transform.SetParent(parent);
        transform.localScale = new Vector3(status.getLocalScale(), 1, 1);
        transform.parent = parent.transform;

        GetComponent<BoxCollider2D>().enabled = false;
        stationary = false;
        reset = true;


    }
}
