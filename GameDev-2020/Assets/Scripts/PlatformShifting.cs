using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformShifting : MonoBehaviour
{
    public Sprite positiveSprite;
    public Sprite negativeSprite;

    public float currentTimer;
    public float shiftInterval;

    private int currentLayer;
    // Start is called before the first frame update
    void Start()
    {
        currentTimer = 0;
        currentLayer = this.gameObject.layer;
    }

    // Update is called once per frame
    void Update()
    {
        changeLayer();
        updateSprite();
    }

    void updateSprite()
    {

        if (this.gameObject.layer == 8)
        {
            GetComponent<SpriteRenderer>().sprite = positiveSprite;

        }
        else if (this.gameObject.layer == 9)
        {
            GetComponent<SpriteRenderer>().sprite = negativeSprite;

        }
    }

    void changeLayer()
    {

        currentTimer += Time.deltaTime;

        if (currentTimer >= shiftInterval)
        {
            currentLayer++;

            if (currentLayer > 9)
            {
                currentLayer = 8;
            }

            this.gameObject.layer = currentLayer;

            currentTimer = 0;
        }
    }
}
