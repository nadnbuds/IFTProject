using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PhoneCamera : MonoBehaviour
{

    private int photoCount;
    private bool camAvailable;
    private WebCamTexture currentCam;
    private WebCamTexture frontCam;
    private WebCamTexture backCam;
    public RawImage background;
    public AspectRatioFitter fit;

    void OnDisable()
    {
        if (camAvailable) // && currentCam.isPlaying)
        {
            //Debug.Log("Successfully disabled camera");
            currentCam.Stop();
        }
    }

    // Use this for initialization
    void Start()
    {
        //Debug.Log("Enabled camera");
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length == 0) //No camera available
        {
            camAvailable = false;
            return;
        }
        else
        {
            camAvailable = true;
        }

        foreach (WebCamDevice dev in devices)
        {
            if (dev.isFrontFacing)
            {
                frontCam = new WebCamTexture(dev.name, Screen.width, Screen.height);
            }
            else
            {
                backCam = new WebCamTexture(dev.name, Screen.width, Screen.height);
            }
        }

        if (backCam != null)
        {
            currentCam = backCam;
        }
        else
        {
            currentCam = frontCam;
        }
        currentCam.Play();
        background.texture = currentCam;
    }

    public void StopCam()
    {
        if (camAvailable)
        {
            currentCam.Stop();
        }
    }

    /* Switches between the front and back camera
     */
    public void SwitchCam()
    {
        if (camAvailable && backCam && frontCam)
        {
            currentCam.Stop();

            if (currentCam == backCam)
            {
                currentCam = frontCam;
            }
            else
            {
                currentCam = backCam;
            }
            currentCam.Play();
            background.texture = currentCam;
        }
    }

    //Saves image to path directory under the current data path of the application.
    private void SavePhoto(byte[] image, string path)
    {
        photoCount++;
        string photoName = "Photo_" + photoCount + System.DateTime.Now.ToString("__yyyy-MM-dd_HH-mm-ss") + ".png";
        File.WriteAllBytes(Application.streamingAssetsPath + "/" + photoName, image);
    }

    //Takes photo and converts it to an image of bytes, then saves it to the input directory.
    private IEnumerator TakePhoto(string path)
    {
        yield return new WaitForEndOfFrame();
        Texture2D photo = new Texture2D(currentCam.width, currentCam.height);
        photo.SetPixels(currentCam.GetPixels());
        photo.Apply();
        byte[] image = photo.EncodeToPNG();
        Destroy(photo);
        SavePhoto(image, path);
        //Debug.Log(Application.dataPath + "/Input/" + photoName);
    }

    //function to use in trigger function from button
    public void UploadPhoto()
    {
        StartCoroutine(TakePhoto("Input"));
        //Debug.Log("Uploading Photo");
    }

    // Update is called once per frame
    void Update()
    {
        if (!camAvailable)
        {
            //Add default redirect screen
            return;
        }

        float ratio = (float)currentCam.width / (float)currentCam.height;
        fit.aspectRatio = ratio;

        float scaleY = currentCam.videoVerticallyMirrored ? -1f : 1f;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        int orient = -currentCam.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
    }
}
