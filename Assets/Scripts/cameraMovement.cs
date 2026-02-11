using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        LoadSettings();

        xRotation = 0f; 
        transform.localRotation = Quaternion.Euler(45f, 0f, 0f);
        
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void LoadSettings()
    {
        if (PlayerPrefs.HasKey("controllerSensitivity"))
        {
            mouseSensitivity = PlayerPrefs.GetInt("controllerSensitivity");
            Debug.Log("ada");
        }
        else
        {
            Debug.Log(" nah ");
        }
    }

    public void SetLookRotation(float xRot)
    {
        xRotation = xRot;
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    public float GetXRotation()
    {
        return xRotation;
    }
}


