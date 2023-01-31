using UnityEngine;

namespace RPG.StatesLogic
{
    public class PlayerFreelookState : PlayerBaseState
    {
        private readonly int freelookSpeedHash = Animator.StringToHash("freelookSpeed");
        private readonly int freelookBlendTreeHash = Animator.StringToHash("Freelook blend tree");
        private const float animatorDampTime = 0.05f;
        private const float crossFadeDuration = 0.1f;
        private bool shouldFade;
        public PlayerFreelookState(PlayerStateMachine stateMachine, bool shouldFade = true) : base(stateMachine)
        {
            this.shouldFade = shouldFade;
        }
        
        public override void Enter()
        {
            stateMachine.playerAnimator.SetFloat(freelookSpeedHash,0);

            if (shouldFade)
            {
                stateMachine.playerAnimator.CrossFadeInFixedTime(freelookBlendTreeHash, crossFadeDuration);

            }
            else
            {
                stateMachine.playerAnimator.Play(freelookBlendTreeHash);
            }
            stateMachine.inputReader.lockTarget += Lock;
            stateMachine.inputReader.jumpEvent += Jump;
        }

        public override void Exit()
        {
            stateMachine.inputReader.lockTarget -= Lock;
            stateMachine.inputReader.jumpEvent -= Jump;
        }

        private void Lock()
        {
            if (!stateMachine.targeter.SelectTarget()) { return; }
            stateMachine.SwitchState(new TargetingState(stateMachine));
        }
        private void Jump()
        {
            stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
        }

        public override void Tick(float deltaTime)
        {
            if (stateMachine.inputReader.isAttacking)
            {
                stateMachine.SwitchState(new AttackingState(stateMachine,0));
                return;
            }

            Vector3 movement = CalculateMovement();

            //stateMachine.characterController.Move(movement * deltaTime * stateMachine.freelookMovementSpeed);
            Move(movement * stateMachine.freelookMovementSpeed, Time.deltaTime);

            if(stateMachine.inputReader.movementValue == Vector2.zero) 
            {
                stateMachine.playerAnimator.SetFloat(freelookSpeedHash, 0,animatorDampTime,deltaTime);
                return; 
            }

            stateMachine.playerAnimator.SetFloat(freelookSpeedHash, 1, animatorDampTime, deltaTime);
            FaceMovementDirection(movement, deltaTime);
        }
        private Vector3 CalculateMovement()
        {
            Vector3 cameraForward = stateMachine.mainCameraTransform.forward;
            Vector3 cameraRight = stateMachine.mainCameraTransform.right;

            cameraForward.y = 0;
            cameraRight.y = 0;

            cameraForward.Normalize();
            cameraRight.Normalize();

            return cameraForward * stateMachine.inputReader.movementValue.y + cameraRight * stateMachine.inputReader.movementValue.x;
        }
        private void FaceMovementDirection(Vector3 movementDirection,float deltaTime)
        {
            stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation,Quaternion.LookRotation(movementDirection),deltaTime * stateMachine.rotationDamping);
        }
    }
}

