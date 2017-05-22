﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    bool pickEnabled;
    List<Card> currentPool;
    Card targetCard;
    public int rounds;
    //Bounds for how many cards per round
    public int lowerBound, upperBound;
    public int numOfChoices;

    private void Start()
    {
        //How many rounds to go through in the game
        for (int x = 0; x < rounds; x++)
        {
            StartRound();
        }
    }

    //The Body of each indivdual round
    private void StartRound()
    {
        //Pull IFT cards into the current pool
        int numOfCards = Random.Range(lowerBound, upperBound);
        for(int x = 0; x < numOfCards; x++)
        {
            currentPool.Add(ObjectPooler.Instance.GetIFTObject());
        }
        DisplayCards();
        DisplayObjective(numOfCards);
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
        int target = Random.Range(1, num);
        target -= 1;
        targetCard = currentPool[target];
    }
}
