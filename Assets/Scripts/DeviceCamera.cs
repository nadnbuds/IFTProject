using System.Collections;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DeviceCamera : MonoBehaviour
{   
    private WebCamTexture currentCam; // Current device camera in use
    private WebCamTexture frontCam; // Front camera
    private WebCamTexture backCam; // Back camera

    private Texture2D photoTexture2D;
    private Rect photoDimensions;
    [SerializeField] private RenderTexture photoRenderTexture;
    [SerializeField] private string photoDirectory; // Dir to save photos
    private RawImage cameraFeedImage; // Where the camera feed is displayed
    private RectTransform parentAxes; // Used to reference mirror axis
    private AspectRatioFitter cameraFeedFit; // Current aspect ratio used

    // Stores rotation vector needed to properly orientate video feed
    Vector3 rotation = new Vector3(0f, 0f, 0f);

    // Scaling used for mirroring video feed horizontally & vertically for front camera
    Vector3 currentScale = new Vector3(1f, 1f, 1f);

    // Disables camera when no longer used
    private void OnDisable()
    {
        if (currentCam)
        {
            currentCam.Stop();
        }
    }

    // Initializes backCam, frontCam, and currentCam
    private void Awake()
    {
        cameraFeedImage = GetComponent<RawImage>();
        parentAxes = transform.parent.GetComponent<RectTransform>();
        cameraFeedFit = GetComponent<AspectRatioFitter>();

        WebCamDevice[] devices = WebCamTexture.devices;

        // No camera available
        if (devices.Length == 0) 
        {
            return;
        }

        //Assigns photoTexture2D
        photoTexture2D = new Texture2D(photoRenderTexture.width, photoRenderTexture.height);
        photoDimensions = new Rect(0, 0, photoRenderTexture.width, photoRenderTexture.height);

        // Assigns backCam and frontCam
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

        // Smoothes out image
        if (frontCam != null)
        {
            frontCam.filterMode = FilterMode.Trilinear; 
        }
        if (backCam != null)
        {
            backCam.filterMode = FilterMode.Trilinear;
        }

        // Assigns current camera
        currentCam = backCam != null ? backCam : frontCam;
    }

    // Starts camera for use
    private void Start()
    {
        if (currentCam)
        {
            Debug.Log("Starting camera");
            cameraFeedImage.texture = currentCam;
            currentCam.Play();
        }
    }

    // Switches between the front and back camera
    public void SwitchCam()
    {
        if (backCam && frontCam)
        {
            Debug.Log("Switching camera");
            currentCam.Stop();
            currentCam = currentCam == backCam ? frontCam : backCam;
            currentCam.Play();
            cameraFeedImage.texture = currentCam;
        }
    }

    // Saves image to path directory under the current data path of the application.
    private void SavePhoto(byte[] image)
    {
        string photoName = "Photo" + DateTime.Now.ToString("__yyyy-MM-dd__HH-mm-ss.fff_tt") + ".png";
        File.WriteAllBytes(Application.persistentDataPath + "/" + photoName, image);
        Debug.Log(Application.persistentDataPath);
        Debug.Log("Photo saved");
    }

    // Takes photo and converts it to an image of bytes, then saves it to the indicated path directory.
    private IEnumerator ProcessPhoto()
    {
        yield return new WaitForEndOfFrame();
        RenderTexture.active = photoRenderTexture;

        //Get the photo in byte[]
        photoTexture2D.ReadPixels(photoDimensions, 0, 0);
        photoTexture2D.Apply();
        byte[] image = photoTexture2D.EncodeToPNG();

        RenderTexture.active = null;

        //Save photo
        SavePhoto(image);
    }

    // Trigger function to take and save a photo
    // SavePhotoDirectory is the directory within Application.datapath to save the photo
    public void TakePhoto()
    {
        // DeviceOrientation temp = Input.deviceOrientation;
        if (currentCam)
        {
            StartCoroutine(ProcessPhoto());
        }
    }

    private void rotateCameraFeed()
    {
        rotation.z = -currentCam.videoRotationAngle;
        cameraFeedImage.rectTransform.localEulerAngles = rotation;
    }

    private void mirrorCameraFeed()
    {
        // Unflip if vertically flipped
        currentScale.y = currentCam.videoVerticallyMirrored ? -1f : 1f;

        // Mirror front-facing camera's image horizontally to look more natural
        currentScale.x = currentCam == frontCam ? -1f : 1f;

        //Change to current scale
        parentAxes.localScale = currentScale;
    }

    // Update is called once per frame
    // Used to update switching camera from vertical to horizontal, and vice versa
    // Only visually updates if camera is currently playing
    private void Update()
    {
        // Checks if camera is even available for updating
        if (!currentCam)
        {
            return;
        }

        // Skip making adjustment for incorrect camera data
        if (currentCam.width < 100)
        {
            Debug.Log("Still waiting another frame for correct info...");
            return;
        }

        // Set AspectRatioFitter's ratio
        float videoRatio = (float)currentCam.width / (float)currentCam.height;
        cameraFeedFit.aspectRatio = videoRatio;

        // Rotate image to show correct orientation 
        rotateCameraFeed();

        // Mirror camera horizontally and/or vertically if necessary
        mirrorCameraFeed();
    }
}