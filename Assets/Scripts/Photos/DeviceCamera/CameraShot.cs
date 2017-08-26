/*
 * Author(s): Joshua Beto
 * Company: MindTAPP
 */

using System.Collections;
using System.IO;
using System;

using UnityEngine;
using UnityEngine.UI;
using MindTAPP.Unity.IFT;

namespace MindTAPP.Unity.PhotoCapture
{
    // Enum to store file endings
    [System.Serializable]
    public enum ImageFileExtension { Jpg, Png, Raw };
    
    // Attach to camera object w/ active render texture
    public class CameraShot : MonoBehaviour
    {
        [SerializeField] private InitialPhoto startingPhoto;
        [SerializeField] private IPhotoService photoSaver;
        private Camera renderingCamera; // Reference to camera that we will take pixels of
        private RenderTexture photoRenderTexture; // Texture that contains camera feed
        private Texture2D photoTexture2D; // Texture to read photoRenderTexture using GetPixels()
        private Rect photoDimensions; // Stores dimension of photo taken

        // Initializes variables
        private void Awake()
        {
            this.renderingCamera = GetComponent<Camera>();
            this.photoRenderTexture = renderingCamera.targetTexture;

            this.photoTexture2D = new Texture2D(this.photoRenderTexture.width, this.photoRenderTexture.height);
            this.photoDimensions = new Rect(0, 0, this.photoRenderTexture.width, this.photoRenderTexture.height);
        }

        private string CreateUniqueFileName(ImageFileExtension fileType)
        {
            string fileExtension;
            switch (fileType)
            {
                case ImageFileExtension.Jpg:
                    fileExtension = ".jpg";
                    break;
                case ImageFileExtension.Png:
                    fileExtension = ".png";
                    break;
                case ImageFileExtension.Raw:
                    fileExtension = ".raw";
                    break;
                default:
                    fileExtension = string.Empty;
                    break;
            }
            return "Photo " + DateTime.Now.ToString("__yyyy-MM-dd__HH-mm-ss.fff_tt") + fileExtension;
        }

        public string CapureCameraShot(Image thumbnail, ImageFileExtension fileFormat)
        {
            string fileName = CreateUniqueFileName(fileFormat);
            StartCoroutine(ProcessPhoto(thumbnail, fileName, fileFormat));
            return fileName;
        }

        // Takes photo and converts it to an image of bytes, then saves it to the indicated path directory.
        private IEnumerator ProcessPhoto(Image thumbnail, string fileName, ImageFileExtension fileType)
        {
            // Wait for end of frame to ensure quality picture
            yield return new WaitForEndOfFrame();

            // Sets active texture for reading
            this.renderingCamera.targetTexture = this.photoRenderTexture;
            RenderTexture.active = this.photoRenderTexture;

            // Read in pixels
            this.photoTexture2D.ReadPixels(this.photoDimensions, 0, 0);
            this.photoTexture2D.Apply();

            // Fixes render bug/error
            RenderTexture.active = null;

            // Get byte data
            byte[] image = GetByteData(fileType);

            // Save photo to chosen path
            Sprite temp = Sprite.Create(photoTexture2D, photoDimensions, new Vector2(0.5f, 0.5f));
            photoSaver.AddPhoto(temp, fileName);
            thumbnail.sprite = temp;
            startingPhoto.StartingPhoto = temp;

            Debug.Log("Photo Saved");
        }

        private byte[] GetByteData(ImageFileExtension fileType)
        {
            switch (fileType)
            {
                case ImageFileExtension.Jpg:
                    return ImageConversion.EncodeToJPG(this.photoTexture2D);
                case ImageFileExtension.Png:
                    return ImageConversion.EncodeToPNG(this.photoTexture2D);
                case ImageFileExtension.Raw:
                    return this.photoTexture2D.GetRawTextureData();
                default:
                    return this.photoTexture2D.GetRawTextureData();
            }
        }

        /*
        // Captures camera shot and saves it to a specified file format
        // located within the 'path' indicated.
        public string CaptureCameraShot(string path, ImageFileExtension fileFormat)
        {
            string fileName = CreateUniqueFileName(fileFormat);
            StartCoroutine(ProcessPhoto(Path.Combine(path, fileName), fileFormat));
            return fileName;
        }

        // Takes photo and converts it to an image of bytes, then saves it to the indicated path directory.
        private IEnumerator ProcessPhoto(string filePath, ImageFileExtension fileType)
        {
            // Wait for end of frame to ensure quality picture
            yield return new WaitForEndOfFrame();

            // Sets active texture for reading
            this.renderingCamera.targetTexture = this.photoRenderTexture;
            RenderTexture.active = this.photoRenderTexture;

            // Read in pixels
            this.photoTexture2D.ReadPixels(this.photoDimensions, 0, 0);
            this.photoTexture2D.Apply();

            // Fixes render bug/error
            RenderTexture.active = null;

            // Get byte data
            byte[] image = GetByteData(fileType);

            // Save photo to chosen path
            File.WriteAllBytes(filePath, image);
            Debug.Log(filePath);
            Debug.Log("Photo saved");
        }
        */
    }
}
 