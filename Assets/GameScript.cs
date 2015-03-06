using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class GameScript : MonoBehaviour {

    public Text totalError, outputText, timeText;
    public InputField inputText;
    public float wordTime = 5f;
    public string[] words;

    private int totError = 0, wordIndex = 0;
    private float timeOfWordStart;

	// Use this for initialization
	void Start () 
    {
        timeOfWordStart = Time.time;
        outputText.text = words[wordIndex];
        totalError.text = totError.ToString() + " errors!";
        inputText.text = "";
        EventSystem.current.SetSelectedGameObject(inputText.gameObject);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Time.time - timeOfWordStart > wordTime)
        {
            timeOfWordStart = Time.time;

            //Add the errors to the total
            totError += Utility.LevenshteinDistance(words[wordIndex], inputText.text);
            totalError.text = totError.ToString() + " errors!";

            //Set the current word to be the one the user typed in.
            words[wordIndex] = inputText.text;

            //wrap the words around to start again
            wordIndex = (wordIndex + 1) % words.Length;

            outputText.text = words[wordIndex];
            inputText.text = "";
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            EventSystem.current.SetSelectedGameObject(inputText.gameObject);
            //inputText.OnPointerClick(null);
        }

        timeText.text = (3 - Time.time + timeOfWordStart).ToString("F2") + " s";
	}
}
