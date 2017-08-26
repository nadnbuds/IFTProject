/*
 * Author(s): Joshua Beto
 * Company: MindTAPP
 */

using System.Collections;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using UnityEngine.Events;

using MindTAPP.Unity.IFT;

namespace MindTAPP.Unity.Gallery
{
    public class PhotoScreens : MonoBehaviour
    {
        [SerializeField] private HorizontalScrollSnap photoDisplay;
        [SerializeField] private GameObject prefabImage;
        [SerializeField] private IPhotoService photoService;
        [SerializeField] private InitialPhoto bam;
        public UnityEvent OnDelete;
        public UnityEvent OnUndo;

        // Holds undo information
        private PhotoMemento lastPhotoDeleted;
        private int indexDeleted;

        private void Start()
        {
            GameObject startingPage = null;
            foreach (Sprite photoSprite in this.photoService.GetPhotos())
            {
                GameObject photo = photoDisplay.InstantiateChild(this.prefabImage);
                photo.GetComponent<Image>().sprite = photoSprite;

                if (bam.StartingPhoto == photoSprite)
                {
                    Debug.Log("Match found.");
                    startingPage = photo;
                }
            }
            if (startingPage)
            {
                photoDisplay.CurrentPage = startingPage.transform.GetSiblingIndex();
                photoDisplay.UpdateLayout();
            }
        }

        public void DeletePhoto()
        {
            if (this.photoDisplay._screensContainer.childCount <= 0)
            {
                return;
            }
            // Moves child screen outside view
            GameObject removedPhoto;
            indexDeleted = photoDisplay.CurrentPage;

            this.photoDisplay.RemoveChild(this.photoDisplay.CurrentPage, out removedPhoto);
            // Delete photo's internal storage
            Sprite photo = removedPhoto.GetComponent<Image>().sprite;
            string fileName = photoService.DeletePhoto(photo);
            // Prevents bugs with snap position.
            this.photoDisplay.UpdateLayout();
            // Save memento state
            this.lastPhotoDeleted = new PhotoMemento(removedPhoto, photo, fileName);

            Debug.Log("Here");
            OnDelete.Invoke();
            Debug.Log("Done");
        }

        public void UndoDeletion()
        {
            photoService.AddPhoto(lastPhotoDeleted.PhotoSprite, lastPhotoDeleted.FileName);
            photoDisplay.AddChild(lastPhotoDeleted.PhotoGameObject, indexDeleted);
            photoDisplay.CurrentPage = indexDeleted;
            // Prevents bugs
            this.photoDisplay.UpdateLayout();
            OnUndo.Invoke();
        }
    }
}