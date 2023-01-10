using CodeBase;
using UnityEngine;

namespace MonoBeh
{
    public class IllusionHintChanger : MonoBehaviour
    {
        private CemraRaycastChanger _raycastChanger;

        private void Start()
        {
            _raycastChanger = FindObjectOfType<CemraRaycastChanger>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Hint"))
            {
                _raycastChanger.IsOnHint = true;
                Debug.Log("hint");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Hint"))
            {
                _raycastChanger.IsOnHint = false;
                Debug.Log("OutOfHint");
            }
        }
    }
}