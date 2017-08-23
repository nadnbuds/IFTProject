using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempScore : MonoBehaviour {

    public GameManager gm;
	// Use this for initialization
	void Start () {
        Text myText = GetComponent<Text>();
        myText.text = "Score: " + gm.GetScore();
	}
}
