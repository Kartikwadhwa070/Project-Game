using UnityEngine;
using Lean.Touch;

public class LeanTouchRotateZoom : MonoBehaviour
{
    public float rotationSpeed = 5f; // Speed of rotation
    public float zoomSpeed = 0.1f;   // Speed of zooming
    public float minZoom = 5f;      // Minimum camera distance
    public float maxZoom = 20f;     // Maximum camera distance

    private Camera mainCamera;

    private void Start()
    {
        // Get the main camera
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("No Main Camera found in the scene!");
        }
    }

    private void OnEnable()
    {
        LeanTouch.OnFingerUpdate += OnFingerUpdate;
    }

    private void OnDisable()
    {
        LeanTouch.OnFingerUpdate -= OnFingerUpdate;
    }

    private void OnFingerUpdate(LeanFinger finger)
    {
        // Check if more than one finger is on the screen
        if (LeanTouch.Fingers.Count == 1)
        {
            // Rotate the object based on single-finger movement
            float swipeX = finger.ScreenDelta.x * rotationSpeed * Time.deltaTime;
            float swipeY = finger.ScreenDelta.y * rotationSpeed * Time.deltaTime;

            transform.Rotate(Vector3.up, -swipeX, Space.World); // Horizontal rotation
            transform.Rotate(Vector3.right, swipeY, Space.World); // Vertical rotation
        }
    }

    private void Update()
    {
        // Pinch-to-zoom functionality (only when more than one finger is active)
        if (LeanTouch.Fingers.Count > 1)
        {
            float pinchScale = LeanGesture.GetPinchScale();

            // Adjust camera field of view for zooming
            if (mainCamera != null)
            {
                mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView / pinchScale, minZoom, maxZoom);
            }
        }
    }
}

