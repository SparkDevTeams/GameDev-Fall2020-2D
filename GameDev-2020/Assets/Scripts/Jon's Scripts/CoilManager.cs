using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoilManager : MonoBehaviour
{
    private bool busy = false;
    private FollowPoint follow;
    private CoilCharge charge;
    private CoilLaunch launch;

    void Start()
    {
        follow = GetComponent<FollowPoint>();
        charge = GetComponent<CoilCharge>();
        launch = GetComponent<CoilLaunch>();
    }

    void Update()
    {
        if (busy) 
        {
            busy = charge.IsCharging();

            if (!busy) 
            {
                follow.Follow();
            }
        }
    }

    public bool IsBusy() 
    {
        return busy;
    }

    public void Charge(Transform target) 
    {
        if (!busy) 
        {
            follow.StopFollowing();
            charge.StartCharge(target);
        }
    }

    /*public void Recall()
    {
        busy = false;
        follow.Follow();
    }*/
}
