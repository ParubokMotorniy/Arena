using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.StatesLogic
{
    public class EnemyImpactState : EnemyBaseState
    {
        private readonly int impactAnimationHash = Animator.StringToHash("HitImpact");
        private const float crossFadeDuration = 0.075f;
        private float duration = 1;
        public EnemyImpactState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
        {
        }

        public override void Enter()
        {
            enemyStateMachine.enemyAnimator.CrossFadeInFixedTime(impactAnimationHash,crossFadeDuration);
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
                enemyStateMachine.SwitchState(new EnemyIdleState(enemyStateMachine));
            }
        }
    }
}

