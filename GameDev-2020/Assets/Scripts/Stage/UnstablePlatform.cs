using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstablePlatform : MonoBehaviour
{
    [SerializeField] float platformDropTime = 0f;
    [SerializeField] float platformRespawnTime = 0f;
    private Vector3 platformOriginalPosition;
    private Rigidbody2D platformRigidbody;
    private bool platformCanBeTriggered = true;

    private void Start()
    {
        platformOriginalPosition = gameObject.transform.position;
        platformRigidbody = gameObject.GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D()
    {
        if (platformCanBeTriggered)
            StartCoroutine(StartDropTime());
    }

    //Waits a set amount of time until the platform changes body type
    //Changes back and resets the position and rotation of the platform to original
    private IEnumerator StartDropTime()
    {
        platformCanBeTriggered = false;
        yield return new WaitForSeconds(platformDropTime);
        platformRigidbody.isKinematic = false;
        yield return new WaitForSeconds(platformRespawnTime);
        platformRigidbody.rotation = 0;
        platformRigidbody.angularVelocity = 0;
        platformRigidbody.velocity = new Vector2 (0, 0);
        transform.position = platformOriginalPosition;
        platformRigidbody.isKinematic = true;
        platformCanBeTriggered = true;
    }
}
