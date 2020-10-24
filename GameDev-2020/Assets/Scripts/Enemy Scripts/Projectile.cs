using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Use this script in enemy AI scripts
public class Projectile : MonoBehaviour
{
    [SerializeField] Rigidbody2D projectile = null;

    public void ShootProjectile(Vector2 velocity, float timeBetweenShots, float timeUntilDissipate, int amountOfProjectiles)
    {
        StartCoroutine(StartProjectile(velocity, timeBetweenShots, timeUntilDissipate, amountOfProjectiles));
    }

    private IEnumerator StartProjectile(Vector2 velocity, float timeBetweenShots, float timeUntilDissipate, int amountOfProjectiles)
    {
            //Instantiate projectile, set death timer, and decrease counter
            Rigidbody2D newProjectile;
            newProjectile = Instantiate(projectile, transform.position, transform.rotation);
            newProjectile.velocity = velocity;
            KillProjectile projectileScript = newProjectile.GetComponent<KillProjectile>();
            projectileScript.deathTimer = timeUntilDissipate;
            amountOfProjectiles--;

            if(amountOfProjectiles != 0)
            {
                yield return new WaitForSeconds(timeBetweenShots);
                StartCoroutine(StartProjectile(velocity, timeBetweenShots, timeUntilDissipate, amountOfProjectiles));
            }
    }
}