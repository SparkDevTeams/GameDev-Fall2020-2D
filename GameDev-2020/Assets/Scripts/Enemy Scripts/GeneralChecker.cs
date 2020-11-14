using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralChecker : MonoBehaviour
{
    public bool Grounded { get; private set; } = false;

    public void Awake()
    {
        if (transform.parent == null || GetComponent<Collider2D>() == null) {
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        gameObject.layer = transform.parent.gameObject.layer;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground") {
            Grounded = true;
        }   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            Grounded = false;
        }
    }

}
