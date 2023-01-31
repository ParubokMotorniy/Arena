using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Ragdoll : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private CharacterController controller;

        private Collider[] colliders;
        private Rigidbody[] rigidbodies;
        // Start is called before the first frame update
        void Awake()
        {
            colliders = GetComponentsInChildren<Collider>(true);
            rigidbodies = GetComponentsInChildren<Rigidbody>(true);

            ToggleRagdoll(false);
        }
        public void ToggleRagdoll(bool isRagdoll)
        {
            foreach(Collider collider in colliders)
            {
                if(collider.gameObject.tag == "Ragdoll")
                {
                    collider.enabled = isRagdoll;
                }
            }
            foreach (Rigidbody rigidbody in rigidbodies)
            {
                if (rigidbody.gameObject.tag == "Ragdoll")
                {
                    rigidbody.isKinematic = !isRagdoll;
                    rigidbody.useGravity = isRagdoll;
                }
            }
            controller.enabled = !isRagdoll;
            animator.enabled = !isRagdoll;
        }
    }
}

