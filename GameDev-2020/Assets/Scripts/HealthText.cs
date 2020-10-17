using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthText : MonoBehaviour
{
    private HealthManager health;
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        health = FindObjectOfType<HealthManager>();
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = health.getShieldHealth().ToString();
    }
}
