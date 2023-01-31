using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.StatesLogic
{
    public class EnemyChasingState : EnemyBaseState
    {
        private readonly int locomotionHash = Animator.StringToHash("Locomotion");
        private readonly int speedHash = Animator.StringToHash("Speed");
        private const float crossFadeDuration = 0.1f;
        private const float animatorDampTime = 0.1f;
        public EnemyChasingState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
        {
        }

        public override void Enter()
        {
            enemyStateMachine.enemyAnimator.CrossFadeInFixedTime(locomotionHash, crossFadeDuration);
        }

        public override void Exit()
        {
            enemyStateMachine.agent.ResetPath();
            enemyStateMachine.agent.velocity = Vector3.zero;
        }

        public override void Tick(float deltaTime)
        {
            if (!IsInChaseRange())
            {
                enemyStateMachine.SwitchState(new EnemyIdleState(enemyStateMachine));
                return;
            }
            if (IsInAttackRange())
            {
                enemyStateMachine.SwitchState(new EnemyAttackingState(enemyStateMachine));
                return;
            }

            MoveToPlayer(deltaTime);

            FacePlayer();

            enemyStateMachine.enemyAnimator.SetFloat(speedHash, 1, animatorDampTime, deltaTime);

        }
        private void MoveToPlayer(float deltaTime)
        {
            if (enemyStateMachine.agent.isOnNavMesh)
            {
                enemyStateMachine.agent.destination = enemyStateMachine.player.transform.position;

                Move(enemyStateMachine.agent.desiredVelocity.normalized * enemyStateMachine.movementSpeed, deltaTime);
            }

            enemyStateMachine.agent.velocity = enemyStateMachine.enemyController.velocity;
        }
        protected bool IsInAttackRange()
        {
            if (enemyStateMachine.player.isDead) { return false; }

            float distance = (enemyStateMachine.player.transform.position - enemyStateMachine.transform.position).sqrMagnitude;
            return distance <= (enemyStateMachine.playerAttackRange * enemyStateMachine.playerAttackRange);
        }
    }
}

