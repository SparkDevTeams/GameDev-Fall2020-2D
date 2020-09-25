using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyPlatform : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] float speed = 50f;
 

    // Start is called before the first frame update
    void Start()
    {
      

    }

    void OnCollisionEnter2D(Collision2D collidedWithThis)
    {
        if (collidedWithThis.gameObject.tag == "Player")
        {
           
          
           
            //yuz TEH FORCE, LUKE. Pushes above?
            rigid.AddForce(new Vector3(0, speed, 0), ForceMode2D.Impulse);

            Debug.Log("CollidingWithPlayer");




        }
    }

    public float Speed
    {
        get => speed; set => speed = value;
    }

    // Update is called once per frame
    void Update()
    {



    }
}










