using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.StatesLogic
{
    public class TargetingState : PlayerBaseState
    {
        private readonly int targetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
        private readonly int targetingForwardHash = Animator.StringToHash("targetingForward");
        private readonly int targetingRightHash = Animator.StringToHash("targetingRight");
        private const float croosFadeDuration = 0.1f;

        public TargetingState(PlayerStateMachine stateMachine) : base(stateMachine){}

        public override void Enter()
        {
            stateMachine.playerAnimator.CrossFadeInFixedTime(targetingBlendTreeHash, croosFadeDuration);
            stateMachine.inputReader.lockTarget += OnLock;
            stateMachine.inputReader.jumpEvent += Jump;
            stateMachine.inputReader.dodgeEvent += OnDodge;
        }

        public override void Exit()
        {
            stateMachine.inputReader.lockTarget -= OnLock;
            stateMachine.inputReader.dodgeEvent -= OnDodge;
            stateMachine.inputReader.jumpEvent -= Jump;
        }

        private void OnDodge()
        {
            if(stateMachine.inputReader.movementValue == Vector2.zero) { return; }

            stateMachine.SwitchState(new PlayerDodgingState(stateMachine,stateMachine.inputReader.movementValue));
        }

        private void OnLock()
        {
            stateMachine.targeter.Unlock();
            stateMachine.SwitchState(new PlayerFreelookState(stateMachine));
        }
        private void Jump()
        {
            stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
        }
        private Vector3 CalculateMovement(float deltaTime)
        {
            Vector3 movement = new Vector3();

            movement += stateMachine.transform.right * stateMachine.inputReader.movementValue.x;
            movement += stateMachine.transform.forward * stateMachine.inputReader.movementValue.y;

            return movement;
        }

        public override void Tick(float deltaTime)
        {
            if (stateMachine.inputReader.isAttacking)
            {
                stateMachine.SwitchState(new AttackingState(stateMachine,0));
                return;
            }
            if (stateMachine.inputReader.isBlocking)
            {
                stateMachine.SwitchState(new PlayerBlockingState(stateMachine));
                return;
            }
            if(stateMachine.targeter.currentTarget == null)
            {
                stateMachine.SwitchState(new PlayerFreelookState(stateMachine));
                return;
            }
            Vector3 movement = CalculateMovement(deltaTime);

            Move(movement * stateMachine.targetingMovementSpeed, deltaTime);

            UpdateAnimator(deltaTime);

            FaceTarget();
        }

        private void UpdateAnimator(float deltaTime)
        {  
            if(stateMachine.inputReader.movementValue.x == 0)
            {
                stateMachine.playerAnimator.SetFloat(targetingForwardHash,0, 0.01f, deltaTime);
            } else
            {
                float movementMagnitude = stateMachine.inputReader.movementValue.x > 0 ? 1f : -1f;
                stateMachine.playerAnimator.SetFloat(targetingForwardHash, movementMagnitude, 0.01f, deltaTime);
            }
            if (stateMachine.inputReader.movementValue.y == 0)
            {
                stateMachine.playerAnimator.SetFloat(targetingRightHash, 0, 0.01f, deltaTime);
            }
            else
            {
                float movementMagnitude = stateMachine.inputReader.movementValue.y > 0 ? 1f : -1f;
                stateMachine.playerAnimator.SetFloat(targetingRightHash, movementMagnitude, 0.01f, deltaTime);

            }
          
        }
    }
}