using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Moving Platform")
        {
            //Debug.Log("Should be child");
            transform.parent = other.transform;
        }
    }



    void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.tag == "Moving Platform")
        {
            transform.parent = null;
        }
    }


}
