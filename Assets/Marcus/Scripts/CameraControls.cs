using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraControls : MonoBehaviour
{
    public bool lockCamera;
    public bool proportionalInput;
    public float panSpeed = 5;
    [Range(0, 1)] public float xDeadZone = 0.99f;
    [Range(0, 1)] public float yDeadZone = 0.99f;


    Camera c;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        c = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lockCamera == false)
        {
            Vector3 s = c.ScreenToViewportPoint(Input.mousePosition); // Turns mouse position on screen into a relative point on the camera viewport
            Vector2 cameraInput = new Vector2(AxisHandler(s.x, xDeadZone), AxisHandler(s.y, yDeadZone)); // Processes ScreenToViewportPoint values into two -1 to 1 input values
            if (proportionalInput == false)
            {
                cameraInput.Normalize();
            }
            Vector3 movement = new Vector3(cameraInput.x * panSpeed, cameraInput.y * panSpeed);
            transform.Translate(movement * Time.deltaTime);
        }
    }

    float AxisHandler(float input, float deadZone)
    {
        input = (Mathf.Clamp01(input) - 0.5f) * 2; // Converts input from a 0-1 value to a -1 to 1 value, and clamps it to not exceed the desired magnitude
        if (input < deadZone && input > -deadZone) // If input value is inside deadzones, set to zero so it has no effect
        {
            return 0;
        }
        return input;
    }
}
