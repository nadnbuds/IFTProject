using System.IO;
using UnityEngine;
using UnityEngine.UI;

// Projects camera feed onto attached raw image. 
// DeviceCamera can be used to correctly display camera feed as well as capture photos.
// Photo capture functionality is delegated to a separate 'Camera' object with a CameraShot and OrientateCamera script.
public class DeviceCamera : MonoBehaviour
{
    // Camera that takes the photo. If null, device camera functions the same but throws an error if taking photos
    [SerializeField] private CameraShot photoCamera;
    //[SerializeField] private string photoDirectory;
    
    private WebCamTexture currentCamera; // Current device camera in use
    private WebCamTexture frontCamera; // Front camera
    private WebCamTexture backCamera; // Back camera
    
    private RawImage cameraFeedImage; // Where the camera feed is displayed
    private Transform parentAxes; // Used to reference correct axes orientation due to cameraFeedImage rotation
    private AspectRatioFitter cameraFeedFit; // Current aspect ratio used

    // Stores rotation vector needed to properly orientate video feed
    Vector3 cameraFeedRotation = new Vector3(0f, 0f, 0f); 
    // Scaling used for mirroring video feed horizontally & vertically for front camera - NOTE: dependent on parentAxes
    Vector3 mirrorScale = new Vector3(1f, 1f, 1f); 

    // Disables camera when no longer used
    private void OnDisable()
    {
        if (currentCamera)
        {
            currentCamera.Stop();
        }
    }

    // Initializes backCam, frontCam, and currentCam
    private void Awake()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
  
        // No camera available
        if (devices.Length == 0) 
        {
            return;
        }

        cameraFeedImage = GetComponent<RawImage>();
        parentAxes = transform.parent.GetComponent<RectTransform>();
        cameraFeedFit = GetComponent<AspectRatioFitter>();

        // Assigns backCam and frontCam
        foreach (WebCamDevice dev in devices)
        {
            if (dev.isFrontFacing)
            {
                frontCamera = new WebCamTexture(dev.name, Screen.width, Screen.height);
            }
            else
            {
                backCamera = new WebCamTexture(dev.name, Screen.width, Screen.height);
            }
        }

        // Smoothes out image
        if (frontCamera != null)
        {
            frontCamera.filterMode = FilterMode.Trilinear; 
        }
        if (backCamera != null)
        {
            backCamera.filterMode = FilterMode.Trilinear;
        }

        // Assigns current camera
        currentCamera = backCamera != null ? backCamera : frontCamera;
    }

    // Starts camera for use
    private void Start()
    {
        if (currentCamera)
        {
            Debug.Log("Starting camera");
            cameraFeedImage.texture = currentCamera;
            currentCamera.Play();
        }
    }

    // Switches between the front and back camera if possible
    public void SwitchCamera()
    {
        if (backCamera && frontCamera)
        {
            Debug.Log("Switching camera");
            currentCamera.Stop();
            currentCamera = currentCamera == backCamera ? frontCamera : backCamera;
            currentCamera.Play();
            cameraFeedImage.texture = currentCamera;
        }
    }

    // Trigger function to take and save a photo
    // SavePhotoDirectory is the directory within Application.datapath to save the photo
    public void CapturePhoto()
    {
        photoCamera.CaptureCameraShot(Application.persistentDataPath);
    }

    // Update is called once per frame
    // Used to update switching camera from vertical to horizontal, and vice versa
    // Only visually updates if camera is currently playing
    private void Update()
    {
        // Checks if camera is even available for updating
        if (!currentCamera)
        {
            return;
        }

        // Skip making adjustment for incorrect camera data
        if (currentCamera.width < 100)
        {
            Debug.Log("Still waiting another frame for correct info...");
            return;
        }

        // Set AspectRatioFitter's ratio
        float videoRatio = (float)currentCamera.width / (float)currentCamera.height;
        cameraFeedFit.aspectRatio = videoRatio;

        // Rotate image to show correct orientation 
        cameraFeedRotation.z = -currentCamera.videoRotationAngle;
        cameraFeedImage.rectTransform.localEulerAngles = cameraFeedRotation;

        // Unflip if vertically flipped
        mirrorScale.y = currentCamera.videoVerticallyMirrored ? -1f : 1f;

        // Mirror front-facing camera's image horizontally to look more natural
        mirrorScale.x = currentCamera == frontCamera ? -1f : 1f;

        //Change to current scale
        parentAxes.localScale = mirrorScale;
    }
}