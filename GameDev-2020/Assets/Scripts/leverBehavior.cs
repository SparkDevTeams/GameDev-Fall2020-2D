using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverBehavior : MonoBehaviour
{
    public bool nodeLever;
    // Start is called before the first frame update
    void Start()
    {
        nodeLever=false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other){
        

        if(other.tag=="Player"&&Input.GetButtonDown("Laser")&&nodeLever==true){
            Debug.Log("Lever Activated");
            nodeLever=false;
        }
        else if(other.tag=="Player"&&Input.GetButtonDown("Laser")&&nodeLever==false){
            Debug.Log("Lever Activated");
            nodeLever=true;
        }
    }

    public bool getNodeLever(){
        return nodeLever;
    }

}
