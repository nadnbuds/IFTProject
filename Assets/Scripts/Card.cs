using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Card : MonoBehaviour {
    Text myText;
    Image myImage;
    //Inject Constructor
    public void Awake()
    {
        myText = this.gameObject.GetComponentInChildren<Text>();
        myImage = this.gameObject.GetComponent<Image>();
    }
    public void CheckSameCard()
    {
        GameManager.Instance.SelectCard(this);
    }

    public void Inject(CardContainer inject)
    {
        myText.text = inject.cardWord;
        myImage = inject.cardPicture;
    }
}
