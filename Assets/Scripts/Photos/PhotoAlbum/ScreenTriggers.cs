/*
 * Author(s): Joshua Beto
 * Company: MindTAPP
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MindTAPP.Unity.Gallery;
using UnityEngine.SceneManagement;
using MindTAPP.Unity.IFT;
using MindTAPP.Unity.PhotoCapture;

//TODO: Redo this class with a UIScreenManager to properly alternate between
// different screens utilizing UnityAction/UnityEvent
public class ScreenTriggers : MonoBehaviour
{
    [SerializeField] private SelectPhotos selectionMode;
    [SerializeField] private InitialPhoto bam;
    [SerializeField] private Toggle checkmark;
    [SerializeField] private ViewThumbnails thumbnails;
    [SerializeField] private GameObject optionsBar;
    [SerializeField] private GameObject selectionScreen;

    public void TransitionToSelectionScreen()
    {
        selectionScreen.SetActive(true);
        selectionMode.EnableSelectionMode();
        optionsBar.SetActive(false);
    }

    public void DeletePhotos()
    {
        thumbnails.DeletePhotos(selectionMode.GetSelections());
        TransitionToView();
    }

    public void TransitionToTheater(Image photo)
    {
        bam.StartingPhoto = photo.sprite;
        SceneManager.LoadScene("GallerySlides");
    }

    public void TransitionToView()
    {
        optionsBar.SetActive(true);
        selectionMode.DisableSelectionMode();
        checkmark.isOn = false;
        selectionScreen.SetActive(false);
    }
}
