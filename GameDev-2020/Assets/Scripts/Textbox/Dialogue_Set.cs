using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Dialogue_Set : ScriptableObject
{
    [SerializeField]
    private List<Dialogue> dialogues = new List<Dialogue>();

    public List<Dialogue> Dialogues { get { return dialogues; } }
    public void sendDialogue() {
        if (Textbox.T != null)
        {
            Textbox.T.read(dialogues);
            Debug.Log("Dialogue Sent to Textbox");
        }
        else {
            Debug.Log("DialogueSet cannot be sent, no textbox active \nGame may not be running");
        }
    }
}
