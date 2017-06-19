using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : Singleton<GameManager> {
    bool pickEnabled = false, roundPause = false, win = false;
    int upperboundChoice, lowerboundChoice, numberOutOf, numOfImages;
    [SerializeField]
    int strikes, score;
    float timeToDisplay;
    List<Rules> rules;
    Card targetCard;
    List<Card> activePool;
    System.Random rng;

    private void Awake()
    {
        rng = new System.Random();
        activePool = new List<Card>();
        StartCoroutine(Round());
    }
    IEnumerator Round()
    {
        //Display the Round Countdown
        roundPause = true;
        StartCoroutine(CountDown());
        //Get the random set of Cards for the round
        int wordPullNumber = rng.Next(upperboundChoice, lowerboundChoice + 1);
        //Add image bounds here if needed
        int imagePullNumber = numOfImages;
        AddCards(wordPullNumber,imagePullNumber);
        Shuffle(activePool);
        //Pause coroutine till CountDown is done
        while (roundPause) { yield return null; }
        roundPause = true;
        //Begin flashing cards to memorize
        StartCoroutine(FlashCards());
        //Set Target Card
        int target = rng.Next(wordPullNumber);
        targetCard = activePool[target];
        //Pause coroutine till FlashCards is done
        while (roundPause) { yield return null; }
        //Add and display the rest of the cards to select from
        AddCards(numberOutOf, 0);
        Shuffle(activePool);
        DisplayCards();
        DisplayHeader(target + 1);
        pickEnabled = true;
        //Pause coroutine till card is selected
        while (pickEnabled) { yield return null; }
        //Finish the round
        FinishRound();
    }
    IEnumerator FlashCards()
    {
        Transform cardHolder = Canvas.Instance.cardHolder.transform;
        Transform reset = ObjectPooler.Instance.parentPool;
        foreach(Card x in activePool)
        {
            x.transform.parent = cardHolder;
            x.gameObject.SetActive(true);
            yield return new WaitForSeconds(timeToDisplay);
            x.transform.parent = reset;
        }
        roundPause = false;
    }
    //Counts down till the begining of the round
    IEnumerator CountDown()
    {
        Text myHeader = Canvas.Instance.headerDisplay;
        string Prompt = "Round begins in... ";
        myHeader.text = Prompt + "3";
        yield return new WaitForSeconds(1f);
        myHeader.text = Prompt + "2";
        yield return new WaitForSeconds(1f);
        myHeader.text = Prompt + "1";
        yield return new WaitForSeconds(1f);
        myHeader.text = "";
        roundPause = false;
    }
    //Adds cards to the current pool
    void AddCards(int wordsToPull, int imagesToPull)
    {
        ObjectPooler.Instance.Shuffle();
        for(int x = 0; x < imagesToPull; x++)
        {
            activePool.Add(ObjectPooler.Instance.GetImageCard());
        }
        for(int x = activePool.Count; x < wordsToPull + 1; x++)
        {
            activePool.Add(ObjectPooler.Instance.GetWordCard());
        }
    }
    void DisplayHeader(int target)
    {
        Text myHeader = Canvas.Instance.headerDisplay;
        
        switch (target)
        {
            case 1:
                myHeader.text = "Select the 1st card displayed";
                break;
            case 2:
                myHeader.text = "Select the 2nd card displayed";
                break;
            case 3:
                myHeader.text = "Select the 3rd card displayed";
                break;
            default:
                myHeader.text = "Select the " + target + "th card displayed";
                break;
        }
    }
    void DisplayCards()
    {
        Transform display = Canvas.Instance.cardDisplay.transform;
        foreach(Card x in activePool)
        {
            x.transform.parent = display;
            x.gameObject.SetActive(true);
        }
    }
    void RemoveCards()
    {
        Transform display = ObjectPooler.Instance.parentPool;
        foreach (Card x in activePool)
        {
            x.transform.parent = display;
            x.gameObject.SetActive(false);
        }
    }
    void FinishRound()
    {
        Text myHeader = Canvas.Instance.headerDisplay;
        RemoveCards();
        activePool.Clear();
        pickEnabled = false;
        if (!win)
        {
            strikes--;
            myHeader.text = "Sorry that was the wrong Card";
        }
        else
        {
            score++;
            myHeader.text = "Good job, that was correct!";
        }
        if(strikes > 0)
        {
            StartCoroutine(Round());
        }
        else
        {
            //Insert end screen here
        }
    }
    public void SelectCard(Card reference)
    {
        if(reference == targetCard)
        {
            win = true;
        }
        pickEnabled = false;
    }
    public void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int x = rng.Next(n + 1);
            T temp = list[x];
            list[x] = list[n];
            list[n] = temp;
        }
    }
}
