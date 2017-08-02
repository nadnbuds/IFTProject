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

    private bool ArePhotosMaxed()
    {
        FileInfo[] fInfo = dInfo.GetFiles("*.png");
        return (fInfo.Length == max);
    }

    // Update is called once per frame
    void Update () {
		if (ArePhotosMaxed())
        {
            myButton.interactable = false;
            disableMessage.SetActive(true);
        }
        else
        {
            myButton.interactable = true;
            disableMessage.SetActive(false);
        }
	}
}
