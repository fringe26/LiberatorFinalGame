using Abstractions;
using UnityEngine;

namespace StateMachines.PlayerStateMachie
{
    public abstract class PlayerBaseState : State
    {
        protected readonly PlayerStateMachine StateMachine;
        
        protected PlayerBaseState(PlayerStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        protected void Move(Vector3 motion, float deltaTime)
        {
            StateMachine.Controller.Move((motion +StateMachine.JumpPhysics.Movement) * deltaTime);
        }

        protected void ReturnToDefaultState()
        {
            StateMachine.SwitchState(new PlayerThirdPersonState(StateMachine));
        }

        protected void ReturnToPreviousState()
        {
            StateMachine.SwitchState(StateMachine.PreviousState);
        }

        protected Vector3 CalculateMovement()
        {
            Vector3 forwad = StateMachine.MainCameraTransform.forward;
            Vector3 right = StateMachine.MainCameraTransform.right;

            forwad.y = 0f;
            right.y = 0f;
            
            forwad.Normalize();
            right.Normalize();

            return forwad * StateMachine.InputReader.MovementValue.y + right * StateMachine.InputReader.MovementValue.x;
        }
    }
}