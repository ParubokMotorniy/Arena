using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using UnityEngine;

namespace RPG.StatesLogic
{
    public class AttackingState : PlayerBaseState
    {
        private Attack currentAttack;
        private float previousFrameTime;
        private bool alreadyAppliedForce = false;
        public AttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
        {
            if(attackIndex >= 0)
            {
                currentAttack = stateMachine.attacks[attackIndex];
            }
        }

        public override void Enter()
        {
            stateMachine.weaponDamage.SetAttack(currentAttack.attackDamage,currentAttack.knockback);
            stateMachine.playerAnimator.CrossFadeInFixedTime(currentAttack.animationName, currentAttack.transitionDuration);
        }

        public override void Exit()
        {
        }

        public override void Tick(float deltaTime)
        {
            Move(Time.deltaTime);
            FaceTarget();

            float normalizedTime = GetNormalizedTime(stateMachine.playerAnimator, "Attack");

            if (normalizedTime >= previousFrameTime && normalizedTime < 1)
            {
                if (normalizedTime >= currentAttack.forceTime)
                {
                    TryApplyForce();
                }

                if (stateMachine.inputReader.isAttacking)
                {
                    TryComboAttack(normalizedTime);
                }
            }
            else
            {
                if(stateMachine.targeter != null)
                {
                    stateMachine.SwitchState(new TargetingState(stateMachine));
                }
                else
                {
                    stateMachine.SwitchState(new PlayerFreelookState(stateMachine));
                }
            }

            previousFrameTime = GetNormalizedTime(stateMachine.playerAnimator, "Attack");
        }

        private void TryComboAttack(float normalizedTime)
        {
            if(currentAttack.comboStateIndex == -1) { return; }

            if(normalizedTime < currentAttack.comboAttackTime) { return; }

            stateMachine.SwitchState(new AttackingState(stateMachine, currentAttack.comboStateIndex));
        }
        private void TryApplyForce()
        {
            if(alreadyAppliedForce) { return; }
            stateMachine.forceReceiver.AddForce(stateMachine.transform.forward * currentAttack.attackForce);
            alreadyAppliedForce = true;
        }
    }
}

