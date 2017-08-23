/*
 * Author(s): Joshua Beto
 * Company: MindTAPP
 */

using UnityEngine;

namespace MindTAPP.Unity.PhotoCapture
{
    public class OrientateVirtualCamera : MonoBehaviour
    {
        private Vector3 rotationVector; // Stores rotation vector for update

        // Use this for initialization
        void Start()
        {
            rotationVector = Vector3.zero;
        }

        // Updates camera rotation to match the input orientation
        // for correctly oriented camera photos.
        // Assumes screen rotation is set to 'Portrait'
        private void Update()
        {
            switch (Input.deviceOrientation)
            {
                case DeviceOrientation.Portrait:
                    rotationVector.z = 0;
                    break;
                case DeviceOrientation.LandscapeLeft:
                    rotationVector.z = -90;
                    break;
                case DeviceOrientation.LandscapeRight:
                    rotationVector.z = 90;
                    break;
                case DeviceOrientation.PortraitUpsideDown:
                    rotationVector.z = 180;
                    break;
                default:
                    break;
            }
            transform.localEulerAngles = rotationVector;
        }
    }
}