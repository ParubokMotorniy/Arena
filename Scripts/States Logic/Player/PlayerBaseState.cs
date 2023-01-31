using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.StatesLogic
{
    public abstract class PlayerBaseState : State
    {
        protected PlayerStateMachine stateMachine;
        public PlayerBaseState(PlayerStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
        protected void Move(Vector3 motion, float deltaTime)
        {
            stateMachine.characterController.Move((motion  + stateMachine.forceReceiver.movement) * deltaTime);
        }
        protected void Move (float deltaTime)
        {
            stateMachine.characterController.Move(stateMachine.forceReceiver.movement * deltaTime);
        }
        protected void FaceTarget()
        {
            if(stateMachine.targeter.currentTarget == null) { return; }

            Vector3 lookPos = stateMachine.targeter.currentTarget.transform.position - stateMachine.transform.position;
            lookPos.y = 0;

            stateMachine.transform.rotation = Quaternion.LookRotation(lookPos); 
        }
        protected void ReturnToLocomotion()
        {
            if(stateMachine.targeter.currentTarget != null)
            {
                stateMachine.SwitchState(new TargetingState(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new PlayerFreelookState(stateMachine));
            }
        }
    }

}
