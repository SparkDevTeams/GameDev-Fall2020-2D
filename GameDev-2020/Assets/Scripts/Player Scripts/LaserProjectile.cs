using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    public float laserSpeed;
    private PlayerStatuses status;

    // Start is called before the first frame update
    void Start()
    {
        status = FindObjectOfType<PlayerStatuses>();



        if (status.getShootDown())
        {
            Debug.Log("Inside if statement");
            GetComponent<Rigidbody2D>().velocity = transform.right * -laserSpeed;
        }
        else if (transform.localScale.x > 0)
        {
            GetComponent<Rigidbody2D>().velocity = transform.right * laserSpeed;
        }
        else if (transform.localScale.x < 0)
        {
            GetComponent<Rigidbody2D>().velocity = transform.right * -laserSpeed;
        }

        status.setShootDown(false);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
