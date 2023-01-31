using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.StatesLogic
{
    public class EnemyIdleState : EnemyBaseState
    {
        private readonly int locomotionHash = Animator.StringToHash("Locomotion");
        private readonly int speedHash = Animator.StringToHash("Speed");
        private const float crossFadeDuration = 0.1f;
        private const float animatorDampTime = 0.1f;
        public EnemyIdleState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
        {
            
        }

        public override void Enter()
        {          
            enemyStateMachine.enemyAnimator.CrossFadeInFixedTime(locomotionHash,crossFadeDuration);
        }

        public override void Exit()
        {
            
        }

        public override void Tick(float deltaTime)
        {
            Move(deltaTime);

            if (IsInChaseRange())
            {
                Debug.Log("In Range!");

                enemyStateMachine.SwitchState(new EnemyChasingState(enemyStateMachine));

                return;
            }
            enemyStateMachine.enemyAnimator.SetFloat(speedHash, 0,animatorDampTime,deltaTime);
        }
    }
}

