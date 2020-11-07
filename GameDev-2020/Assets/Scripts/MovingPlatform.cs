using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pos1;
    public Transform pos2;
    public Transform startPos;

    public bool onDemand;
    public bool activated;

    Vector3 nextPos;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        //speed = 5f;
        nextPos = startPos.position;
        activated = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {

        if (transform.position == pos1.position)
        {
            nextPos = pos2.position;

        }

        if (transform.position == pos2.position)
        {
            nextPos = pos1.position;
        }

        if (!onDemand)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Player")
        {
            onDemand = false;
        }
    }
}
