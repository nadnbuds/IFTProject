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

    [HideInInspector]
    public LinkedList<CardContainer> cardDatabase;

    public List<string> devWordList;

    private void Awake()
    {
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
            cardDatabase.AddLast(WrapObject(x));
        }
    }

    //Two overloads to automatically determine the object type(Image or string) and wrap it in the container and return it
    private CardContainer WrapObject(Image myPicture)
    {
        CardContainer temp = new CardContainer(null, myPicture);
        return temp;
    }

    private CardContainer WrapObject(string myWord)
    {
        CardContainer temp = new CardContainer(myWord, null);
        return temp;
    }

}
