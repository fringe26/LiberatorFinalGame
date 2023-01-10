using Abstractions;
using UnityEngine;

namespace StateMachines
{
    public abstract class StateMachine : MonoBehaviour
    {
        private State _currentState;
        public State PreviousState { get; private set; }

        public void SwitchState(State newState)
        {
            PreviousState = _currentState;
            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter(); 
        }
        private void Update()
        {
            _currentState?.Tick(Time.deltaTime);
        }
    }
}