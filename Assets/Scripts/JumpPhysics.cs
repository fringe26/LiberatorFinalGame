using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Vector3 = UnityEngine.Vector3;

public class JumpPhysics : MonoBehaviour
{
    private CharacterController _characterController;
    [SerializeField] private float _drag = 5f;

    private Vector3 _dumpingVelocity;
    private Vector3 _impact;
    private float _verticalVelocity;

    public Vector3 Movement => _impact + Vector3.up * _verticalVelocity;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (_verticalVelocity < 0 && _characterController.isGrounded)
        {
            _verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            _verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        _impact = Vector3.SmoothDamp(_impact, Vector3.zero, ref _dumpingVelocity, _drag);
        
    }

    public void Reset()
    {
        _impact = Vector3.zero;
        _verticalVelocity = 0;
    }

    public void Jump(float jumpForce)
    {
        _verticalVelocity = jumpForce;
    }
    
    
}
