using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualDimension : MonoBehaviour
{
    private DimensionManager dimension;
    private Color color;
    private int dimensionReference;
    // Start is called before the first frame update
    void Start()
    {
        dimension = FindObjectOfType<DimensionManager>();
        color = GetComponent<SpriteRenderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        dimensionReference = checkDimension();

        if (dimensionReference != this.gameObject.layer)
        {
            color = new Color(color.r, color.b, color.g, 0.3f);
        }
        else
        {
            color = new Color(color.r, color.b, color.g, 1f);
        }



        GetComponent<SpriteRenderer>().material.color = color;
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
}
