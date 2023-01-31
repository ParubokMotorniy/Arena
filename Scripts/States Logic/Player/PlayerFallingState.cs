using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.StatesLogic
{
    public class PlayerFallingState : PlayerBaseState
    {
        private readonly int fallingHash = Animator.StringToHash("Fall");
        private const float croosFadeDuration = 0.1f;

        private Vector3 momentum;
        public PlayerFallingState(PlayerStateMachine stateMachine) : base(stateMachine){}

        public override void Enter()
        {
            stateMachine.ledgeDetector.OnLedgeDetect += HandleLedgeDetect;

            stateMachine.playerAnimator.CrossFadeInFixedTime(fallingHash,croosFadeDuration);
            momentum = stateMachine.characterController.velocity;
            momentum.y = 0;
        }

        public override void Exit()
        {
            stateMachine.ledgeDetector.OnLedgeDetect -= HandleLedgeDetect;
        }

        private void HandleLedgeDetect(Vector3 ledgeForward,Vector3 closestPoint)
        {
            stateMachine.SwitchState(new PlayerHangingState(stateMachine,ledgeForward,closestPoint));
        }

        public override void Tick(float deltaTime)
        {
            Move(momentum,deltaTime);

            if (stateMachine.characterController.isGrounded)
            {
                ReturnToLocomotion();
            }

            FaceTarget();
        }
    }
}

