using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas : Singleton<Canvas> {

    public Text headerDisplay;
	// Use this for initialization
	void Start () {
        headerDisplay.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
