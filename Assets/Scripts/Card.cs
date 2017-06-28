using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class Card : MonoBehaviour {
    Text myText;
    RawImage myImage;
    //Inject Constructor
    public void Awake()
    {
        myText = this.gameObject.GetComponentInChildren<Text>();
        myImage = this.gameObject.GetComponent<RawImage>();
    }
    public void CheckSameCard()
    {
        GameManager.Instance.SelectCard(this);
    }

    public void Inject(CardContainer inject)
    {
        //Loads the image from file directory
        if (inject.cardPicture != null)
        {
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(File.ReadAllBytes("./Assets/Input" + "\\" + inject.cardPicture.Name));
            myImage.texture = texture;
            Debug.Log("Test");
        }
        myText.text = inject.cardWord;
    }

}
