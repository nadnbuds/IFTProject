﻿using System.Collections;
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
        //Reads the directory from android for pictures taken and puts the file path in the injection
        string path = Application.persistentDataPath;
        Debug.Log(path);
        DirectoryInfo dInfo = new DirectoryInfo(path);
        //Filters for jpg only
        FileInfo[] fInfo = dInfo.GetFiles("*.png");
        foreach(FileInfo f in fInfo)
        {
            Debug.Log("1");
            cardDatabase.Add(WrapObject(f));
        }

        string filePath = Path.Combine(Application.persistentDataPath, "WordsList.txt");

        if (!File.Exists(filePath))
        {
            File.Create(filePath).Dispose();
        }

        string[] linesFromfile = File.ReadAllText(filePath).Split('\n');
        if (linesFromfile.Length == 0) //Word list not initialized
        {
            string[] IFTWords = { "Goes Above and Beyond", "Hardworking", "Productive",
                            "Excited", "Outgoing", "Happy", "Loyal", "Reliable",
                            "Team play", "Industrious", "Enthusiasm", "Good Citizen",
                            "Gregarious", "Thrilled", "Prompt", "Faithful", "Playful",
                            "Conscientious", "Brave", "Creative", "Assertive", "Educated", "Organized",
                            "Efficient" };
            linesFromfile = IFTWords;
        }
        
        foreach (string word in linesFromfile)
        {
            Debug.Log(word);
            devWordList.Add(word);
            cardDatabase.Add(WrapObject(word));
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
