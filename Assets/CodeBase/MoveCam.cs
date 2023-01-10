using System;
using UnityEngine;

namespace CodeBase
{
    public class MoveCam : MonoBehaviour
    {
        [SerializeField] private Transform _cameraPosition;
        private Transform _transform;
        private void Start()
        {
            _transform = transform;
        }

        private void Update()
        {
            _transform.position = _cameraPosition.position;
        }
    }
}
