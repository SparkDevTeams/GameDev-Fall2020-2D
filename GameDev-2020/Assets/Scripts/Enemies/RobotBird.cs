﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBird : MonoBehaviour
{
    [SerializeField] float endPosX = 0;
    [SerializeField] float duration = 0;

    private float t;
    private float time;
    private float posX;
    private float maxX;
    private float minX;
    private float randomTimeForEggDrop;
    private bool canDropEgg = true;
    private Vector3 birdRotation;

    private void Start()
    {
        maxX = transform.position.x + endPosX;
        minX = transform.position.x;
        birdRotation = transform.rotation.eulerAngles;
        randomTimeForEggDrop = Random.value;
    }
    private void Update()
    {
        //Debug.Log(randomTimeForEggDrop);
        //Debug.Log(t);
        transform.position = new Vector3(posX, transform.position.y, 0);

        time += Time.deltaTime;
        t = time / duration;
        t = t * t * (3f - 2f * t); //Smooth turn

        posX = Mathf.Lerp(minX, maxX, t);

        if (time > duration)
        {
            //Switch min and max x to switch directions
            float tempX = maxX;
            maxX = minX;
            minX = tempX;

            //Rotate object
            birdRotation.y = (birdRotation.y + 180) % 360;
            transform.rotation = Quaternion.Euler(birdRotation);

            randomTimeForEggDrop = Random.value;
            canDropEgg = true;

            time = 0;
        }

        if (randomTimeForEggDrop < t + 0.01 && randomTimeForEggDrop > t - 0.01 && canDropEgg)
        {
            canDropEgg = false;
            Debug.Log(randomTimeForEggDrop);
        }
    }
}
