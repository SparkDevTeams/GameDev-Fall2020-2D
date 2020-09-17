using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPoundBounce : MonoBehaviour
{
    private PlayerStatuses status;
    private BoxCollider2D box;
    // Start is called before the first frame update
    void Start()
    {
        status = FindObjectOfType<PlayerStatuses>();
        //box = GetComponent<BoxCollider2D>();
        //box.enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (status.getPounding())
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            //box.enabled = true;
        }
        else if(status.getTechyBounce() || status.getGrounded())
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            //box.enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Techy" && status.getPounding())
        {
            Debug.Log("Bouncing on turtle");
            status.setTechyBounce(true);

        }
    }
}
