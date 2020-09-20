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

    private void OnTriggerEnter2D(Collider2D other)
    {
        health.damagePlayer(damage);
    }
}
