/*
 * Author(s): Joshua Beto
 * Company: MindTAPP
 */

using System.Collections;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using UnityEngine.EventSystems;

using MindTAPP.Unity.IFT;

namespace MindTAPP.Unity.Gallery
{
    /*
    public class DisplayPhoto : MonoBehaviour
    {
        [SerializeField] private HorizontalScrollSnap photoDisplayScroll;
        [SerializeField] private EventSystem eventSystem;
        private IPhotoService photoService;
        
        private void Awake()
        {
            photoService = FindObjectOfType<PhotoDirectory>();
        }

        //TODO: revamp logic
        // Currently sets starting screen (photo)
        private void OnEnable()
        {
            Sprite photo = eventSystem.currentSelectedGameObject.GetComponent<Image>().sprite;
            for (int childIndex = 0; childIndex < transform.childCount; childIndex++) 
            {
                if (transform.GetChild(childIndex).Equals(photo))
                {
                    photoDisplayScroll.StartingScreen = childIndex;
                    return;
                }
            }

        }
        
        private IEnumerator DeletePhotoEndOfFrame(int indexToDelete)
        {
            Transform photoToDelete = transform.GetChild(indexToDelete);

            // Delete photo's internal storage
            photoService.DeletePhoto(photoToDelete.GetComponent<Image>().sprite);

            // Destroy physical game object representation of photo
            Destroy(photoToDelete.gameObject);

            // Destroy occurs at end of frame. Not waiting for end of frame would cause esoteric interactions
            // where the game object is still in the scene, but not yet destroyed.
            yield return new WaitForEndOfFrame();
        }

        public void DeletePhoto()
        {
            StartCoroutine(DeletePhotoEndOfFrame(photoDisplayScroll.CurrentPage));
        }
    } */
}