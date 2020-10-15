using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPoint : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 5.0f;
    private bool following = true;

    private void Start()
    {
        following = true;
    }

    void FixedUpdate()
    {
        if (following)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
        }
    }

    public void Follow() 
    {
        following = true;
    }

    public void StopFollowing() 
    {
        following = false;
    }
}
