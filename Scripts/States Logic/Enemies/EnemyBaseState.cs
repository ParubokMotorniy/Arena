using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.StatesLogic {
    public abstract class EnemyBaseState : State
    {
        protected EnemyStateMachine enemyStateMachine;
        public EnemyBaseState(EnemyStateMachine enemyStateMachine)
        {
            this.enemyStateMachine = enemyStateMachine;
        }

        protected void Move(Vector3 motion, float deltaTime)
        {
            enemyStateMachine.enemyController.Move((motion + enemyStateMachine.forceReceiver.movement) * deltaTime);
        }
        protected void Move(float deltaTime)
        {
            enemyStateMachine.enemyController.Move(enemyStateMachine.forceReceiver.movement * deltaTime);
        }
        protected void FacePlayer()
        {
            if (enemyStateMachine.player == null) { return; }

            Vector3 lookPos = enemyStateMachine.player.transform.position - enemyStateMachine.transform.position;
            lookPos.y = 0;

            enemyStateMachine.transform.rotation = Quaternion.LookRotation(lookPos);
        }
        protected bool IsInChaseRange()
        {
            if (enemyStateMachine.player.isDead) { return false; }

            float distance = (enemyStateMachine.player.transform.position - enemyStateMachine.transform.position).sqrMagnitude;
            return distance <= (enemyStateMachine.playerDetectionRange*enemyStateMachine.playerDetectionRange);
        }
    }
}


