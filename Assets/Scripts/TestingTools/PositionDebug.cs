using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionDebug : MonoBehaviour {
    private Vector3 pos;
    private Text myText;
	// Use this for initialization
	void Start () {
        pos = transform.localPosition;
        myText = GetComponentInChildren<Text>();
        if (myText == null)
        {
            Debug.Log("LOL");
        }
        myText.text = pos.ToString();
        Debug.Log(pos);
	}
	
	// Update is called once per frame
	void Update () {
        if (!pos.Equals(transform.localPosition))
        {
            pos = transform.localPosition;
            myText.text = pos.ToString();
            Debug.Log(transform.localPosition);
        }
	}
}
