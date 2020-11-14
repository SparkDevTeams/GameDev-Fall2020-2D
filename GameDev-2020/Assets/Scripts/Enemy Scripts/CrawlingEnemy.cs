using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlingEnemy : MonoBehaviour, IHitable
{
    [SerializeField] private int hp = 10;
    [SerializeField] private float speed = 5.5f;
    [SerializeField] private float minDist = 0.2f;
    [SerializeField] Transform[] waypoints;

    private bool frameInvincibility = false;
    private int nextWaypoint = 0;

    void Update() 
    {
        Move();
        float distToPoint = Vector2.Distance(transform.position, waypoints[nextWaypoint].position);

        if (distToPoint < minDist) 
        {
            Turn();
        }
    }

    void LateUpdate()
    {
        if (frameInvincibility) 
        {
            frameInvincibility = false;
        }
    }

    public void Hit(int damage) 
    {
        if (!frameInvincibility)
        {
            hp -= damage;

            if (hp <= 0)
            {
                gameObject.SetActive(false);
            }
            else 
            {
                frameInvincibility = true;
            }
        }
    }

    private void Move() 
    {
        transform.position = Vector2.MoveTowards(transform.position, waypoints[nextWaypoint].position
                        , speed * Time.deltaTime);
    }

    private void UpdateWaypoint() 
    {
        nextWaypoint++;

        if (nextWaypoint >= waypoints.Length) 
        {
            nextWaypoint = 0;
        }
    }

    private void Turn() 
    {
        Vector3 enemyEuler = transform.eulerAngles;
        enemyEuler.z = waypoints[nextWaypoint].eulerAngles.z;
        transform.eulerAngles = enemyEuler;
        UpdateWaypoint();
    }
}
