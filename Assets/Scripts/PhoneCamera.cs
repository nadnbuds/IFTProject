using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PhoneCamera : MonoBehaviour {

    private bool camAvailable;
    private WebCamTexture currentCam;
    private WebCamTexture frontCam;
    private WebCamTexture backCam;
    public RawImage background;
    public AspectRatioFitter fit;

    // Use this for initialization
    void Start() {
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

    public IEnumerator TakePhoto ()
    {
        yield return new WaitForEndOfFrame();
        Texture2D photo = new Texture2D(currentCam.width, currentCam.height);
        photo.SetPixels(currentCam.GetPixels());
        photo.Apply();
        byte[] bytes = photo.EncodeToPNG();
        Destroy(photo);
        //File.WriteAllBytes(Application.dataPath + "/" + photo.png, bytes);
    }

    public void UploadPhoto()
    {
        TakePhoto();
    }

    // Update is called once per frame
    void Update () {
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
