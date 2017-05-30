using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : Singleton<GameManager> {
    [HideInInspector]
    public bool pickEnabled;
    List<Card> currentPool;
    Transform myParent;
    Transform myHolder;
    System.Random rng;
    int numOfCards;
    public int rounds;
    //Bounds for how many cards per round
    public int lowerBound, upperBound;
    public int numOfChoices;
    public float timeToDisplay;
    public Card targetCard;

    private void Start()
    {
        myParent = Canvas.Instance.cardDisplay.transform;
        myHolder = Canvas.Instance.cardHolder.transform;
        rng = new System.Random();
        currentPool = new List<Card>();
        //How many rounds to go through in the game
        StartRound();
    }

    //The Body of each indivdual round
    private void StartRound()
    {
        ObjectPooler.Instance.Shuffle();
        //Pull IFT cards into the current pool
        numOfCards = rng.Next(lowerBound, upperBound);
        for(int x = 0; x < numOfCards; x++)
        {
            Debug.Log("Pulling");
            currentPool.Add(ObjectPooler.Instance.GetIFTObject());
        }
        //Show the cards
        StartCoroutine(FlashCards());
        //Pull distraction cards
    }
    private void EndRound()
    {
        DisplayObjective(numOfCards);
        for (int x = 0; x < numOfChoices - numOfCards; x++)
        {
            Debug.Log("Adding");
            currentPool.Add(ObjectPooler.Instance.GetAllObject());
        }
        ObjectPooler.Instance.Shuffle(currentPool);
        DisplayCards();
        Debug.Log(currentPool.Count);
    }

    private void DisplayCards()
    {
        foreach (Card x in currentPool)
        {
            x.gameObject.SetActive(true);
            x.transform.parent = myParent;
        }
    }

    private IEnumerator FlashCards()
    {
        foreach(Card x in currentPool)
        {
            x.gameObject.SetActive(true);
            x.transform.parent = myHolder;
            yield return new WaitForSeconds(timeToDisplay);
            x.gameObject.SetActive(false);
        }
        EndRound();
    }

    private void RemoveCards()
    {
        foreach (Card x in currentPool)
        {
            x.gameObject.SetActive(false);
            x.transform.parent = ObjectPooler.Instance.parentPool;
        }
    }

    private void DisplayObjective(int num)
    {
        int target = rng.Next(1, num);
        DisplayText(target);
        target -= 1;
        targetCard = currentPool[target];
        pickEnabled = true;
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
        if (correct)
        {
            Canvas.Instance.headerDisplay.text = "Correct!";
        }
        else
        {
            Canvas.Instance.headerDisplay.text = "Sorry, that was incorrect";
        }
        pickEnabled = false;
        rounds--;
        if(rounds > 0)
        {
            RemoveCards();
            currentPool.Clear();
            StartRound();
        }
    }
}
