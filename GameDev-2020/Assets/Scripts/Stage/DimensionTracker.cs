using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionTracker : MonoBehaviour
{
    private DimensionManager currentDimension;
    //public int dimensionID;
    // Start is called before the first frame update
    void Start()
    {
        currentDimension = FindObjectOfType<DimensionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (dimensionID != currentDimension.getDimensionID())
        // {
        //     gameObject.GetComponent<BoxCollider2D>().enabled = false;
        // }
        // else
        // {
        //     gameObject.GetComponent<BoxCollider2D>().enabled = true;
        // }

        // if (currentDimension.getDimensionID() == 1)
        // {
        //     Physics2D.IgnoreLayerCollision(11, 12, true);
        //     Physics2D.IgnoreLayerCollision(10, 12, false);
        //     //Debug.Log(Physics2D.GetIgnoreLayerCollision(10, 12));

        // }
        // else if (currentDimension.getDimensionID() == 2)
        // {
        //     Physics2D.IgnoreLayerCollision(12, 11, false);
        //     Physics2D.IgnoreLayerCollision(12, 10, true);
        //     //Debug.Log(Physics2D.GetIgnoreLayerCollision(11, 12));
        // }


        // switch (currentDimension.getDimensionID())
        // {
        //     case 1:
        //         Physics2D.IgnoreLayerCollision(11, 12, true);
        //         Physics2D.IgnoreLayerCollision(10, 12, false);
        //         break;

        //     case 2:
        //         Physics2D.IgnoreLayerCollision(12, 11, false);
        //         Physics2D.IgnoreLayerCollision(12, 10, true);
        //         break;
        // }
    }
}
