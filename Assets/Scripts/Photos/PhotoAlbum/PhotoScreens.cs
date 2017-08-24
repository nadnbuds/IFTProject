/*
 * Author(s): Joshua Beto
 * Company: MindTAPP
 */

using System.Collections;
using System.Linq;

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using UnityEngine.EventSystems;

using MindTAPP.Unity.IFT;

namespace MindTAPP.Unity.Gallery
{
    public class PhotoScreens : MonoBehaviour
    {
        [SerializeField] private HorizontalScrollSnap photoDisplay;
        [SerializeField] private GameObject prefabImage;
        [SerializeField] private IPhotoService photoService;
        public Sprite StartingPhoto { get; set; }

        private void Awake()
        {
            foreach (Sprite photoSprite in photoService.GetPhotos())
            {
                GameObject photo = Instantiate(this.prefabImage, transform);
                photo.GetComponent<Image>().sprite = photoSprite;
                if (photoSprite == StartingPhoto)
                {
                    photoDisplay.StartingScreen = photo.transform.GetSiblingIndex();
                }
            }
        }
        
        public void DeletePhoto()
        {
            if (photoDisplay._screensContainer.childCount <= 0)
            {
                return;
            }
            // Moves child screen outside view
            GameObject photoRemoved;
            photoDisplay.RemoveChild(photoDisplay.CurrentPage, true, out photoRemoved);
            // Delete photo's internal storage
            photoService.DeletePhoto(photoRemoved.GetComponent<Image>().sprite);
        }
    }
}