using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingHazard: MonoBehaviour
{
    public Transform pos1;
    public Transform pos2;
    public Transform pos3;
    public Transform pos4;

    public Transform startPos;

    Vector3 nextPos;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        //speed = 5f;
        nextPos = startPos.position;
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
            //Debug.Log("1");

        }

        if (transform.position == pos2.position)
        {
            nextPos = pos3.position;
            //Debug.Log("2");
        }

        if(transform.position == pos3.position)
        {
            nextPos = pos4.position;
            //Debug.Log("3");
        }

        if(transform.position == pos4.position){
            nextPos = pos1.position;
            //Debug.Log("4");
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }
}
