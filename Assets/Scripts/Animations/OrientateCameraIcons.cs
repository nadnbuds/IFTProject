using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientateCameraIcons : MonoBehaviour {
    public float speed;
    private Quaternion rotateToward;

    private void Awake()
    {
        rotateToward = Quaternion.Euler(0, 0, 0);
    }

    private void Update()
    {
        switch (Input.deviceOrientation)
        {
            case DeviceOrientation.Portrait:
                rotateToward = Quaternion.Euler(0, 0, 0);
                break;
            case DeviceOrientation.LandscapeLeft:
                rotateToward = Quaternion.Euler(0, 0, -90);
                break;
            case DeviceOrientation.LandscapeRight:
                rotateToward = Quaternion.Euler(0, 0, 90);
                break;
            case DeviceOrientation.PortraitUpsideDown:
                rotateToward = Quaternion.Euler(0, 0, 180);
                break;
            default:
                break;
        }

        float step = speed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateToward, step);
    }
}
