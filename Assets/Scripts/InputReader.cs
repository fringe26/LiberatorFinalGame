using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{

    public event Action OnJump;
    public event Action OnChangeCamera; 
    
    private Controls _controls;

    #region MonoBehaviors

    private void Start()
    {
        _controls = new Controls();
        _controls.Player.SetCallbacks(this);
        _controls.Player.Enable();
    }

    private void OnDestroy()
    {
        _controls.Player.Disable();
    }

    #endregion
  

    public Vector3 MovementValue { get; private set; }

    public void OnMoving(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnJumping(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        OnJump?.Invoke();
    }

    public void OnCameraChanger(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        OnChangeCamera?.Invoke();
    }
}