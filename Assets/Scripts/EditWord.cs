using System.IO;
using System;
using UnityEngine;

public class EditWord
{
    private string filePath;
    public string[] IFTWordList { set; get; }

	// Use this for initialization
	public EditWord()
    {
        filePath = Path.Combine(Application.persistentDataPath, "WordsList.txt");
        IFTWordList = File.ReadAllText(filePath).Split('\n');
    }

    public void WriteToTxt()
    {
        File.WriteAllText(filePath, String.Join("\r\n", IFTWordList));
    }
}
