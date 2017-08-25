/*
 * Author(s): Joshua Beto
 * Company: MindTAPP
 */

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using MindTAPP.Unity.IFT;

namespace MindTAPP.Unity.Gallery
{
    // Grabs a list of all photos from PhotoDirectory and adds all these photos to
    // the copied GameObject's Image component as children of the current GameObject
    // this script is attached to.
    public class ViewThumbnails : MonoBehaviour
    {
        [SerializeField] private GameObject photoToCopy;
        [SerializeField] IPhotoService photoService;

        // Holds undo data
        private List<PhotoMemento> photosDeleted;

        private void Start()
        {
            foreach (Sprite photoSprite in this.photoService.GetPhotos())
            {
                GameObject photo = Instantiate(this.photoToCopy, transform);
                photo.GetComponent<Image>().sprite = photoSprite;
            }
        }

        public void DeletePhotos(List<GameObject> photosToDelete)
        {
            // Clear Previous Memento
            photosDeleted.Clear();

            foreach (GameObject photoGO in photosToDelete)
            {
                // Deletion
                Sprite photoSprite = photoGO.GetComponent<Image>().sprite;
                string fileName = photoService.DeletePhoto(photoSprite);
                photoGO.SetActive(false);

                // Save Memento State
                photosDeleted.Add(new PhotoMemento(photoGO, photoSprite, fileName));
            }
        }

        public void UndoDeletion()
        {
            foreach (PhotoMemento deletion in photosDeleted)
            {
                photoService.AddPhoto(deletion.PhotoSprite, deletion.FileName);
                deletion.PhotoGameObject.SetActive(true);
            }
        }
    }
}