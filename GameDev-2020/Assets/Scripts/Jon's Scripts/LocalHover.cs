using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalHover : MonoBehaviour
{
    private Vector3 baseLocalPos;
    public float xAmplitude = 1.2f;
    public float xFrequency = 1.5f;
    public float yAmplitude = 1.0f;
    public float yFrequency = 2.0f;

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
