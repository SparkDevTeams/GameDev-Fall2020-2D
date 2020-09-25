﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamVent : MonoBehaviour, IHitable
{
    private bool beginVenting = true;
    [SerializeField] GameObject ventHitbox = null;
    [SerializeField] float timeToStopVenting = 0;
    [SerializeField] float timeToStartVenting = 0;
    [SerializeField] int damageDealt = 0;
    private void Update()
    {
        if (beginVenting)
            StartCoroutine(StartVent());
    }

    private IEnumerator StartVent()
    {
        beginVenting = false;
        ventHitbox.SetActive(true);
        yield return new WaitForSeconds(timeToStopVenting);
        ventHitbox.SetActive(false);
        yield return new WaitForSeconds(timeToStartVenting);
        beginVenting = true;
    }

    private void OnTriggerEnter2D()
    {
        Hit(damageDealt);
    }
    public void Hit(int damage)
    {
        
    }
}