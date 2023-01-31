using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.StatesLogic
{
    public class PlayerPullUpState : PlayerBaseState
    {
        private readonly int pullingUpHash = Animator.StringToHash("PullUp");
        private const float croosFadeDuration = 0.075f;
        public PlayerPullUpState(PlayerStateMachine stateMachine) : base(stateMachine){}
        public override void Enter()
        {
            stateMachine.playerAnimator.CrossFadeInFixedTime(pullingUpHash,croosFadeDuration);
        }

        public override void Exit()
        {
            stateMachine.forceReceiver.ResetForces();
            stateMachine.characterController.Move(Vector3.zero);
        }

        public override void Tick(float deltaTime)
        {
            if(GetNormalizedTime(stateMachine.playerAnimator,"Climbing") < 1) { return; }

            stateMachine.characterController.enabled = false;
            stateMachine.transform.Translate(new Vector3(0f, /*3.685311f*/3.5f, /*0.839717f*/1.5f),Space.Self);
            stateMachine.characterController.enabled = true;

            stateMachine.SwitchState(new PlayerFreelookState(stateMachine,false));
        }
    }
}

