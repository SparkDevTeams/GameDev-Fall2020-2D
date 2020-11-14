using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LinkSet
{
    [SerializeField]
    public Dialogue_Set linkedSet;
    [SerializeField]
    public string option = "Option";

    public LinkSet() {
        option = "Option";
        linkedSet = null;
    }
    
}
