using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.StatesLogic
{
    public class EnemyDeadState : EnemyBaseState
    {
        public EnemyDeadState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
        {
        }

        public override void Enter()
        {
            enemyStateMachine.ragdoll.ToggleRagdoll(true);

            enemyStateMachine.weapon.gameObject.SetActive(false);
            GameObject.Destroy(enemyStateMachine.target);
        }

        public override void Exit()
        {
        }

        public override void Tick(float deltaTime)
        {
        }
    }
}

