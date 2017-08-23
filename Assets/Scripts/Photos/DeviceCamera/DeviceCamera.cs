/*
 * Author(s): Joshua Beto
 * Company: MindTAPP
 * 
 * I would also like to credit: http://answers.unity3d.com/questions/773464/webcamtexture-correct-resolution-and-ratio.html
 * for how to properly display camera feed.
 */

using UnityEngine;
using UnityEngine.UI;

using MindTAPP.Unity.IFT;

namespace MindTAPP.Unity.PhotoCapture
{
    // Projects camera feed onto attached raw image. 
    // DeviceCamera can be used to correctly display camera feed as well as capture photos.
    // Photo capture functionality is delegated to a separate virtual camera with a CameraShot and OrientateCamera script.

    public class DeviceCamera : MonoBehaviour
    {
        [SerializeField] private CameraShot photoCamera; // Virtual camera that takes the photo
        [SerializeField] private Image photoThumbnail;

        private WebCamTexture activeCamera; // Current device camera in use
        private WebCamTexture frontCamera; // Front camera
        private WebCamTexture backCamera; // Back camera

        [SerializeField] private RawImage cameraFeed; // Where the camera feed is displayed
        private Transform parentAxes; // Used to reference correct axes orientation for mirroring due to camera feed rotation
        private AspectRatioFitter aspectRatioFit; // Current aspect ratio used

        // Stores rotation vector needed to properly orientate video feed
        private Vector3 cameraFeedRotation = new Vector3(0f, 0f, 0f);
        // Scaling used for mirroring video feed horizontally & vertically for front camera - NOTE: dependent on parentAxes
        private Vector3 mirrorScale = new Vector3(1f, 1f, 1f);

        // Initializes backCam, frontCam, and currentCam
        private void Awake()
        {
            WebCamDevice[] devices = WebCamTexture.devices;

            // No camera available
            if (devices.Length == 0)
            {
                return;
            }

            this.parentAxes = cameraFeed.transform.parent.transform;
            this.aspectRatioFit = GetComponent<AspectRatioFitter>();
            if (aspectRatioFit == null)
            {
                throw new System.Exception("AspectRatioFitter is not attached to camera feed.");
            }

            // Assigns backCam and frontCam
            foreach (WebCamDevice dev in devices)
            {
                if (dev.isFrontFacing)
                {
                    this.frontCamera = new WebCamTexture(dev.name, Screen.width, Screen.height);
                }
                else
                {
                    this.backCamera = new WebCamTexture(dev.name, Screen.width, Screen.height);
                }
            }

            // Smoothes out image
            if (this.frontCamera != null)
            {
                frontCamera.filterMode = FilterMode.Trilinear;
            }
            if (this.backCamera != null)
            {
                this.backCamera.filterMode = FilterMode.Trilinear;
            }

            // Assigns current camera (backCamera is default)
            this.activeCamera = this.backCamera != null ? this.backCamera : this.frontCamera;
        }

        // Starts camera for use
        private void OnEnable()
        {
            if (this.activeCamera)
            {
                Debug.Log("Starting camera");
                this.cameraFeed.texture = this.activeCamera;
                this.activeCamera.Play();
            }
        }

        // Disables camera when no longer used
        private void OnDisable()
        {
            if (this.activeCamera)
            {
                this.activeCamera.Stop();
            }
        }

        #region TriggerFunctions

        // Switches between the front and back camera if possible
        public void SwitchCamera()
        {
            if (this.backCamera && this.frontCamera)
            {
                Debug.Log("Switching camera");
                this.activeCamera.Stop();
                this.activeCamera = this.activeCamera == this.backCamera ? this.frontCamera : this.backCamera;
                this.activeCamera.Play();
                this.cameraFeed.texture = this.activeCamera;
            }
        }

        // Takes and saves a photo by utilizing a virtual camera in front of the camera feed
        public void CapturePhoto()
        {
            string file = this.photoCamera.CapureCameraShot(ImageFileExtension.Png);
        }

        #endregion TriggerFunctions

        // Update is called once per frame
        // Used to update switching camera from vertical to horizontal, and vice versa
        // Only visually updates if camera is currently playing
        private void Update()
        {
            // Checks if camera is even available for updating
            if (!this.activeCamera)
            {
                return;
            }

            // Skip making adjustment for incorrect camera data
            if (this.activeCamera.width < 100)
            {
                Debug.Log("Still waiting another frame for correct info...");
                return;
            }

            // Set aspectRatioFitFitter's ratio
            float videoRatio = (float)this.activeCamera.width / (float)this.activeCamera.height;
            this.aspectRatioFit.aspectRatio = videoRatio;

            // Rotate image to show correct orientation 
            this.cameraFeedRotation.z = -this.activeCamera.videoRotationAngle;
            this.cameraFeed.rectTransform.localEulerAngles = this.cameraFeedRotation;

            // Unflip if vertically flipped
#if UNITY_IOS
            this.mirrorScale.y = this.activeCamera.videoVerticallyMirrored ? 1f : -1f;
#else
            this.mirrorScale.y = this.activeCamera.videoVerticallyMirrored ? -1f : 1f;
#endif

            // Mirror front-facing camera's image horizontally to look more natural
#if UNITY_IOS
            this.mirrorScale.x = this.activeCamera == this.backCamera ? -1f : 1f;
#else
            this.mirrorScale.x = this.activeCamera == this.frontCamera ? -1f : 1f;
#endif
            //Change to current scale
            this.parentAxes.localScale = mirrorScale;
        }
    }
}