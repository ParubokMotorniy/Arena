using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.GamePhysics
{
    public class ForceReceiver : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private float drag;
        [SerializeField] private NavMeshAgent agent;
        private float verticalVelocity;
        private Vector3 impact;
        private Vector3 dampingVelocity;
        public Vector3 movement => impact + Vector3.up * verticalVelocity;
        void Update()
        {
            if (verticalVelocity < 0 && characterController.isGrounded)
            {
                verticalVelocity = Physics.gravity.y * Time.deltaTime;
            } else
            {
                verticalVelocity += Physics.gravity.y * Time.deltaTime;
            }
            impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity,drag);
            if(impact.sqrMagnitude <= (0.2f * 0.2f)  && agent != null)
            {
                agent.enabled = true;
            }
        }
        public void AddForce(Vector3 force)
        {
            impact += force;
            if(agent != null)
            {
                agent.enabled = false;
            }
        }

        internal void ResetForces()
        {
            verticalVelocity = 0;
            impact = Vector3.zero;
        }

        public void Jump(float jumpForce)
        {
            verticalVelocity += jumpForce;
        }
    }
}

