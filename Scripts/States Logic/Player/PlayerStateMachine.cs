using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Constitution;
using RPG.GamePhysics;
using UnityEngine;

namespace RPG.StatesLogic {
    public class PlayerStateMachine : StateMachine
    {
        [field: SerializeField] public InputReader inputReader { get; private set;}
        [field: SerializeField] public CharacterController characterController { get; private set; }
        [field: SerializeField] public float freelookMovementSpeed { get; private set; }
        [field: SerializeField] public float targetingMovementSpeed { get; private set; }
        [field: SerializeField] public float rotationDamping { get; private set; }
        [field: SerializeField] public float dodgeDuration { get; private set; }
        [field: SerializeField] public float dodgeLength { get; private set; }
        [field: SerializeField] public float jumpForce { get; private set; }
        [field: SerializeField] public LedgeDetector ledgeDetector { get; private set; }
        [field: SerializeField] public Animator playerAnimator { get; private set; }
        [field: SerializeField] public Targeter targeter { get; private set; }
        [field: SerializeField] public ForceReceiver forceReceiver { get; private set; }
        [field: SerializeField] public Attack[] attacks { get; private set; }
        [field: SerializeField] public WeaponDamage weaponDamage { get; private set; }
        [field: SerializeField] public Health health { get; private set; }
        [field: SerializeField] public Ragdoll ragdoll { get; private set; }


        public Transform mainCameraTransform { get; private set; }
        public float previousDodgeTime { get; private set; } = Mathf.NegativeInfinity;
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            mainCameraTransform = Camera.main.transform;
            SwitchState(new PlayerFreelookState(this));
        }

        private void OnEnable()
        {
            health.onTakeDamage += HandleTakeDamage;
            health.onDie += HandleDeath;
        }
        private void OnDisable()
        {
            health.onTakeDamage -= HandleTakeDamage;
            health.onDie -= HandleDeath;
        }

        private void HandleDeath()
        {
            SwitchState(new PlayerDeadState(this));
        }

        private void HandleTakeDamage()
        {
            SwitchState(new PlayerImpactState(this));
        }
    }
}


