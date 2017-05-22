using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : Singleton<ObjectPooler> {

    private Transform parentPool;
    public Card objToClone;
    public List<Card> CompletePool;
    public List<Card> IFTPool;
    public int numIFTWords;
    public int numObjToMake;

    private void Awake()
    {
        parentPool = this.gameObject.transform;
        CreateObjects(numObjToMake);
    }

    public void CreateObjects(int x)
    {
        for(int y = 0; y < x; y++)
        {
            Card obj = Instantiate<Card>(objToClone);
            obj.gameObject.SetActive(false);
            obj.transform.parent = parentPool;
            obj.myWord = (Words)y;
            CompletePool.Add(obj);
            if((int)obj.myWord < numIFTWords)
            {
                IFTPool.Add(obj);
            }
        }
    }

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

    }
}
