using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : Singleton<ScoreManager>
{

    int score;
    int strikes;
    Text scoreDisplay;
    Text strikeDisplay;
	// Use this for initialization
    ScoreManager(int Score, int Strikes)
    {
        score = Score;
        strikes = Strikes;
        scoreDisplay = GameObject.Find("ScoreText").GetComponent<Text>();
        strikeDisplay = GameObject.Find("StrikesText").GetComponent<Text>();
    }
	void Start () {
        scoreDisplay = GameObject.Find("ScoreText").GetComponent<Text>();
        strikeDisplay = GameObject.Find("StrikesText").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
