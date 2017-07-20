using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class initializeChildPhotos : MonoBehaviour {
    Sprite Default;
	FileInfo myFileInfo;
	Image myChildImage;

    private void Awake()
    {
        myChildImage = this.gameObject.GetComponent<Image>();
    }

	public void injectPhotos(FileInfo f)
	{
        if (myChildImage != null)
        {
            Debug.Log(f.Name);
            myFileInfo = f;
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(File.ReadAllBytes(Application.persistentDataPath + "/" + f.Name));
            Default = myChildImage.sprite;
            myChildImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2());
        }
    }

	public void deleteThis()
	{
        myChildImage.sprite = Default;
		File.Delete (Application.persistentDataPath + "/" + myFileInfo.Name);
	}
}

