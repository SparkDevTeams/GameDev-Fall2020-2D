using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFire : MonoBehaviour
{
    public float velocityX;
    public float velocityY;
    public float timeBetweenShots;
    public float timeUntilDissipate;
    public int amountOfProjectiles;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Projectile>().ShootProjectile(new Vector2(velocityX, velocityY), timeBetweenShots, timeUntilDissipate, amountOfProjectiles);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
