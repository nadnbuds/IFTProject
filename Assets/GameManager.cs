using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    List<Card> currentPool;
    public int rounds;
    //Bounds for how many cards per round
    public int lowerBound, upperBound;

    private void Start()
    {
        for (int x = 0; x < rounds; x++)
        {
            StartRound();
        }
    }

    private void StartRound()
    {
        int numOfCards = Random.Range(lowerBound, upperBound);
        for(int x = 0; x < numOfCards; x++)
        {
            currentPool.Add(ObjectPooler.Instance.GetIFTObject());
        }
    }
}
