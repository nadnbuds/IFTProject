using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public struct CardContainer
{
    public string cardWord;
    public FileInfo cardPicture;
    public CardContainer(string word, FileInfo picture)
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
        ReadDirectory();
        //Temporary population tool
        //DevPopulate();
    }

    private void ReadDirectory()
    {
        //Reads the directory "Input" and puts the file path in the injection
        string path = "./Assets/Input";
        DirectoryInfo dInfo = new DirectoryInfo(path);
        //Filters for jpg only
        FileInfo[] fInfo = dInfo.GetFiles("*.jpg");
        foreach(FileInfo f in fInfo)
        {
            cardDatabase.Add(WrapObject(f));
        }
        StreamReader fileReader = new StreamReader(path + "\\words.txt");
        string line;
        using (fileReader)
        {
            do
            {
                line = fileReader.ReadLine();
                if (line != null)
                {
                   cardDatabase.Add(WrapObject(line));
                }
            }
            while (line != null);
            fileReader.Close();
        }
    }

    //Uses the devWordList to populate the cards which is temporary until readDir works
    private void DevPopulate()
    {
        foreach(string x in devWordList)
        {
            Debug.Log("Test" + cardDatabase.Count);
            cardDatabase.Add(WrapObject(x));
        }
        //cardDatabase.Add(WrapObject(test));
    }

    //Two overloads to automatically determine the object type(Image or string) and wrap it in the container and return it
    private CardContainer WrapObject(FileInfo myPicture)
    {
        return new CardContainer(null, myPicture);
    }

    private CardContainer WrapObject(string myWord)
    {
        return new CardContainer(myWord, null);
    }

}
