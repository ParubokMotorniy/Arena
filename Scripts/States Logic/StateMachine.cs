using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.StatesLogic
{
    public abstract class StateMachine : MonoBehaviour
    {
        private State currentState;
        private void Update()
        {
            currentState?.Tick(Time.deltaTime);
        }
        public void SwitchState(State newState)
        {
            currentState?.Exit();
            currentState = newState;
            currentState?.Enter();     
        }
    }
}

