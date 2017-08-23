using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class CheckPhotosAndWords : MonoBehaviour {
    public Button myButton;
    public GameObject disableMessage;

    private DirectoryInfo dInfo;
    private string path;

    // Use this for initialization
    void Awake () {
        path = Application.persistentDataPath;
        dInfo = new DirectoryInfo(path);
    }

    private void Start()
    {
        if (AreTherePhotos() && AreThereWords())
        {
            disableMessage.SetActive(false);
            myButton.interactable = true;
        }
        else
        {
            myButton.interactable = false;
            disableMessage.SetActive(true);

            if (!AreThereWords() && AreTherePhotos())
            {
                disableMessage.GetComponent<Text>().text = "* Add More IFT Words!";
            }
            else if (!AreTherePhotos() && AreThereWords())
            {
                disableMessage.GetComponent<Text>().text = "* Take a photo!";
            }
            else
            {
                disableMessage.GetComponent<Text>().text = "* Add More IFT Words/Photos!";
            }
        } 
    }

    private bool AreTherePhotos()
    {
        FileInfo[] fInfo = dInfo.GetFiles("*.png");
        return (fInfo.Length != 0);
    }

    private bool AreThereWords()
    {
        string filePath = Path.Combine(path, "WordsList.txt");
        if (File.Exists(filePath))
        { 
            string[] IFTWordList = File.ReadAllText(filePath).Split('\n');
            return (IFTWordList.Length >= 6);
        }
        else
        {
            File.Create(filePath).Dispose();
            string[] IFTWords = { "Goes Above and Beyond", "Hardworking", "Productive",
                            "Excited", "Outgoing", "Happy", "Loyal", "Reliable",
                            "Team play", "Industrious", "Enthusiasm", "Good Citizen",
                            "Gregarious", "Thrilled", "Prompt", "Faithful", "Playful",
                            "Conscientious", "Brave", "Creative", "Assertive", "Educated", "Organized",
                            "Efficient" };
            File.WriteAllText(filePath, String.Join("\r\n", IFTWords));

            return true;
        }
    }
}
