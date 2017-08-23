using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class Card : MonoBehaviour {
    Text myText;
    Image myImage;
    //Inject Constructor
    public void Awake()
    {
        myText = this.gameObject.GetComponentInChildren<Text>();
        myImage = this.gameObject.GetComponent<Image>();
    }

    public void Adjust()
    {
        this.transform.localScale = Vector3.one;
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
            texture.LoadImage(File.ReadAllBytes(Application.persistentDataPath + "/" + inject.cardPicture.Name));
            myImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2());
        }
        myText.text = inject.cardWord;
    }

}