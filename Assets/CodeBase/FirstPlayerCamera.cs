using System;
using UnityEngine;

namespace CodeBase
{
    public class FirstPlayerCamera : MonoBehaviour
    {
        [SerializeField] private float _sensX;
        [SerializeField] private float _sensY;

        [SerializeField] private Transform _orientation;

        private float _xRotation;
        private float _yRotation;
        private Transform _transform;
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _transform = transform;
        }

        private void Update()
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * _sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * _sensX;

            _yRotation += mouseX;
            _xRotation -= mouseY;

            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
            
            //rotate cam and orientation mathafucka
            _transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
            _orientation.rotation = Quaternion.Euler(0,_yRotation,0);

        }
    }
}
