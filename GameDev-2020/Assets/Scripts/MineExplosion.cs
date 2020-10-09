using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineExplosion : MonoBehaviour
{
    public GameObject explosion;
    public bool timerStarted = false;
    private float time = 0f;
    [SerializeField] float timeAmount = 2;


    // Start is called before the first frame upd

     private void OnTriggerEnter2D(Collider2D other){
         if(other.tag == "Player"){

         
                if(!timerStarted){
                    timerStarted = true;
                }
         }

        // if(other.tag == "Player"){ //noramlly tags for players is Player
        //     GameObject e =  Instantiate(explosion) as GameObject;
        //     e.transform.position = transform.position;
        //     Destroy(explosion.gameObject);
        //     this.gameObject.SetActive(false);
        //     //   other.GetComponent<Rigidbody2D>().AddForce(0.0f, -2.0f);
        // }
    }

        // private void explosionTrig(Collider2D other){
        //      if(other.tag == "Player"){ //noramlly tags for players is Player
        //     GameObject e =  Instantiate(explosion) as GameObject;
        //     e.transform.position = transform.position;
        //     Destroy(explosion.gameObject);
        //     this.gameObject.SetActive(false);
        //      }
        // }

        void Update(){
            if (timerStarted)
            {
            time += Time.deltaTime;
            
                if (time >= timeAmount)
                {
                GameObject e =  Instantiate(explosion) as GameObject;
                e.transform.position = transform.position;
                Destroy(explosion.gameObject);
                this.gameObject.SetActive(false);
                }
            }
        }

}
