using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : Singleton<GameManager> {
    [SerializeField]
    bool pickEnabled = false, roundPause = false, win = false;
    [SerializeField]
    int timeToPick, numberOutOf, numOfImages;
    int strikes, score, brick;
    [SerializeField]
    float timeToDisplay;
    [SerializeField]
    Card targetCard;
    [SerializeField]
    List<Card> activePool;
    System.Random rng;
    [SerializeField]
    GameObject scoreDisplay;
    [SerializeField]
    GameObject strikeDisplay;
    [SerializeField]
    int round;
    public GameObject continueScreen;
    [SerializeField]
    GameObject continueButton;
    bool continuePressed = false;
    public int GetScore()
    {
        return score;
    }

    private void Awake()
    {
        rng = new System.Random();
        strikes = 3;
        activePool = new List<Card>();
        score = 0;
        brick = 1;
        round = 0;
        UpdateScoreStrikes();
        StartCoroutine(Round());
        continueButton = GameObject.Find("ContinueButton");
        continueScreen.SetActive(false);
        
    }

    private void UpdateScoreStrikes() //Updates the Score/Strikes Number for Gamemanager
    {
        scoreDisplay = GameObject.Find("ScoreText");
        strikeDisplay = GameObject.Find("StrikesText");
        scoreDisplay.GetComponent<Text>().text = "Score: " + score;
        strikeDisplay.GetComponent<Text>().text = "Lives: " + strikes;
    }

    private void DisplayScoreStrikes() //Sets the Score/Strike UI Elements to active and visible
    {
        scoreDisplay.SetActive(true);
        strikeDisplay.SetActive(true);
    }
    private void HideScoreStrikes() //Sets the Score/Strike UI Elements to inactive and hidden
    {
        scoreDisplay.SetActive(false);
        strikeDisplay.SetActive(false);
    }
    IEnumerator Round()
    {
        //Display the Round Countdown
        round++;
        roundPause = true;
        win = false;
        StartCoroutine(CountDown());
        //Get the random set of Cards for the round
        int wordPullNumber = (brick / 5) + 3;
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
        StartCoroutine(PickTimer());
        //Pause coroutine till card is selected
        while (pickEnabled) { yield return null; }
        //Finish the round
        StartCoroutine(FinishRound());
    }
    IEnumerator FlashCards()
    {
        Transform cardHolder = CanvasScript.Instance.cardHolder.transform;
        Transform reset = ObjectPooler.Instance.parentPool;
        foreach(Card x in activePool)
        {
            x.transform.SetParent(cardHolder);
            x.Adjust();
            x.gameObject.SetActive(true);
            yield return new WaitForSeconds(timeToDisplay);
            x.transform.SetParent(reset);
        }
        roundPause = false;
    }
    //Counts down till the begining of the round
    IEnumerator CountDown()
    {
        Text myHeader = CanvasScript.Instance.headerDisplay;
        myHeader.gameObject.SetActive(true);
        string Prompt = "Round begins in ... ";
        myHeader.text = Prompt + "3";
        yield return new WaitForSeconds(1f);
        myHeader.text = Prompt + "2";
        yield return new WaitForSeconds(1f);
        myHeader.text = Prompt + "1";
        yield return new WaitForSeconds(1f);
        myHeader.text = "";
        myHeader.gameObject.SetActive(false);
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
        for(int x = activePool.Count; x < wordsToPull; x++)
        {
            activePool.Add(ObjectPooler.Instance.GetWordCard());
        }
    }
    void DisplayHeader(int target)
    {
        Text myHeader = CanvasScript.Instance.headerDisplay;
        myHeader.gameObject.SetActive(true);
        switch (target)
        {
            case 1:
                myHeader.text = "Select the 1st card displayed in:";
                break;
            case 2:
                myHeader.text = "Select the 2nd card displayed in:";
                break;
            case 3:
                myHeader.text = "Select the 3rd card displayed in:";
                break;
            default:
                myHeader.text = "Select the " + target + "th card displayed in:";
                break;
        }
    }

    IEnumerator PickTimer()
    {
        Text myHeader = CanvasScript.Instance.headerDisplay;
        string baseText = myHeader.text;
        int timer = timeToPick;
        while (timer > 0 && pickEnabled == true)
        {
            myHeader.text = baseText + " " + timer;
            yield return new WaitForSeconds(1);
            timer--;
        }
        pickEnabled = false;

    }

    void DisplayCards()
    {
        List<Card> displayCards = new List<Card>();
        Transform display = CanvasScript.Instance.cardDisplay.transform;
        displayCards.Add(targetCard);
        activePool.Remove(targetCard);
        Shuffle<Card>(activePool);
        for(int x = 0; x < 5; x++)
        {
            displayCards.Add(activePool[x]);
        }
        activePool.Add(targetCard);
        foreach(Card x in displayCards)
        {
            x.transform.SetParent(display);
            x.Adjust();
            x.gameObject.SetActive(true);
        }
    }
    void RemoveCards()
    {
        Transform display = ObjectPooler.Instance.parentPool;
        foreach (Card x in activePool)
        {
            x.transform.SetParent(display);
            x.gameObject.SetActive(false);
        }
    }
    void ButtonPressed()
    {
        continuePressed = true;
        //Debug.Log("C pressed");
    }
    IEnumerator FinishRound()
    {
        Text myHeader = CanvasScript.Instance.headerDisplay;
        RemoveCards();
        activePool.Clear();
        pickEnabled = false;
        brick++;
        //Stops every 5 rounds 
        if (round % 5 == 0)
        {
            continueScreen.SetActive(true);
            myHeader.text = "Congrats on clearing " + round + " rounds!";
            while (continuePressed == false)
            {
                continueButton.GetComponent<Button>().onClick.AddListener(ButtonPressed);
                
                //Debug.Log("wait");
                yield return null;
            }
            continuePressed = false;
            continueScreen.SetActive(false);
        }
        if (!win)
        {
            strikes--;
            UpdateScoreStrikes();
            myHeader.text = "Sorry that was the wrong Card";
            if (strikes > 0)
            {
                yield return new WaitForSeconds(1f);
                StartCoroutine(Round());
            }
            else
            {
                CanvasScript.Instance.headerDisplay.gameObject.SetActive(false);
                HideScoreStrikes();
                foreach(Transform x in CanvasScript.Instance.transform)
                {
                    if(x.tag == "GameOver")
                    {
                        x.gameObject.SetActive(true);
                    }
                }
            }
        }
        else
        {
            score++;
            myHeader.text = "Good job, that was correct!";
            UpdateScoreStrikes();
            
            yield return new WaitForSeconds(1f);
            StartCoroutine(Round());
        }
    }
    public void SelectCard(Card reference)
    {
        win = false;
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
