using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public Transform currentCheckpoint;
    public Transform player;

    private HealthManager health;
    public bool respawning;
    // Start is called before the first frame update
    void Start()
    {
        respawning = false;
        health = FindObjectOfType<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health.getShieldHealth() == 0)
        {
            respawnPlayer();
            //respawning = true;
        }

        // if (respawning)
        // {
        //     respawnPlayer();
        // }
    }

    void respawnPlayer()
    {
        player.transform.position = currentCheckpoint.transform.position;
        health.resetHealth();
        respawning = false;
    }

    public void setCurrentCheckpoint(Transform check)
    {
        currentCheckpoint = check;
    }


}
