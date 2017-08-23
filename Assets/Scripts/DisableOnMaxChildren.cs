using UnityEngine;
using UnityEngine.UI;

public class DisableOnMaxChildren : MonoBehaviour {
    public GameObject parent;
    public int max;
    public GameObject disableMessage;
    private Button interactableButton;

    private void Start()
    {
        interactableButton = transform.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update () {
		if (parent.transform.childCount >= max)
        {
            interactableButton.interactable = false;
            disableMessage.SetActive(true);
        }
        else
        {
            interactableButton.interactable = true;
            disableMessage.SetActive(false);
        }
	}
}
