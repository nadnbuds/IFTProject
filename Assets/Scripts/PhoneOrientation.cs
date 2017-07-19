using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneOrientation : MonoBehaviour {

    private ScreenOrientation baseScreenOrientation;
    [SerializeField] private ScreenOrientation activeScreenOrientation;
    // Use this for initialization
    void Start () {
        baseScreenOrientation = Screen.orientation;
        Screen.orientation = activeScreenOrientation;
	}
	
	// Update is called once per frame
	void OnDisable () {
        Screen.orientation = baseScreenOrientation;
	}
}
