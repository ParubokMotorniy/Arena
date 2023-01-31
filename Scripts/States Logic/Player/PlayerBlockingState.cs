using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.StatesLogic 
{
    public class PlayerBlockingState : PlayerBaseState
    {
        private readonly int blockingAnimationHash = Animator.StringToHash("PlayerBlock");
        private const float crossfadeDuration = 0.05f;

        public PlayerBlockingState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            stateMachine.playerAnimator.CrossFadeInFixedTime(blockingAnimationHash,crossfadeDuration);
            stateMachine.health.SetInvulnerable(true);
        }

        public override void Exit()
        {
            stateMachine.health.SetInvulnerable(false);
        }

        public override void Tick(float deltaTime)
        {
            Move(deltaTime);

            if (!stateMachine.inputReader.isBlocking)
            {
                stateMachine.SwitchState(new TargetingState(stateMachine));
                return;
            }
            if(stateMachine.targeter.currentTarget == null)
            {
                stateMachine.SwitchState(new PlayerFreelookState(stateMachine));
                return;
            }
        }
    }
}

