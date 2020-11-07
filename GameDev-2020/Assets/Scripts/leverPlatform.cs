using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverPlatform : MonoBehaviour
{
    public Transform pos1;
    public Transform pos2;
    public bool nodePos;
    public float speed;

    Vector3 nextPos;

    public leverBehavior currentLever;

    // Start is called before the first frame update
    void Start()
    {
        //currentLever=FindObjectOfType<leverBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate(){
        if(currentLever.getNodeLever()==true){
            nextPos=pos2.position;
        }
        if(currentLever.getNodeLever()==false){
            nextPos=pos1.position;
        }

        transform.position=Vector3.MoveTowards(transform.position, nextPos, speed*Time.deltaTime);
    }
}
