using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace CodeBase
{
    public class Resize : MonoBehaviour
    {
        public LayerMask _detectWalls;
        [Header("Components")] public Transform target; // The target object we picked up for scaling
        public Collider targetCollider;

        [Header("Parameters")]
        public LayerMask targetMask; // The layer mask used to hit only potential targets with a raycast

        public LayerMask
            ignoreTargetMask; // The layer mask used to ignore the player and target objects while raycasting

        public float offsetFactor; // The offset amount for positioning the object so it doesn't clip into walls

        private float originalDistance; // The original distance between the player camera and the target
        private float originalScale; // The original scale of the target objects prior to being resized
        private Vector3 targetScale; // The scale we want our object to be set to each frame
        private Bounds _bounds;

        private new Camera _camera;

        void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            _camera = Camera.main;
        }


        void Update()
        {
            HandleInput();
            ResizeTarget();
        }

        private Bounds GetBounds(GameObject root)
        {
            var bounds = new Bounds();

            var renderers = root.GetComponentsInChildren<Renderer>(false);
            var center = renderers.Aggregate(Vector3.zero, (current, transform) => current + transform.bounds.center);
            bounds.center = center / renderers.Length;
            foreach (var renderer in renderers)
            {
                bounds.Encapsulate(renderer.bounds);
            }

            return bounds;
        }

        void HandleInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (target == null)
                {
                    if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out var hit,
                            Mathf.Infinity, targetMask))
                    {
                        target = hit.transform;
                        targetCollider = target.GetComponent<Collider>();
                        target.GetComponent<Rigidbody>().isKinematic = true;
                        originalDistance = Vector3.Distance(_camera.transform.position, target.position);
                        targetScale = target.localScale;
                        originalScale = targetScale.x;

                        _bounds = GetBounds(target.gameObject);
                    }
                }
                else
                {
                    target.GetComponent<Rigidbody>().isKinematic = false;
                    target = null;
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (target != null)
            {
                Gizmos.color = Color.red;
                var position = target.position;
                var rotationMatrix = Matrix4x4.TRS(position, target.localRotation, Vector3.one);
                Gizmos.matrix = rotationMatrix;
                Gizmos.DrawWireCube(Vector3.zero, _bounds.size);
            }
        }

        void ResizeTarget()
        {
            if (target == null)
            {
                return;
            }


            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out var hit, Mathf.Infinity,
                    ignoreTargetMask))
            {
                target.position = hit.point;
                var targetPosition = target.position;
                var currentDistance = Vector3.Distance(_camera.transform.position, targetPosition);
                var s = currentDistance / originalDistance;
                targetScale.x = targetScale.y = targetScale.z = s;
                target.localScale = targetScale * originalScale;

                _bounds = GetBounds(target.gameObject);

                var walls = Physics.OverlapBox(targetPosition, _bounds.extents, target.rotation, _detectWalls);

                foreach (var wall in walls)
                {
                    var wallTransform = wall.transform;
                    if (Physics.ComputePenetration(targetCollider, target.position, target.rotation, wall,
                            wallTransform.position, wallTransform.rotation,
                            out var dir, out var dist))
                    {
                        target.position += dir * dist;

                        Debug.DrawLine(hit.point, target.position, Color.green, 1f);
                    }
                }
            }
        }
    }
}