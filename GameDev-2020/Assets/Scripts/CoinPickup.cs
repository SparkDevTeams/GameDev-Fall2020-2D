using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private int value = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("CoinGet");

            if (PlayerWallet.Wallet == null) {
                PlayerWallet.Wallet = new PlayerWallet();
            }

            PlayerWallet.Wallet.GetMoney(value);

            Destroy(gameObject.transform.parent.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnTriggerEnter2D(collision.collider);
    }
}
