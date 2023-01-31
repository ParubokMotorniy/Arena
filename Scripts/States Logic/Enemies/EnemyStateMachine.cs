using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Constitution;
using RPG.GamePhysics;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.StatesLogic
{
    public class EnemyStateMachine : StateMachine
    {
        [field: SerializeField] public Animator enemyAnimator { get; private set; }
        [field: SerializeField] public float playerDetectionRange { get; private set; }
        [field: SerializeField] public float playerAttackRange { get; private set; }
        [field: SerializeField] public float movementSpeed { get; private set; }
        [field: SerializeField] public float attackKnockback { get; private set; }
        [field: SerializeField] public int attackDamage { get; private set; }
        [field: SerializeField] public CharacterController enemyController { get; private set; }
        [field: SerializeField] public ForceReceiver forceReceiver { get; private set; }
        [field: SerializeField] public NavMeshAgent agent { get; private set; }
        [field: SerializeField] public WeaponDamage weapon { get; private set; }
        [field: SerializeField] public Health health { get; private set; }
        [field: SerializeField] public Target target { get; private set; }
        [field: SerializeField] public Ragdoll ragdoll { get; private set; }

        public Health player { get; private set; }
        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

            agent.updatePosition = false;
            agent.updateRotation = false;

            SwitchState(new EnemyIdleState(this));
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
            SwitchState(new EnemyDeadState(this));
        }

        private void HandleTakeDamage()
        {
            SwitchState(new EnemyImpactState(this));
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, playerDetectionRange);
        }
    }
}

