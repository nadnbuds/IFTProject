using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : Singleton<GameManager> {
    bool pickEnabled;
    List<Card> currentPool;
    Transform myCanvas;
    System.Random rng;
    public int rounds;
    //Bounds for how many cards per round
    public int lowerBound, upperBound;
    public int numOfChoices;
    public float timeToDisplay;
    [NonSerialized]
    public Card targetCard;

    private void Start()
    {
        myCanvas = Canvas.Instance.transform;
        rng = new System.Random();
        currentPool = new List<Card>();
        //How many rounds to go through in the game
        for (int x = 0; x < rounds; x++)
        {
            StartCoroutine(StartRound());
        }
    }

    //The Body of each indivdual round
    private IEnumerator StartRound()
    {
        ObjectPooler.Instance.Shuffle();
        //Pull IFT cards into the current pool
        int numOfCards = rng.Next(lowerBound, upperBound);
        for(int x = 0; x < numOfCards; x++)
        {
            Debug.Log("Pulling");
            currentPool.Add(ObjectPooler.Instance.GetIFTObject());
            currentPool[x].transform.parent = myCanvas;
        }
        //Show the cards
        DisplayCards();
        yield return new WaitForSeconds(timeToDisplay);
        //Functionality for picking and winning/losing the round
        //Pull distraction cards
        for(int x = 0; x < numOfChoices - numOfCards; x++)
        {
            currentPool.Add(ObjectPooler.Instance.GetAllObject());
        }
        DisplayObjective(numOfCards);
        DisplayCards();
    }

    private void DisplayCards()
    {

    }

    private void DisplayObjective(int num)
    {
        int target = rng.Next(1, num);
        //Put pick [number] card on screen
        target -= 1;
        targetCard = currentPool[target];
        pickEnabled = true;
    }
}
