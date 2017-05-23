using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPooler : Singleton<ObjectPooler> {
    //Parent Object
    private Transform parentPool;
    public Card objToClone;
    //Pool with distractors
    public List<Card> CompletePool;
    //Pool with IFT Only
    public List<Card> IFTPool;
    //Number of IFT Words
    public int numIFTWords;
    //Number of Total words
    int numObjToMake;

    private void Awake()
    {
        parentPool = this.gameObject.transform;
        numObjToMake = Enum.GetNames(typeof(Words)).Length;
        CreateObjects(numObjToMake);
    }

    public void CreateObjects(int x)
    {
        for(int y = 0; y < x; y++)
        {
            Card obj = Instantiate<Card>(objToClone);
            obj.gameObject.SetActive(false);
            obj.transform.parent = parentPool;
            //Sets the word of the card
            obj.myWord = (Words)y;
            CompletePool.Add(obj);
            //Adds to IFT pool if IFT word
            if((int)obj.myWord < numIFTWords)
            {
                IFTPool.Add(obj);
            }
        }
    }

    //Get object from entire pool
    public Card GetAllObject()
    {
        foreach (Card x in CompletePool)
        {
            if (!x.gameObject.activeInHierarchy)
            {
                x.gameObject.SetActive(true);
                return x;
            }
        }
        return null;
    }

    //Get object from IFT pool
    public Card GetIFTObject()
    {
        foreach (Card x in IFTPool)
        {
            if (!x.gameObject.activeInHierarchy)
            {
                x.gameObject.SetActive(true);
                return x;
            }
        }
        return null;
    }

    public void Shuffle()
    {
        System.Random rng = new System.Random();
        int n = CompletePool.Count;
        while(n > 1)
        {
            n--;
            int x = rng.Next(n + 1);
            Card temp = CompletePool[x];
            CompletePool[x] = CompletePool[n];
            CompletePool[n] = temp;
        }
        n = IFTPool.Count;
        while (n > 1)
        {
            n--;
            int x = rng.Next(n + 1);
            Card temp = IFTPool[x];
            IFTPool[x] = IFTPool[n];
            IFTPool[n] = temp;
        }
    }
}
