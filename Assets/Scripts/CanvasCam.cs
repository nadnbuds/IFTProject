using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasCam : MonoBehaviour {
    public GameObject myCanvas;
    public Camera newCam;
    public Camera baseCam;

	// Use this for initialization
	void Start () {
        //myCanvas = GetComponent<CanvasRenderer>();
	}
	
	public void SwitchCam()
    {
        //myCanvas.worldCamera = newCam;
    }

    public void BaseCam()
    {
      //  myCanvas.worldCamera = baseCam;
    }
}
