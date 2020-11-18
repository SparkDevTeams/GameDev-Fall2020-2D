using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slideOff : MonoBehaviour
{
    [SerializeField] private float slideSpeed;
    Vector2 pos;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player") {

            pos = other.transform.position;

            if ((transform.position.x - other.GetComponent<Transform>().position.x) < 0)
            {
                pos.x = pos.x + slideSpeed;
                other.transform.position = pos;
            }
            else
            {
                pos.x = pos.x - slideSpeed;
                other.transform.position = pos;
            }
        }
    }
}
