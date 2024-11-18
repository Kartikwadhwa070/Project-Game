using UnityEngine;

public class SwipeAndZoom : MonoBehaviour
{
    public float rotationSpeed = 0.2f;
    public float zoomSpeed = 0.1f; 
    public float minZoom = 20f; 
    public float maxZoom = 80f;  

    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private bool isDragging;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        startTouchPosition = touch.position;
                        isDragging = true;
                        break;

                    case TouchPhase.Moved:
                        if (isDragging)
                        {
                            currentTouchPosition = touch.position;
                            Vector2 delta = currentTouchPosition - startTouchPosition;

                            
                            float rotationX = delta.y * rotationSpeed; 
                            float rotationY = -delta.x * rotationSpeed; // Horizontal swipe rotates around Y-axis
                            transform.Rotate(rotationX, rotationY, 0, Space.World);

                            startTouchPosition = currentTouchPosition; // Update the start position
                        }
                        break;

                    case TouchPhase.Ended:
                        isDragging = false;
                        break;
                }
            }

            // Detect multi-touch for zoom
            if (Input.touchCount == 2)
            {
                Touch touch1 = Input.GetTouch(0);
                Touch touch2 = Input.GetTouch(1);

                // Calculate previous and current distances between the two touches
                float prevDistance = (touch1.position - touch2.position).magnitude;
                float currentDistance = ((touch1.position + touch1.deltaPosition) - (touch2.position + touch2.deltaPosition)).magnitude;

                // Calculate zoom delta
                float zoomDelta = (currentDistance - prevDistance) * zoomSpeed;

                // Adjust the camera's field of view
                Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView - zoomDelta, minZoom, maxZoom);
            }
        }
    }
}
