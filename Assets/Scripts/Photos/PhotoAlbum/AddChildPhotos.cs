/*
 * Author(s): Joshua Beto
 * Company: MindTAPP
 */

using UnityEngine;
using UnityEngine.UI;

using MindTAPP.Unity.IFT;

namespace MindTAPP.Unity.Gallery
{
    // Grabs a list of all photos from PhotoDirectory and adds all these photos to
    // the copied GameObject's Image component as children of the current GameObject
    // this script is attached to.
    public class AddChildPhotos : MonoBehaviour
    {
        [SerializeField] private GameObject photoToCopy;
        [SerializeField] IPhotoService photoService;

        private void Awake()
        {
            foreach (Sprite photoSprite in photoService.GetPhotos())
            {
                GameObject photo = Instantiate(this.photoToCopy, transform);
                photo.GetComponent<Image>().sprite = photoSprite;
            }
        }
    }
}