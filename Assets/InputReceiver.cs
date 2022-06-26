using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class InputReceiver : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private float _commonSpeed = 10f;
    private float _runSpeed = 20f;
    private CharacterController _characterController;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!Input.anyKey)
            return;
        CheckInput();
    }

    private void CheckInput()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        var inputAxis = transform.right * x + transform.forward * z;
        var sqrMagnitude = inputAxis.sqrMagnitude;
        var moveSpeed = Input.GetKey(KeyCode.LeftShift) ? _runSpeed : _commonSpeed;
        if (sqrMagnitude > float.Epsilon)
        {
            _characterController.Move(inputAxis * moveSpeed * Time.deltaTime);
        }

        if (Input.GetButton("Fire1"))
        {
            var screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
            Ray ray = _camera.ScreenPointToRay(screenCenter);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var hitObject = hit.transform.gameObject;
                if (hitObject.CompareTag("Enemy"))
                {
                    hitObject.GetComponent<MeshRenderer>().material.color = Color.blue;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("other: " + other.collider.tag);
    }
}
