using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EditWord : MonoBehaviour
{
    [SerializeField]
    GameObject inputField;
    [SerializeField]
    GameObject cardDirectory;
    [SerializeField]
    string[] updatedWords;
	// Use this for initialization

	void Awake () {
        inputField = GameObject.Find("WordEditor");
        inputField.SetActive(false);
        cardDirectory = GameObject.Find("CardDirectory");
    }
	
	// Update is called once per frame
    public void Onclick()
    {
        if(!inputField.activeSelf)
        {
            inputField.SetActive(true);
            foreach (string word in cardDirectory.GetComponent<CardDirectory>().devWordList)
            {
                inputField.GetComponent<InputField>().text += word + "\n";
            }
        }
        else
        {
            Debug.Log(inputField.GetComponent<InputField>().text);
            updatedWords = inputField.GetComponent<InputField>().text.Split("\n"[0]);
            cardDirectory.GetComponent<CardDirectory>().devWordList.Clear();
            foreach (string word in updatedWords)
            {
                cardDirectory.GetComponent<CardDirectory>().devWordList.Add(word);
            }
            WriteToTxt(inputField.GetComponent<InputField>().text);
            inputField.SetActive(false);
        }

        
    }
    void WriteToTxt(string newList )
    {
        string fileName = "WordsList" + System.DateTime.Now.ToString("__yyyy-MM-dd_HH-mm-ss") + ".txt";
        //File.WriteAllBytes(Application.persistentDataPath + "/" + photoName, image);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/" + fileName, newList);
    }
	void Update () {
		
	}
}
