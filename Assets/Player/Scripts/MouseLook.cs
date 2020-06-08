using System;
using UnityEngine;

namespace Player
{
    public class MouseLook : MonoBehaviour
    {

        public float mouseSensitivity = 1000f;

        public Transform cameraTransform;

        private float xRotation = 0f;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            var inputMouse = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            inputMouse = mouseSensitivity * Time.deltaTime * inputMouse;

            xRotation -= inputMouse.y;
            xRotation = Mathf.Clamp(xRotation, -80f, 90f);
    
            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * inputMouse.x);
        }
    }
}