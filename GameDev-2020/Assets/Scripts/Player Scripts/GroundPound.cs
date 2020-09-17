using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPound : MonoBehaviour
{
    private PlayerStatuses status;
    private Animator anim;

    [SerializeField] private float dropSpeed;
    private bool pounding;
    [SerializeField] private bool hangtime;

    private float hangTimer;
    private float currentHangtime;
    private float maxHangTime;
    // Start is called before the first frame update
    void Start()
    {
        dropSpeed = -30f;
        maxHangTime = 0.3f;
        currentHangtime = 0f;
        pounding = false;
        hangtime = false;
        status = FindObjectOfType<PlayerStatuses>();
        anim = GetComponent<Animator>();
        anim.SetBool("Pounding", false);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.GetButtonDown("GroundPound"));

        if (!status.getGrounded() && Input.GetButtonDown("GroundPound"))
        {
            pounding = true;
            status.setPounding(true);
            anim.SetBool("Pounding", pounding);
            //Debug.Log("Ground pound input handled");

            if (!hangtime)
            {
                hangtime = true;
            }

        }


    }

    void FixedUpdate()
    {
        if (pounding)
        {

            if (hangtime)
            {
                currentHangtime += Time.deltaTime;

                if (currentHangtime < maxHangTime)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 3.0f);
                }
                else
                {
                    hangtime = false;
                    currentHangtime = 0;
                }
            }

            if (!hangtime)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, dropSpeed);
            }



        }

        if (status.getGrounded() || status.getTechyBounce())
        {
            pounding = false;
            status.setPounding(false);
            anim.SetBool("Pounding", pounding);
        }
    }
}
