using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DisplayPhoto : MonoBehaviour {
    public GameObject PhotoAlbumPanel; // Has children images
    public EventSystem eventSystem;
    public Image EnlargedPhoto;
    private int currentChildIndex;
    
    private void Start()
    {
        GameObject image = eventSystem.currentSelectedGameObject;
        SetPhoto(image);
    }

    public void SetPhoto(GameObject photo)
    {
        for(int i = 0; i < PhotoAlbumPanel.transform.childCount; i++)
        {
            if (photo == PhotoAlbumPanel.transform.GetChild(i).gameObject)
            {
                currentChildIndex = i;
                break;
            }
        }
        SetPhoto(currentChildIndex);
    }

    public void SetPhoto(int index)
    {
        Image childPhoto = PhotoAlbumPanel.transform.GetChild(index).gameObject.GetComponent<Image>();
        EnlargedPhoto.sprite = childPhoto.sprite;
    }

    private IEnumerator DeletePhotoEndOfFrame()
    {
        if (currentChildIndex == PhotoAlbumPanel.transform.childCount)
        {
            yield break;
        }

        PopulatePhotoGrid parentImageGrid = PhotoAlbumPanel.GetComponent<PopulatePhotoGrid>();
        parentImageGrid.DeleteChildByIndex(currentChildIndex);
        yield return new WaitForEndOfFrame(); // DeleteChildByIndex deletes photo at end of frame

        if (currentChildIndex < PhotoAlbumPanel.transform.childCount) 
        {
            SetPhoto(currentChildIndex);
        }
        else
        {
            EnlargedPhoto.sprite = null;
        }
    }

    public void DeletePhoto()
    {
        StartCoroutine(DeletePhotoEndOfFrame());
    }

    public void NextPhoto()
    {
        if (currentChildIndex < PhotoAlbumPanel.transform.childCount - 1)
        {
            currentChildIndex++;
            SetPhoto(currentChildIndex);
        }
    }

    public void PreviousPhoto()
    {
        if (currentChildIndex > 0)
        {
            currentChildIndex--;
            SetPhoto(currentChildIndex);
        }
    }
}
