using UnityEngine.UI;
using UnityEngine;
using System.IO;

public class CheckPhotos : MonoBehaviour {
    public Button myButton;
    public int max;
    public GameObject disableMessage;
    private DirectoryInfo dInfo;

    // Use this for initialization
    void Awake () {
        string path = Application.persistentDataPath;
        dInfo = new DirectoryInfo(path);
    }

    public void ArePhotosMaxed()
    {
        FileInfo[] fInfo = dInfo.GetFiles("*.png");
        if (fInfo.Length >= max)
        {
            myButton.interactable = false;
            disableMessage.SetActive(true);
        }
    }
}