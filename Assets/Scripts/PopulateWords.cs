using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateWords : MonoBehaviour {
    private EditWord wordEditor;
    private string[] defaultIFTWords;
    [SerializeField] private GameObject childToClone;

    private void Awake()
    {
        wordEditor = new EditWord();
        string[] IFTWords = { "Goes Above and Beyond", "Hardworking", "Productive", 
                            "Excited", "Outgoing", "Happy", "Loyal", "Reliable",
                            "Team play", "Industrious", "Enthusiasm", "Good Citizen",
                            "Gregarious", "Thrilled", "Prompt", "Faithful", "Playful",
                            "Conscientious", "Brave", "Creative", "Assertive", "Educated", "Organized",
                            "Efficient" };
        defaultIFTWords = IFTWords;
    }

	// Use this for initialization
	void Start () {
        foreach (string word in wordEditor.IFTWordList)
        {
            if (!word.Equals(string.Empty))
            {
                Debug.Log("Word: " + word);
                GameObject child = Instantiate(childToClone);
                child.transform.SetParent(transform);
                child.transform.localScale = Vector3.one;
                Transform childWord = child.transform.Find("IFTWordInputField");
                childWord.GetComponent<InputField>().text = word;
            }
        }
    }
	public void ResetWords()
    {
        int childIndex = 0;
        int childrenAlreadyCreated = transform.childCount;
        foreach (string word in defaultIFTWords)
        {
            if (childIndex < childrenAlreadyCreated)
            {
                Transform child = transform.GetChild(childIndex);
                child.Find("IFTWordInputField").GetComponent<InputField>().text = word;
                childIndex++;
                Debug.Log("Within card: " + word);
            }
            else
            {
                GameObject child = Instantiate(childToClone);
                child.transform.SetParent(transform);
                child.transform.localScale = Vector3.one;
                Transform childWord = child.transform.Find("IFTWordInputField");
                childWord.GetComponent<InputField>().text = word;
                Debug.Log("Outside card: " + word);
            }
        }
        for (int i = defaultIFTWords.Length; transform.childCount > i; i++)
        {
            Transform child = transform.GetChild(i);
            Destroy(child.gameObject);
        }
    }

    public void AddWord()
    {
        GameObject child = Instantiate(childToClone);
        child.transform.SetParent(transform);
        child.transform.localScale = Vector3.one;
    }

    public void DeleteWord(GameObject wordObject)
    {
        Destroy(wordObject);
    }

	public void UpdateWordList()
    {
        List<string> IFTWords = new List<string>();

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            Transform childWord = child.transform.Find("IFTWordInputField");
            string childWordText = childWord.GetComponent<InputField>().text;
            if (!childWordText.Equals(string.Empty))
            {
                IFTWords.Add(childWordText);
                Debug.Log(childWordText);
            }
        }

        wordEditor.IFTWordList = IFTWords.ToArray();
        wordEditor.WriteToTxt();
    }
}
