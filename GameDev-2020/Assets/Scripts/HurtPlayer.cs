using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    private HealthManager health;
    [SerializeField] private int damage;
    // Start is called before the first frame update
    void Start()
    {
        health = FindObjectOfType<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Upon hitting the player, call the damage player function
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            health.damagePlayer(damage);
        }

    }
}
