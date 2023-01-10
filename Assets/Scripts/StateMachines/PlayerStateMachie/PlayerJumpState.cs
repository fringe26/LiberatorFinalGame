using UnityEngine;

namespace StateMachines.PlayerStateMachie
{
    public class PlayerJumpState:PlayerBaseState
    {
        private Vector3 _momentum;
        public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
           StateMachine.JumpPhysics.Jump(6f);
           _momentum = StateMachine.Controller.velocity;
           _momentum += CalculateMovement();
           _momentum.y = 0f;

        }

        public override void Tick(float deltaTime)
        {
            Move(_momentum,deltaTime);
            if (StateMachine.Controller.isGrounded)
            {
                ReturnToPreviousState();
            }
        }

        public override void Exit()
        {
            
        }
    }
}