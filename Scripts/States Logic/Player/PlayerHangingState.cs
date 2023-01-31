using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.StatesLogic
{
    public class PlayerHangingState : PlayerBaseState
    {
        private readonly int hangingHash = Animator.StringToHash("Hanging");
        private Vector3 ledgeForward;
        private Vector3 closestPoint;
        private const float croosFadeDuration = 0.1f;
        public PlayerHangingState(PlayerStateMachine stateMachine,Vector3 ledgeForward,Vector3 closestPoint) : base(stateMachine)
        {
            this.ledgeForward = ledgeForward;
            this.closestPoint = closestPoint;
        }

        public override void Enter()
        {
            stateMachine.transform.rotation = Quaternion.LookRotation(ledgeForward,Vector3.up);
            stateMachine.transform.position += closestPoint - stateMachine.ledgeDetector.transform.position;

            stateMachine.characterController.enabled = false;
            stateMachine.playerAnimator.CrossFadeInFixedTime(hangingHash,croosFadeDuration);
            stateMachine.characterController.enabled = true;
        }

        public override void Exit()
        {
            
        }

        public override void Tick(float deltaTime)
        {
            if(stateMachine.inputReader.movementValue.y < 0)
            {
                stateMachine.characterController.Move(Vector3.zero);
                stateMachine.forceReceiver.ResetForces();
                stateMachine.SwitchState(new PlayerFallingState(stateMachine));
                return;
            }
            if(stateMachine.inputReader.movementValue.y > 0)
            {
                stateMachine.SwitchState(new PlayerPullUpState(stateMachine));
                return;
            }
        }
    }
}

