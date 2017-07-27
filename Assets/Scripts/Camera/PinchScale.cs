using UnityEngine;

public class PinchScale : MonoBehaviour
{
    [SerializeField] private float scaleSpeed = 0.5f;

    // Returns a float within the range of 1 to 4
    // Rounds up if below the boundary / Down if upper boundary
    private float GetBoundedScale(float scaleDimension)
    {
        if (scaleDimension < 1)
        {
            return 1;
        }
        else if (scaleDimension > 4)
        {
            return 4;
        }
        else
        {
            return scaleDimension;
        }
    }
    
    // Trigger function to resize scale
    public void ChangeScale(float resize)
    {
        Vector3 scale = transform.localScale;
        scale.x = resize;
        scale.y = resize;
        transform.localScale = scale;
    }

    // Detects pinch, and scales object up or down
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
            float deltaMagnitudeDiff = (prevTouchDeltaMag - touchDeltaMag) / 30; //30 is an arbitrary number to reduce zoom speed further

            // Adjust to new scale 
            Vector3 zoomScale = transform.localScale;
            zoomScale.x -= deltaMagnitudeDiff * scaleSpeed;
            zoomScale.y -= deltaMagnitudeDiff * scaleSpeed;

            zoomScale.x = GetBoundedScale(zoomScale.x);
            zoomScale.y = GetBoundedScale(zoomScale.y);

            transform.localScale = zoomScale;
        }
    }
}