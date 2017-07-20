using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public struct myImages
{
	public FileInfo myFileInfo;
	public Sprite mySprite;
	public myImages(FileInfo f, Sprite s)
	{
		myFileInfo = f;
		mySprite = s;
	}
};

public class PopulatePhotoGrid : MonoBehaviour {
	

	List<myImages> imageList;

	// Use this for initialization
	void Start () {
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void ReadDirectory()
	{
		//Reads the directory from android for pictures taken and puts the file path in the injection
		string path = Application.persistentDataPath;
		Debug.Log (path);
		DirectoryInfo dInfo = new DirectoryInfo (path);
		//Filters for png only
		FileInfo[] fInfo = dInfo.GetFiles ("*.png");
		foreach (FileInfo f in fInfo) {
			Debug.Log ("1");
			Texture2D texture = new Texture2D (2, 2);
			texture.LoadImage (File.ReadAllBytes (Application.persistentDataPath + "/" + f.Name));

			Sprite tempSprite = Sprite.Create (texture, new Rect (0, 0, texture.width, texture.height), new Vector2 ());

			// calls "constructor" to initialize file directory and 
			imageList.Add(WrapObject(f, tempSprite));
		}

	}

	private myImages WrapObject(FileInfo f, Sprite s)
	{
		return new myImages(f, s);
	}

	private void populateChildren()
	{
		int i = 0;
		foreach (Transform child in transform) {
			if (i < imageList.Count) {
				child.GetComponent<initializeChildPhotos> ().injectPhotos (imageList[i]);
				i++;
			}
		}
	}



//	public void Inject(CardContainer inject)
//	{
//		//Loads the image from file directory
//		if (inject.cardPicture != null)
//		{
//			Texture2D texture = new Texture2D(2, 2);
//			texture.LoadImage(File.ReadAllBytes(Application.persistentDataPath + "/" + inject.cardPicture.Name));
//
//			/*
//            if (inject.cardPicture.Name.Contains("IFTPhoto"))
//            {
//                texture.LoadImage(File.ReadAllBytes(Application.persistentDataPath + "/" + inject.cardPicture.Name));
//            }
//            else
//            {
//                texture.LoadImage(File.ReadAllBytes(Application.dataPath + "/Resources/" + inject.cardPicture.Name));
//            }
//            */
//			myImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2());
//		}
//	}
}
