using Managers;
using UnityEngine;

namespace StateMachines.PlayerStateMachie
{
    public class PlayerFirstPersonState:PlayerBaseState
    {
        private Vector3 _direction;
        public PlayerFirstPersonState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void Enter()
        {
            StateMachine.InputReader.OnJump += Jumping;
            StateMachine.InputReader.OnChangeCamera+=CameraChange;
            CameraManager.Instance.OpenCamera("FirstPerson");

        }

        private void CameraChange()
        {
           StateMachine.SwitchState(new PlayerThirdPersonState(StateMachine));
        }

        private void Jumping()
        {
            StateMachine.SwitchState(new PlayerJumpState(StateMachine));
        }

        public override void Tick(float deltaTime)
        {
            _direction = CalculateMovement();

            if (_direction.magnitude > 0.01f)
            {
                float targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(StateMachine.transform.eulerAngles.y, targetAngle,
                    ref StateMachine.turnSmoothVelocity, StateMachine.smoothTurnTime);

                StateMachine.transform.rotation = Quaternion.Euler(0, angle, 0);

                Move(_direction * StateMachine.movementSpeed, deltaTime);
            }
            
            
            
        }

        public override void Exit()
        {
            StateMachine.InputReader.OnJump -= Jumping;
            StateMachine.InputReader.OnChangeCamera+=CameraChange;

        }
    }
}