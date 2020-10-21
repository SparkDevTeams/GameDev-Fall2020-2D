using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//maybe add a stun animation? for some flare idk

public class ElectricField : MonoBehaviour
{
    private HealthManager health;
    [SerializeField] private int damage;

    void Start(){

        health = FindObjectOfType<HealthManager>();

    }
    void OnTriggerEnter2D (Collider2D other) {

        //if player touches the electric field
        if(other.CompareTag("Player")){

            health.damagePlayer(damage);

        }
        
    }
}
