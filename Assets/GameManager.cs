using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : Singleton<GameManager> {
    bool pickEnabled;
    List<Card> currentPool;
    Transform myParent;
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
        myParent = Canvas.Instance.cardDisplay.transform;
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
        }
        //Show the cards
        DisplayCards();
        yield return new WaitForSeconds(timeToDisplay);
        RemoveCards();
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
        foreach (Card x in currentPool)
        {
             x.transform.parent = myParent;
        }
    }

    private void RemoveCards()
    {
        foreach (Card x in currentPool)
        {
            x.transform.parent = null;
        }
    }

    private void DisplayObjective(int num)
    {
        int target = rng.Next(1, num);
        DisplayText(target);
        target -= 1;
        targetCard = currentPool[target];
        pickEnabled = true;
        ObjectPooler.Instance.Shuffle(currentPool);
    }

    private void DisplayText(int target)
    {
        string place;
        switch (target)
        {
            case 1:
                place = "1st";
                break;
            case 2:
                place = "2nd";
                break;
            case 3:
                place = "3rd";
                break;
            default:
                place = target + "th";
                break;
        }
        Canvas.Instance.headerDisplay.text = "Pick the " + place + " card in the row previously displayed";
        Canvas.Instance.headerDisplay.gameObject.SetActive(true);
    }

    public void CorrectCardPick(bool correct)
    {

    }
}
