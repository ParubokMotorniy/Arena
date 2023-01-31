using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.StatesLogic
{
    public class PlayerImpactState : PlayerBaseState
    {
        private readonly int impactAnimationHash = Animator.StringToHash("HitImpact");
        private const float crossFadeDuration = 0.075f;
        private float duration = 1;
        public PlayerImpactState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            stateMachine.playerAnimator.CrossFadeInFixedTime(impactAnimationHash,crossFadeDuration);
        }

        public override void Exit()
        {
        }

        public override void Tick(float deltaTime)
        {
            Move(deltaTime);

            duration -= deltaTime;

            if(duration <= 0)
            {
                ReturnToLocomotion();
            }
        }
    }
}

