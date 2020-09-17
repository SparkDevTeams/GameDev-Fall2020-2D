using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestDummy : MonoBehaviour, IHitable
{
    private int hp = 2000;
    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
        UpdateText();
    }

    public void Hit(int damage) 
    {
        hp -= damage;
        UpdateText();
        Debug.Log("Took " + damage + " damage HP: " + hp);

        if (hp <= 0) 
        {
            gameObject.SetActive(false);
        }
    }

    private void UpdateText() 
    {
        text.text = hp.ToString();
    }
}
