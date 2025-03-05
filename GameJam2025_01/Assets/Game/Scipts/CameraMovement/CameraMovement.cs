using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Movement speed in units per second.
    public float movementSpeed = 10f;
    // Mouse sensitivity for looking around.
    public float mouseSensitivity = 100f;

    // Current rotation around the X axis.
    private float rotationX = 0f;
    // Current rotation around the Y axis.
    private float rotationY = 0f;

    void Start()
    {
        // Lock the cursor to the game window and hide it.
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get mouse input for rotation.
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Update rotation values.
        rotationY += mouseX;
        rotationX -= mouseY;
        // Clamp the vertical rotation to prevent over-rotation.
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        // Apply the rotations to the camera.
        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);

        // Get keyboard input for movement.
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right arrow keys.
        float vertical = Input.GetAxis("Vertical");     // W/S or Up/Down arrow keys.

        // Calculate movement direction relative to the camera's orientation.
        Vector3 direction = transform.forward * vertical + transform.right * horizontal;
        // Move the camera.
        transform.position += direction * movementSpeed * Time.deltaTime;
    }
}
