using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDeath : MonoBehaviour
{
    public GameObject Target_Entity;
    private PlayerStatuses status;
    private Collider2D targetCollider;
    private Collider2D objectCollider;
    public Transform spawn;

    void Start()
    {
        status = getObjectOfType<PlayerStatuses>(Target_Entity);
        targetCollider = getObjectOfType<Collider2D>(Target_Entity);
        objectCollider = getObjectOfType<Collider2D>(gameObject);
    }

    // finds component from target gameobject, returning the first component found with the given type
    private T getObjectOfType<T>(GameObject from)
    {
        if (from.transform.GetComponent<T>() != null)
        {
            return Target_Entity.transform.GetComponent<T>();
        }
        else
        {
            foreach (Transform child in from.transform)
            {
                if (child.GetComponent<T>() != null)
                {
                    return child.GetComponent<T>();
                }
            }
        }
        return default(T);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log($"target collider: {targetCollider.name}, object collider: {objectCollider.name}, status: {status.name}, param {collider.name}");
        if (collider.name == targetCollider.name && collider.tag == targetCollider.tag)
        {
            status.isAlive = false;
            Debug.Log("Dead");
            collider.transform.position = spawn.transform.position;
        }
    }
}
