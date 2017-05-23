using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : Singleton<GameManager> {
    bool pickEnabled;
    List<Card> currentPool;
    Card targetCard;
    System.Random rng;
    public int rounds;
    //Bounds for how many cards per round
    public int lowerBound, upperBound;
    public int numOfChoices;

    private void Start()
    {
        rng = new System.Random();
        //How many rounds to go through in the game
        for (int x = 0; x < rounds; x++)
        {
            StartRound();
        }
    }

    //The Body of each indivdual round
    private void StartRound()
    {
        ObjectPooler.Instance.Shuffle();
        //Pull IFT cards into the current pool
        int numOfCards = rng.Next(lowerBound, upperBound);
        for(int x = 0; x < numOfCards; x++)
        {
            currentPool.Add(ObjectPooler.Instance.GetIFTObject());
        }
        //Show the cards
        DisplayCards();
        //Functionality for picking and winning/losing the round
        DisplayObjective(numOfCards);
        //Pull distraction cards
        for(int x = 0; x < numOfChoices - numOfCards; x++)
        {
            currentPool.Add(ObjectPooler.Instance.GetAllObject());
        }
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
