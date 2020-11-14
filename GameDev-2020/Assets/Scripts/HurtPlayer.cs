using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    private HealthManager health;
    [SerializeField] private int damage;
    [SerializeField] private float knockX;
    [SerializeField] private float knockY;
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
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            health.damagePlayer(damage);
            //knockBack(other);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            health.damagePlayer(damage);
            //knockBack(other);
        }

    }



    void knockBack(Collider2D player)
    {
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x + knockX, knockY);
    }
}
