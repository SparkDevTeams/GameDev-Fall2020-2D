using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprinting : MonoBehaviour
{
    public bool isSprinting;

    private PlayerStatuses status;
    // Start is called before the first frame update
    void Start()
    {
        status = FindObjectOfType<PlayerStatuses>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Sprint"))
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }
    }

    void FixedUpdate()
    {
        if (isSprinting)
        {
            status.setSprintSpeed();
        }
        else
        {
            status.setNormalSpeed();
        }
    }
}
