using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTracker : MonoBehaviour
{
    private CheckpointManager checkpointManager;
    public bool passed;
    // Start is called before the first frame update
    void Start()
    {
        checkpointManager = FindObjectOfType<CheckpointManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !passed)
        {
            checkpointManager.setCurrentCheckpoint(this.transform);
            passed = true;
        }
    }
}
