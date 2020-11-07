using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformShifting : MonoBehaviour
{
    public Sprite positiveSprite;
    public Sprite negativeSprite;

    private Color color;

    public float currentTimer;
    public float shiftInterval;

    private int currentLayer;
    // Start is called before the first frame update
    void Start()
    {
        currentTimer = 0;
        currentLayer = this.gameObject.layer;

        //color = GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        changeLayer();

    }

    void updateSprite()
    {

        if (this.gameObject.layer == 8)
        {
            //GetComponent<SpriteRenderer>().sprite = positiveSprite;
            //color = new Color(0, 1, 0, color.a);

        }
        else if (this.gameObject.layer == 9)
        {
            //GetComponent<SpriteRenderer>().sprite = negativeSprite;
            //color = new Color(1, 0.92f, 0.016f, color.a);

        }

        GetComponent<Renderer>().material.color = color;
        //Debug.Log("Shift script rgb: " + color.r + "-" + color.g + "-" + color.b);
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

            updateSprite();

            currentTimer = 0;
        }
    }
}
