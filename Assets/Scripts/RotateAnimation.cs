using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAnimation : MonoBehaviour {
    public float speed;

    void Update()
    {
        Quaternion rotateToward;
        if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft)
        {
            rotateToward = Quaternion.Euler(0, 0, -90);
        }
        else if (Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {
            rotateToward = Quaternion.Euler(0, 0, 90);
        }
        else if (Input.deviceOrientation == DeviceOrientation.Portrait)
        {
            rotateToward = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            rotateToward = Quaternion.Euler(0, 0, 180);
        }

        float step = speed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateToward, step);
    }
}
