using System;
using UnityEngine;

namespace CameraScripts
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform _playerCamera;
        [SerializeField] private float _sensitivity;
        private Vector2 _mouseInput;
        private float _xRotation;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void MovePlayerCamera()
        {
            _xRotation -= _mouseInput.y * _sensitivity;
            transform.Rotate(0f, _mouseInput.x * _sensitivity, 0f);
            _playerCamera.localRotation = Quaternion.Euler(_xRotation,0f,0f);
        }

        private void Update()
        {
            _mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            MovePlayerCamera();
        }
    }
}