using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PopulatePhotoGrid : MonoBehaviour
{
    private List<FileInfo> imageFileList;
    private WordAndImageFile dataFile;
    [SerializeField] private GameObject childImageToClone;

    // Use this for initialization
    private void Start()
    {
        dataFile = new WordAndImageFile();
        imageFileList = new List<FileInfo>();
        FileInfo[] imageFiles = dataFile.GetImageFiles();

        foreach (FileInfo fInfo in imageFiles)
        {
            imageFileList.Add(fInfo);
            GameObject childPhoto = Instantiate(childImageToClone);
            childPhoto.transform.SetParent(transform);
            childPhoto.GetComponent<Image>().sprite = dataFile.GetImageFromFile(fInfo);
        }
    }

    public void AddChild(FileInfo childFileInfo)
    {
        imageFileList.Add(childFileInfo);
        GameObject childPhoto = Instantiate(childImageToClone);
        childPhoto.transform.SetParent(transform);
        childPhoto.GetComponent<Image>().sprite = dataFile.GetImageFromFile(childFileInfo);
    }

    public void DeleteChildByIndex(int index)
    {
        imageFileList[index].Delete();
        imageFileList.RemoveAt(index);
        Destroy(transform.GetChild(index).gameObject);
    }
}