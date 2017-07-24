using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ANyPhotos : MonoBehaviour {
    public Button stuff;

	// Use this for initialization
	void Start () {
        //Reads the directory from android for pictures taken and puts the file path in the injection
        string path = Application.persistentDataPath;
        Debug.Log(path);
        DirectoryInfo dInfo = new DirectoryInfo(path);
        //Filters for png only
        FileInfo[] fInfo = dInfo.GetFiles("*.png");
        if (fInfo.Length == 0)
        {
            stuff.interactable = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
