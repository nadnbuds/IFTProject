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
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Dispose();
            string[] IFTWords = { "Goes Above and Beyond", "Hardworking", "Productive",
                            "Excited", "Outgoing", "Happy", "Loyal", "Reliable",
                            "Team play", "Industrious", "Enthusiasm", "Good Citizen",
                            "Gregarious", "Thrilled", "Prompt", "Faithful", "Playful",
                            "Conscientious", "Brave", "Creative", "Assertive", "Educated", "Organized",
                            "Efficient" };
            IFTWordList = IFTWords;
            WriteToTxt();
        }
        else
        {
            IFTWordList = File.ReadAllText(filePath).Split('\n');
        }   
    }

    public void WriteToTxt()
    {
        File.WriteAllText(filePath, String.Join("\r\n", IFTWordList));
    }
}
