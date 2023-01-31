using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.StatesLogic
{
    public class PlayerDeadState : PlayerBaseState
    {
        public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }
        public override void Enter()
        {
            stateMachine.ragdoll.ToggleRagdoll(true);

            stateMachine.weaponDamage.gameObject.SetActive(false);
        }

        public override void Exit()
        {
        }

        public override void Tick(float deltaTime)
        {
        }
    }
}
