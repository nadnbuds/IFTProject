using UnityEngine;
using System.IO;

public class WordAndImageFile
{
    DirectoryInfo mainDataDirectory;

    public WordAndImageFile()
    {
        mainDataDirectory = new DirectoryInfo(Application.persistentDataPath);
    }

    public FileInfo[] GetImageFiles()
    {
        return mainDataDirectory.GetFiles("*.png"); // Filters for png only
    }

    public FileInfo[] GetWordFiles()
    {
        return mainDataDirectory.GetFiles("*.txt");
    }

    public Sprite GetImageFromFile(FileInfo imageFile)
    {
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(File.ReadAllBytes(Path.Combine(Application.persistentDataPath, imageFile.Name)));
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2());
    }
}
