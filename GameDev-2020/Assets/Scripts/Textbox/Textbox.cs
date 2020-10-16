using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Textbox : MonoBehaviour
{
    public static Textbox T; //The one and only textbox for the scene

    //Sizes the font can be
    //When writing dialogues start with "_s" for smol text or "_l" for big text, anything else creates normal text.
    const int FONT_SIZE_SMALL = 12;
    const int FONT_SIZE_NORMAL = 18;
    const int FONT_SIZE_BIG = 32;
    [SerializeField]
    private Text text;
    [SerializeField]
    private Image profile;
    [SerializeField]
    private float speed = 10.0f; // 10 chars a second

    [SerializeField]
    private List<List<Dialogue>> nextDialogues = new List<List<Dialogue>>();

    private Coroutine coroutine = null;

    public bool Go = false;
    public bool forcedGo = false;

    //Is a textbox active?
    public static bool On {
        get {
            if (T == null) {
                return false;
            }

            return (T.coroutine != null);
        }
    }

    private void Awake()
    {
        if (T == null)
        {
            T = this;
            gameObject.SetActive(false);
        }
        else {
            Destroy(gameObject);
        }


    }

    private void Update()
    {
        if (forcedGo) {
            Debug.Log("Textbox : Forced GO");
            Go = true;
            forcedGo = false;
            return;
        }

        if (Input.GetButtonDown("Continue"))
        {
            Go = true;
        }
        else {
            Go = false;
        }
    }

    public void read(List<Dialogue> dialogues) {
        if (coroutine == null)
        {
            GetComponent<Image>().rectTransform.localScale = new Vector3(1, 0, 1);
            gameObject.SetActive(true);
            coroutine = StartCoroutine(readDialogue(dialogues));
        }
        else {
            nextDialogues.Add(dialogues);
        }
    }

    //Insert Dialogue Here

    //Get Dialogue and begin showing text
    public IEnumerator readDialogue(List<Dialogue> dialogues) {
        for (int d = 0; d < dialogues.Count; d++) {

            if (dialogues[d].Profile != null)
            {
                profile.sprite = dialogues[d].Profile;
                profile.gameObject.SetActive(true);
            }
            else
            {
                profile.gameObject.SetActive(false);
            }
            
            text.text = "";

            //pops the textbox in if it's hidden
            if ( GetComponent<Image>().rectTransform.localScale.y == 0) {

                while (GetComponent<Image>().rectTransform.localScale.y < 1) {
                    GetComponent<Image>().rectTransform.localScale = new Vector3(1, GetComponent<Image>().rectTransform.localScale.y + 0.05f , 1);
                    yield return new WaitForFixedUpdate();
                }

                GetComponent<Image>().rectTransform.localScale = new Vector3(1, 1, 1);
            }

            //Set the text to display
            string line = dialogues[d].Line;

            //Get needed size here
            text.fontSize = FONT_SIZE_NORMAL;

            if (line[0].ToString() == "_") {
                if (line[1].ToString() == "s") {
                    text.fontSize = FONT_SIZE_SMALL;
                    line = line.Remove(0,2);
                }
                else if (line[1].ToString() == "l")
                {
                    text.fontSize = FONT_SIZE_BIG;
                    line = line.Remove(0, 2);
                }
            }

            for (int c = 0; c < line.Length; c++) {
                text.text = text.text + line[c];

                if (Input.GetButton("Continue"))
                {
                    yield return new WaitForSeconds(1 / (speed * 4));
                }
                else {
                    yield return new WaitForSeconds(1 / speed);
                }   

            }//END forloop c

            yield return new WaitForSeconds(0.5f);

            //Press to continue // while continue not down, loop
            yield return new WaitUntil(() => Go);

            yield return new WaitForFixedUpdate();
        }//END forloop d

        if (nextDialogues.Count > 0)
        {
            coroutine = StartCoroutine(readDialogue(nextDialogues[0]));
            nextDialogues.RemoveAt(0);
        }
        else {
            coroutine = StartCoroutine(hideTextbox());
        }

        yield break;
    } //END readDialogue

    //hide textbox
    public IEnumerator hideTextbox() {
        int framesPassed = 0;
        while (GetComponent<Image>().rectTransform.localScale.y > 0) {
            framesPassed++;
            GetComponent<Image>().rectTransform.localScale = new Vector3(1, GetComponent<Image>().rectTransform.localScale.y - 0.05f, 1);
            yield return new WaitForFixedUpdate();
        }
        GetComponent<Image>().rectTransform.localScale = new Vector3(1, 0, 1);

        if (nextDialogues.Count > 0)
        {
            coroutine = StartCoroutine(readDialogue(nextDialogues[0]));
            nextDialogues.RemoveAt(0);
            yield break;
        }

        coroutine = null;
        gameObject.SetActive(false);
        yield break;
    }
}
