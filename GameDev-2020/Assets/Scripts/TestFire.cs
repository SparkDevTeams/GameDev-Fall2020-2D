using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFire : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Projectile>().ShootProjectile(new Vector2(-15f, 0), 1f, 2f, 5);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
