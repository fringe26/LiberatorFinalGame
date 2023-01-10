using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase
{
    public class CemraRaycastChanger : MonoBehaviour
    {
        [SerializeField] private float rayRange;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private Image _aim;
        public bool IsOnHint { get; set; } = false;
        private new Camera _camera;
        private void Start()
        {
            _camera = Camera.main;
            
        }

        void LateUpdate()
        {
            Debug.DrawRay(_camera.transform.position, _camera.transform.forward*int.MaxValue, Color.cyan);

            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hit, rayRange, layerMask))
            {
                if (IsOnHint)
                {
                    _aim.color = Color.green;
                }
                
                Debug.Log(hit.collider.gameObject.name);
                if (Input.GetKey(KeyCode.X) && IsOnHint)
                {
                    hit.collider.gameObject.SetActive(false);
                }
            }
            else
            {
                _aim.color = Color.white;
            }
            
        }
    }
}
