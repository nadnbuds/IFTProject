using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct CardContainer
{
    public string cardWord;
    public Image cardPicture;
    public CardContainer(string word, Image picture)
    {
        cardWord = word;
        cardPicture = picture;
    }
}

public class CardDirectory : Singleton<CardDirectory> {
    
    public List<CardContainer> cardDatabase;

    public List<string> devWordList;

    private void Awake()
    {
        cardDatabase = new List<CardContainer>();
        //ReadDirectory()
        //Temporary population tool
        DevPopulate();
    }

    private void ReadDirectory()
    {

    }

    //Uses the devWordList to populate the cards which is temporary until readDir works
    private void DevPopulate()
    {
        foreach(string x in devWordList)
        {
            Debug.Log("Test" + cardDatabase.Count);
            cardDatabase.Add(WrapObject(x));
        }
    }

    //Two overloads to automatically determine the object type(Image or string) and wrap it in the container and return it
    private CardContainer WrapObject(Image myPicture)
    {
        return new CardContainer(null, myPicture);
    }

    private CardContainer WrapObject(string myWord)
    {
        return new CardContainer(myWord, null);
    }

}
