using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualDimension : MonoBehaviour
{
    private DimensionManager dimension;
    private Color32 color;
    private int dimensionReference;

    private byte red;
    private byte blue;
    private byte green;

    [SerializeField]
    private bool colored;
    // Start is called before the first frame update
    void Start()
    {
        dimension = FindObjectOfType<DimensionManager>();
        color = GetComponent<Renderer>().material.color;

        red = 0;
        blue = 0;
        green = 0;

    }

    // Update is called once per frame
    void Update()
    {
        dimensionReference = checkDimension();

        if (!colored)
        {
            switch (this.gameObject.layer)
            {
                case 8:
                    setColors(99, 155, 255);
                    //setColors(color.r, color.g, color.b);
                    break;
                case 9:
                    setColors(253, 243, 55);
                    //setColors(color.r, color.g, color.b);
                    break;
            }
        }
        else
        {
            switch (this.gameObject.layer)
            {
                case 8:
                    //setColors(255, 0, 0);
                    setColors(color.r, color.g, color.b);
                    break;
                case 9:
                    //setColors(253, 243, 55);
                    setColors(color.r, color.g, color.b);
                    break;
            }
        }


        //Debug.Log("visual script rgb: " + color.r + "-" + color.g + "-" + color.b);

        if (dimensionReference != this.gameObject.layer)
        {
            color = new Color32(red, green, blue, 100);
            //Debug.Log(red + "-" + green + "-" + blue);


        }
        else
        {
            color = new Color32(red, green, blue, 255);
            //Debug.Log(red + "-" + green + "-" + blue);
        }




        GetComponent<Renderer>().material.color = color;
        //Debug.Log("visual script rgb: " + color.r + "-" + color.g + "-" + color.b);
        //GetComponent<SpriteRenderer>().material.color.a = 0.2f;
    }

    private int checkDimension()
    {
        int temp = 0;

        if (dimension.getDimensionID() == 1)
        {
            temp = 8;

        }
        else if (dimension.getDimensionID() == 2)
        {
            temp = 9;

        }

        return temp;

    }

    private void setColors(byte paramRed, byte paramGreen, byte paramBlue)
    {
        red = paramRed;
        blue = paramBlue;
        green = paramGreen;

        //Debug.Log(red + "-" + green + "-" + blue);
    }
}
