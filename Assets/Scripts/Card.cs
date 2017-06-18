using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Card : MonoBehaviour {
    Text myText;
    Image myImage;
    //Inject Constructor
    public Card(CardContainer inject)
    {
        myText.text = inject.cardWord;
        myImage = inject.cardPicture;
    }
    public void Awake()
    {
        myText = this.gameObject.GetComponent<Text>();
        myImage = this.gameObject.GetComponent<Image>();
    }
    public void checkSameCard()
    {
        GameManager.Instance.SelectCard(this);
    }
}
