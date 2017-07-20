using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class initializeChildPhotos : MonoBehaviour
{
	FileInfo myFileInfo;
	Image myChildImage;

	public initializeChildPhotos ()
	{
		
	}

	public void injectPhotos(myImages tempImageStruct)
	{
		myFileInfo = tempImageStruct.myFileInfo;
		myChildImage.sprite = tempImageStruct.mySprite;
	}

	public void deletThis()
	{
		File.Delete (Application.persistentDataPath + "/" + myFileInfo.Name);
	}
}

