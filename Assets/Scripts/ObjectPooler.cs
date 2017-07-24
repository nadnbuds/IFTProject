using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ObjectPooler : Singleton<ObjectPooler> {
    /*
    public Card cardToClone; //Card to clone
    public List<Card> WordPool; //List of cards with IFT words only
    public List<Card> ImagePool; //List of cards with images
    */
    private void Awake()
    {
        //CreateObjects();
    }
    /*
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
    */
}
