using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleHealthPickup : MonoBehaviour
{
    [SerializeField] private int healAmount;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log(other.tag);
            Debug.Log("heal by: " + healAmount);
            other.GetComponent<HealthManager>().damagePlayer(-healAmount);
            Destroy(gameObject);
        }
    }
}
