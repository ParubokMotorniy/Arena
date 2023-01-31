using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.StatesLogic
{
    public class PlayerJumpingState : PlayerBaseState
    {
        private readonly int jumpingHash = Animator.StringToHash("Jump");
        private const float croosFadeDuration = 0.1f;
        private Vector3 momentum;
        public PlayerJumpingState(PlayerStateMachine stateMachine) : base(stateMachine){}

        public override void Enter()
        {
            stateMachine.ledgeDetector.OnLedgeDetect += HandleHanging;

            stateMachine.forceReceiver.Jump(stateMachine.jumpForce);

            momentum = stateMachine.characterController.velocity;
            momentum.y = 0;

            stateMachine.playerAnimator.CrossFadeInFixedTime(jumpingHash,croosFadeDuration);
        }

        public override void Exit()
        {
            stateMachine.ledgeDetector.OnLedgeDetect -= HandleHanging;
        }

        private void HandleHanging(Vector3 ledgeForward, Vector3 closestPoint)
        {
            stateMachine.SwitchState(new PlayerHangingState(stateMachine,ledgeForward,closestPoint));
        }

        public override void Tick(float deltaTime)
        {
            Move(momentum,deltaTime);

            if(stateMachine.characterController.velocity.y <= 0)
            {
                stateMachine.SwitchState(new PlayerFallingState(stateMachine));
                return;
            }

            FaceTarget();
        }
    }
}

