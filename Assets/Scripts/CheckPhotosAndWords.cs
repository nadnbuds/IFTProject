using UnityEngine;
using UnityEngine.UI;
using System.IO;

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
                disableMessage.GetComponent<Text>().text = "Not Enough IFT Words";
            }
            else if (!AreTherePhotos() && AreThereWords())
            {
                disableMessage.GetComponent<Text>().text = "Missing A Photo";
            }
            else
            {
                disableMessage.GetComponent<Text>().text = "Not Enough IFT Words/Photos";
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
        string[] IFTWordList = File.ReadAllText(filePath).Split('\n');
        return (IFTWordList.Length >= 6);
    }
}
