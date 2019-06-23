using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField]
    private string mouseXInputName, mouseYInputName;
    [SerializeField]
    private float mouseSensitivity;
    [SerializeField]
    private Transform playerBody;
    private float yLookClamp;

    private void Awake()
    {
        LockCursor();
        yLookClamp = 0.0f;
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        CameraRotation();
    }


    private void CameraRotation()
    {
        float mouseX = Input.GetAxis(mouseXInputName) * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis(mouseYInputName) * mouseSensitivity * Time.deltaTime;

        yLookClamp += mouseY;

        if(yLookClamp > 90)
        {
            yLookClamp = 90;
            mouseY = 0;
            ClampYLookToValue(270);
        }
        else if(yLookClamp < -90)
        {
            yLookClamp = -90;
            mouseY = 0;
            ClampYLookToValue(90);
        }

        transform.Rotate(Vector3.left * mouseY);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void ClampYLookToValue(float clampValue)
    {
        Vector3 eulerRotationCamera = transform.eulerAngles;
        eulerRotationCamera.x = clampValue;
        transform.eulerAngles = eulerRotationCamera;
    }
}
