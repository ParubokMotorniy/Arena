using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.StatesLogic
{
    public class PlayerDodgingState : PlayerBaseState
    {
        private readonly int dodgingBlendTreeHash = Animator.StringToHash("Dodging");
        private readonly int dodgingForwardHash = Animator.StringToHash("DodgeForward");
        private readonly int dodgingRightHash = Animator.StringToHash("DodgeRight");

        private Vector3 dodgingDirectionInput;
        private float remainingDodgeTime;
        private const float croosFadeDuration = 0.03f;
        public PlayerDodgingState(PlayerStateMachine stateMachine, Vector3 dodgingDirectionInput) : base(stateMachine)
        {
            this.dodgingDirectionInput = dodgingDirectionInput;
        }

        public override void Enter()
        {
            remainingDodgeTime = stateMachine.dodgeDuration;

            stateMachine.playerAnimator.SetFloat(dodgingForwardHash,dodgingDirectionInput.y);
            stateMachine.playerAnimator.SetFloat(dodgingRightHash, dodgingDirectionInput.x);
            stateMachine.playerAnimator.CrossFadeInFixedTime(dodgingBlendTreeHash,croosFadeDuration);

            stateMachine.health.SetInvulnerable(true);
        }

        public override void Exit()
        {
            stateMachine.health.SetInvulnerable(false);
        }

        public override void Tick(float deltaTime)
        {
            Vector3 movement = new Vector3();
            float speed = stateMachine.dodgeLength / stateMachine.dodgeDuration;

            movement += stateMachine.transform.right * dodgingDirectionInput.x * speed;
            movement += stateMachine.transform.forward * dodgingDirectionInput.y * speed;

            Move(movement, deltaTime);
            FaceTarget();

            remainingDodgeTime -= deltaTime;
            if(remainingDodgeTime <= 0)
            {
                stateMachine.SwitchState(new TargetingState(stateMachine));
            }
        }
    }
}

