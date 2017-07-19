using UnityEngine;

public class PinchZoom : MonoBehaviour
{
    [SerializeField] private RectTransform zoomObject;
    [SerializeField] private float zoomSpeed = 0.1f;

    private void adjustScaleBounds(ref float scaleDimension)
    {
        if (scaleDimension < 1)
        {
            scaleDimension = 1;
        }
        else if (scaleDimension > 4)
        {
            scaleDimension = 4;
        }
    }
    
    public void ChangeScale(float resize)
    {
        Vector3 zoomScale = zoomObject.localScale;
        zoomScale.x = resize;
        zoomScale.y = resize;
        zoomObject.localScale = zoomScale;
    }

    private void Update()
    {
        // If there are two touches on the device...
        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
            Debug.Log("Magnitude Difference: " + deltaMagnitudeDiff);

            Vector3 zoomScale = zoomObject.localScale;
            zoomScale.x -= deltaMagnitudeDiff * zoomSpeed;
            zoomScale.y -= deltaMagnitudeDiff * zoomSpeed;
            adjustScaleBounds(ref zoomScale.x);
            adjustScaleBounds(ref zoomScale.y);
            zoomObject.localScale = zoomScale;
            Debug.Log("Zoom: " + zoomObject.localScale);
        }
    }
}