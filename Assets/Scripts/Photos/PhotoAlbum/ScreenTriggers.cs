/*
 * Author(s): Joshua Beto
 * Company: MindTAPP
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MindTAPP.Unity.Gallery;
using MindTAPP.Unity.IFT;
using MindTAPP.Unity.PhotoCapture;

//TODO: Redo this class with a UIScreenManager to properly alternate between
// different screens utilizing UnityAction/UnityEvent
public class ScreenTriggers : MonoBehaviour
{
    [SerializeField] private SelectPhotos selectionMode;
    [SerializeField] private Toggle checkmark;
    [SerializeField] private ViewThumbnails thumbnails;
    [SerializeField] private GameObject optionsBar;
    [SerializeField] private GameObject selectionScreen;

    public void TransitionToSelectionScreen()
    {
        optionsBar.SetActive(false);
        selectionScreen.SetActive(true);
        selectionMode.EnableSelectionMode();
    }

    public void DeletePhotos()
    {
        thumbnails.DeletePhotos(selectionMode.GetSelections());
    }

    public void TransitionToView()
    {
        optionsBar.SetActive(true);
        selectionScreen.SetActive(false);
        selectionMode.DisableSelectionMode();
        checkmark.isOn = false;
    }
}
