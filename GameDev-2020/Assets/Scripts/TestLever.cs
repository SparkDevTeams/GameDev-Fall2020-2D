using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLever : MonoBehaviour
{
    public bool activate;
    public bool activated;
    // Start is called before the first frame update
    void Start()
    {
        activate = false;
        activated = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetButtonDown("Laser"))
        // {
        //     activate = true;
        //     Debug.Log("Laser");
        //     //activate = false;
        // }
        // else
        // {
        //     activate = false;
        // }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && Input.GetButtonDown("Laser"))
        {
            Debug.Log("Success");


        }



    }


}
