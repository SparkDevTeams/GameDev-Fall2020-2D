using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    private HealthManager health;
    [SerializeField] private int damage;
    [SerializeField] private float knockX;
    [SerializeField] private float knockY;
    [SerializeField] private bool omniDimensional = true;
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
            if (!omniDimensional)
            {
                if (other.gameObject.GetComponent<DimensionManager>().getDimensionID() == 1 && transform.parent.gameObject.layer == 9)
                {
                    return;
                }
                if (other.gameObject.GetComponent<DimensionManager>().getDimensionID() == 2 && transform.parent.gameObject.layer == 8)
                {
                    return;
                }
            }

                health.damagePlayer(damage);
            knockBack(other);
        }

    }

    void knockBack(Collider2D player)
    {
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x + knockX, knockY);
    }
}
