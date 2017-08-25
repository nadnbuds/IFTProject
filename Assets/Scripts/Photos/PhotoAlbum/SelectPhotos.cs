/*
 * Author(s): Joshua Beto
 * Company: MindTAPP
 */

using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace MindTAPP.Unity.Gallery 
{
    // Assumes that the list of photo children under thumbnailContent
    // has not been changed once selection mode is enabled
    public class SelectPhotos : MonoBehaviour
    {
        public UnityEvent OnEnableSelection { get; private set; }
        public UnityEvent OnDisableSelection { get; private set; }

        [SerializeField] private GameObject thumbnailContent;
        [SerializeField] private Button buttonColors;

        private IEnumerable<Button> allButtons;
        private HashSet<Button> selectedObjects;
        private UnityAction selectable;

        private ColorBlock selectedButtonColors;
        private ColorBlock originalButtonColors;

        private void Awake()
        {
            OnEnableSelection = new UnityEvent();
            OnDisableSelection = new UnityEvent();
            selectable = new UnityAction(ToggleSelection);
            selectedObjects = new HashSet<Button>();
        }

        private void Start()
        {
            InitColors();
        }

        private void InitColors()
        {
            originalButtonColors = buttonColors.colors;
            // Swaps selected disable with normal and highlighted colors
            selectedButtonColors = originalButtonColors;
            selectedButtonColors.normalColor = originalButtonColors.pressedColor;
            selectedButtonColors.highlightedColor = originalButtonColors.pressedColor;
            selectedButtonColors.pressedColor = originalButtonColors.normalColor;
        }

        public void AlternateAllSelects(bool isSelected)
        {
            if (isSelected)
            {
                SelectAll();
            }
            else
            {
                DeselectAll();
            }
        }

        public void SelectAll()
        {
            foreach (Button selectableButton in allButtons.Except(selectedObjects))
            {
                AddSelection(selectableButton);
            }
        }

        public void DeselectAll()
        {
            foreach (Button selectedButton in selectedObjects)
            {
                selectedButton.colors = originalButtonColors;
            }
            selectedObjects.Clear();
        }

        private void AddSelection(Button toAdd)
        {
            Debug.Log("Hi");
            toAdd.colors = selectedButtonColors;
            selectedObjects.Add(toAdd);
            Debug.Log("Count: " + selectedObjects.Count);
        }

        private void RemoveSelection(Button toDelete)
        {
            toDelete.colors = originalButtonColors;
            selectedObjects.Remove(toDelete);
        }

        private void ToggleSelection()
        {
            GameObject selected = EventSystem.current.currentSelectedGameObject;
            Button thumbnail = selected.GetComponent<Button>();

            if (selectedObjects.Contains(thumbnail))
            {
                RemoveSelection(thumbnail);
            }
            else
            {
                AddSelection(thumbnail);
            }
        }

        public void EnableSelectionMode()
        {
            Debug.Log("Enable Selection Mode");
            allButtons = thumbnailContent.GetComponentsInChildren<Button>();
            OnEnableSelection.Invoke();
            foreach (Button selectableButton in allButtons)
            {
                selectableButton.onClick.AddListener(selectable);
            }
        }

        public void DisableSelectionMode()
        {
            Debug.Log("Disable Selection Mode");
            OnDisableSelection.Invoke();
            foreach (Button selectableButton in allButtons)
            {
                selectableButton.onClick.RemoveListener(selectable);
            }
            foreach (Button selectedButton in selectedObjects)
            {
                selectedButton.colors = originalButtonColors;
            }
        }

        public IEnumerable<GameObject> GetSelections()
        {
            Debug.Log("Number of objects: " + selectedObjects.Count);
            return selectedObjects.Select(item => item.gameObject);
        }
    }
}