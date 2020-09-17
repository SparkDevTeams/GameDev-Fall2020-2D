using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectables : MonoBehaviour
{
    //item dimensions
    private int xWidth;
    private int yHeight;

    //item statuses
    private bool hasCollected;
    
    //item generation
    public Collider2D collider;
    private Collider2D itemCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        itemCollider = FindObjectOfType<Collider2D>();
        hasCollected = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D passedCollider) {
        if (passedCollider.name == collider.name && passedCollider.tag == collider.tag) {
            hasCollected = true;
            Debug.Log($"item pickup: {itemCollider.name}");
            Destroy(gameObject);
            Debug.Log("E Z P Z");
        }
    }
}
