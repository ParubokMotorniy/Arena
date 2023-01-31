using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.StatesLogic
{
    public class EnemyAttackingState : EnemyBaseState
    {
        private readonly int attackAnimationHash = Animator.StringToHash("Attack");
        private const float crossFadeDuration = 0.075f;
        public EnemyAttackingState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine){}

        public override void Enter()
        {
            FacePlayer();

            enemyStateMachine.weapon.SetAttack(enemyStateMachine.attackDamage,enemyStateMachine.attackKnockback);
            enemyStateMachine.enemyAnimator.CrossFadeInFixedTime(attackAnimationHash, crossFadeDuration);
        }

        public override void Exit()
        {

        }

        public override void Tick(float deltaTime)
        {
            if (GetNormalizedTime(enemyStateMachine.enemyAnimator,"Attack") >= 1)
            {
                enemyStateMachine.SwitchState(new EnemyChasingState(enemyStateMachine));
            }
        }
    }
}

