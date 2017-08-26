using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class Dissapear : MonoBehaviour {
    public HorizontalScrollSnap snap;
    private int index;

    // Use this for initialization
    void OnEnable () {
        index = snap.CurrentPage;
        StartCoroutine(Wait());
	}

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (snap.CurrentPage != index)
        {
            gameObject.SetActive(false);
        }
    }
}
