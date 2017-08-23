using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(LoadingFade());
	}
	IEnumerator LoadingFade()
    {
        yield return new WaitForSeconds(3.0f);
        this.gameObject.GetComponent<LoadSceneOnClick>().loadByName("MainMenu");
    }
	// Update is called once per frame
	void Update () {
		
	}
}
