/*
 * Author(s): Joshua Beto
 * Company: MindTAPP
 */

using System.Collections;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

using MindTAPP.Unity.IFT;

namespace MindTAPP.Unity.Gallery
{
    public class PhotoScreens : MonoBehaviour
    {
        [SerializeField] private HorizontalScrollSnap photoDisplay;
        [SerializeField] private GameObject prefabImage;
        [SerializeField] private IPhotoService photoService;
        public Sprite StartingPhoto { get; set; }

        // Holds undo information
        private PhotoMemento lastPhotoDeleted;

        private void Start()
        {
            int startingScreenIndex = 0;
            foreach (Sprite photoSprite in this.photoService.GetPhotos())
            {
                GameObject photo = photoDisplay.InstantiateChild(this.prefabImage);
                photo.GetComponent<Image>().sprite = photoSprite;
                if (photoSprite == this.StartingPhoto)
                {
                    startingScreenIndex = photo.transform.GetSiblingIndex();
                }
            }
            // Sets starting page
            this.photoDisplay.CurrentPage = startingScreenIndex;
            // Prevents bugs
            this.photoDisplay.UpdateLayout();
        }

        public void DeletePhoto()
        {
            if (this.photoDisplay._screensContainer.childCount <= 0)
            {
                return;
            }
            // Moves child screen outside view
            GameObject removedPhoto;
            this.photoDisplay.DeactivateChild(this.photoDisplay.CurrentPage, out removedPhoto);
            // Delete photo's internal storage
            Sprite photo = removedPhoto.GetComponent<Image>().sprite;
            string fileName = photoService.DeletePhoto(photo);
            // Prevents bugs with snap position.
            this.photoDisplay.UpdateLayout();
            // Save memento state
            this.lastPhotoDeleted = new PhotoMemento(removedPhoto, photo, fileName);
        }

        public void UndoDeletion()
        {
            photoService.AddPhoto(lastPhotoDeleted.PhotoSprite, lastPhotoDeleted.FileName);
            photoDisplay.ReactivateChild(lastPhotoDeleted.PhotoGameObject);
            // Prevents bugs
            this.photoDisplay.UpdateLayout();
        }
    }
}