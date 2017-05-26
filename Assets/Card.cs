using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum Words
{
    //IFT Words
    Hardworking,
    Productive,
    Goes_above_and_beyond,
    Excited,
    Outgoing,
    Happy,
    Loyal,
    Reliable,
    Team_Player,
    Industry,
    Enthusiasm,
    Good_Citizen,
    //Distraction Words
    Gregarious,
    Thrilled,
    Prompt,
    Faithful,
    Conscientious,
    Dependable,
    Brave,
    Efficient,
    Organized,
    Educated,
    Assertive,
    Creative

}

public class Card : MonoBehaviour {

    public GameObject myBody;
    public Text myText;
    public Button myButton;
    public Words myWord;

    Card(GameObject body, Text text, Button button, Words word)
    {
        myBody = body;
        myText = text;
        myButton = button;
        myWord = word;
    }
    private Words randomWord()
    {
        return (Words)(UnityEngine.Random.Range(0, Enum.GetNames(typeof(Words)).Length));
    }
    void Start () {
        myBody = this.gameObject;
        myButton = this.gameObject.GetComponent<Button>();
        myText = this.gameObject.GetComponentInChildren<Text>();
        //myWord = randomWord();
        myText.text = myWord.ToString();
	}
	
    public void checkSameCard()
    {
        if (GameManager.Instance.pickEnabled)
        {
            if (myWord == GameManager.Instance.targetCard.myWord)
            {
                GameManager.Instance.CorrectCardPick(true);
            }
            else
            {
                GameManager.Instance.CorrectCardPick(false);
            }
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
