using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempTransparent : MonoBehaviour {
    private UnityEngine.UI.Image logo;
    private Color background;
    public GameObject EndScreen;

    private void Start()
    {
        logo = GetComponent<UnityEngine.UI.Image>();
        background = logo.color;
    }

    // Update is called once per frame
    void Update () {
		if (EndScreen.activeInHierarchy)
        {
            background.a = .25f; // Transparent
            logo.color = background;
        }
        else
        {
            background.a = 1; // Opaque
            logo.color = background;
        }
	}
}