using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerWallet
{
    public static PlayerWallet Wallet;

    [SerializeField]
    private int value;
    public int Value { get { return value; } }

    public PlayerWallet(int value) {
        this.value = value;
    }

    public PlayerWallet() {
        value = 0;
    }

    public static void SetWallet(PlayerWallet wallet) {
        Wallet = wallet;
    }

    public void SetWallet() {
        Wallet = this;
    }

    public void GetMoney(int change) {
        value += change;
        if (value > 99999) {
            value = 99999;
        }
    }

    public void SpendMoney(int change) {
        value -= change;
        if (value < 0)
        {
            value = 0;
        }
    }
}
