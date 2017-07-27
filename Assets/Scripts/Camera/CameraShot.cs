using System.Collections;
using System.IO;
using System;
using UnityEngine;

// Script to attach to camera object w/ active render texture

public class CameraShot : MonoBehaviour
{
    private RenderTexture photoRenderTexture; // Texture that contains camera feed
    private Texture2D photoTexture2D; // Texture to read photoRenderTexture using GetPixels()
    private Rect photoDimensions; // Stores dimension of photo taken

    // Initializes variables
    private void Start()
    {
        photoRenderTexture = GetComponent<Camera>().targetTexture;
        if (photoRenderTexture == null)
        {
            throw new NullReferenceException("No render texture attached to camera");
        }
        photoTexture2D = new Texture2D(photoRenderTexture.width, photoRenderTexture.height);
        photoDimensions = new Rect(0, 0, photoRenderTexture.width, photoRenderTexture.height);
    }

    // Trigger function
    // Captures camera shot and saves it to a .png file
    // located within the 'path' indicated.
    public void CaptureCameraShot(string path)
    {
        StartCoroutine(ProcessPhoto(path));
    }

    // Takes photo and converts it to an image of bytes, then saves it to the indicated path directory.
    private IEnumerator ProcessPhoto(string path)
    {
        // Wait for end of frame to ensure quality picture
        yield return new WaitForEndOfFrame();

        // Sets active texture for reading
        RenderTexture.active = photoRenderTexture;

        // Get the photo in byte[]
        photoTexture2D.ReadPixels(photoDimensions, 0, 0);
        photoTexture2D.Apply();
        byte[] image = photoTexture2D.EncodeToPNG();

        // Deactivate active texture
        RenderTexture.active = null;

        // Save photo
        SavePhoto(image, path);
    }

    // Saves image to path directory under the current data path of the application.
    private void SavePhoto(byte[] image, string path)
    {
        string photoName = "Photo" + DateTime.Now.ToString("__yyyy-MM-dd__HH-mm-ss.fff_tt") + ".png";
        File.WriteAllBytes(Path.Combine(path, photoName), image);
        Debug.Log("Photo saved");
    }
}
