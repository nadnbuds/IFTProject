using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
