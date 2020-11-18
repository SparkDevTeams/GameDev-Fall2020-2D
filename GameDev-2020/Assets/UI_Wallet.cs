using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Wallet : MonoBehaviour
{
    [SerializeField]
    private Text text;
    private int currentMoney = 0;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();

        if (PlayerWallet.Wallet == null) {
            PlayerWallet.Wallet = new PlayerWallet();
        }

        currentMoney = PlayerWallet.Wallet.Value;
        text.text = currentMoney.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerWallet.Wallet != null) {
            if (currentMoney < PlayerWallet.Wallet.Value)
            {
                currentMoney++;
            }
            else if (currentMoney < PlayerWallet.Wallet.Value) {
                currentMoney--;
            }

            text.text = currentMoney.ToString();
        }
    }
}
