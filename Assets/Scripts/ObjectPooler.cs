using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ObjectPooler : Singleton<ObjectPooler> {
    //Parent Object
    public Transform parentPool;
    //Object base to clone
    public Card myObj;
    //Pool with IFT Only
    public List<Card> WordPool;
    //list of cards with images
    public List<Card> ImagePool;

    private void Awake()
    {
        parentPool = this.gameObject.transform;
        CreateObjects();
    }

    public void CreateObjects()
    {
        foreach (CardContainer x in CardDirectory.Instance.cardDatabase)
        {
            //Injects the container to the card and initializes it
            Card temp = GameObject.Instantiate<Card>(myObj) as Card;
            temp.Inject(x);
            temp.transform.parent = parentPool;
            temp.gameObject.SetActive(false);
            //Check if the card is an imagecard or word card using x
            if (x.cardWord == null)
            {
                ImagePool.Add(temp);
            }
            else
            {
                WordPool.Add(temp);
            }
        }
    }
    //Get card from WordPool
    public Card GetWordCard()
    {
        foreach (Card x in WordPool)
        {
            if (!x.gameObject.activeInHierarchy)
            {
                x.gameObject.SetActive(true);
                return x;
            }
        }
        return null;
    }
    //Get card from ImagePool
    public Card GetImageCard()
    {
        foreach (Card x in ImagePool)
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
        GameManager.Instance.Shuffle(WordPool);
        GameManager.Instance.Shuffle(ImagePool);
    }
}
