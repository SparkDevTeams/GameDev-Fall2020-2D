using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceLaser : MonoBehaviour
{
    private float knockbackVertical;
    private float knockbackHorizontal;
    private bool charging;
    private bool firing;

    public bool ready;

    private int fireDirection;

    public Transform firePoint1;
    public Transform firePoint2;
    public Transform firePoint3;
    //public Transform firePoint4;

    public GameObject laser;

    private PlayerStatuses status;

    // Start is called before the first frame update
    void Start()
    {
        charging = false;
        firing = false;
        ready = true;

        status = FindObjectOfType<PlayerStatuses>();

        knockbackVertical = 40f;
        knockbackHorizontal = 40f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Laser") && ready)
        {

            charging = true;
            ready = false;

            if (Input.GetAxisRaw("Horizontal") == -1)
            {
                fireDirection = 4;
            }

            if (Input.GetAxisRaw("Horizontal") == 1)
            {
                fireDirection = 2;
            }

            if (Input.GetAxisRaw("Vertical") == -1)
            {
                status.setShootDown(true);
                fireDirection = 3;
            }

            if (Input.GetAxisRaw("Vertical") == 1)
            {
                fireDirection = 1;
            }
        }
        else
        {
            if (charging)
            {
                charging = false;
                firing = true;

            }
        }


        if (status.getGrounded())
        {
            status.setKnocked(false);
            ready = true;
        }
    }

    void FixedUpdate()
    {

        if (transform.localScale.x > 0)
        {
            firePoint2.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (transform.localScale.x < 0)
        {
            firePoint2.transform.rotation = Quaternion.Euler(0, 0, 180);
            //firePoint3.transform.localScale = new Vector3(1f, -1f, 1f);
            //firePoint3.transform.rotation = Quaternion.Euler(0, 0, 270);

        }

        if (firing)
        {
            status.setKnocked(true);

            if (fireDirection == 1)
            {
                Instantiate(laser, firePoint1.position, firePoint1.rotation);
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, -knockbackVertical);
            }

            if (fireDirection == 2)
            {
                Instantiate(laser, firePoint2.position, firePoint2.rotation);
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                GetComponent<Rigidbody2D>().velocity = new Vector2(-knockbackHorizontal, 0);
            }

            if (fireDirection == 3)
            {

                Instantiate(laser, firePoint3.position, firePoint3.rotation);
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, knockbackVertical);
            }

            if (fireDirection == 4)
            {
                //firePoint2.transform.rotation = Quaternion.Euler(0, 0, 180);

                Instantiate(laser, firePoint2.position, firePoint2.rotation);
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                GetComponent<Rigidbody2D>().velocity = new Vector2(knockbackHorizontal, 0);
            }
        }

        firing = false;
        //status.setKnocked(false);

    }
}
