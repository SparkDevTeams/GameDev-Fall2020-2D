using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalHover : MonoBehaviour
{
    [SerializeField] private float xAmplitude = 1.2f;
    [SerializeField] private float xFrequency = 1.5f;
    [SerializeField] private float yAmplitude = 1.0f;
    [SerializeField] private float yFrequency = 2.0f;
    private Vector3 baseLocalPos;

    void Start()
    {
        baseLocalPos = transform.localPosition;
    }

    void FixedUpdate()
    {       
        float newLocalX = xAmplitude * Mathf.Cos(Time.time * xFrequency) + baseLocalPos.x;
        float newLocalY = yAmplitude * Mathf.Sin(Time.time * yFrequency) + baseLocalPos.y;

        transform.localPosition = new Vector3(newLocalX, newLocalY, baseLocalPos.z);     
    }
}
