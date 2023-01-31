using System.Collections;
using System.Collections.Generic;
using RPG.Constitution;
using RPG.GamePhysics;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponDamage : MonoBehaviour
    {
        [SerializeField] private Collider mainCollider;
        private List<Collider> alreadyCollidedWith = new List<Collider>();
        private int damage;
        private float knockback;
        private void OnEnable()
        {
            alreadyCollidedWith.Clear();
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other == mainCollider) { return; }

            if (alreadyCollidedWith.Contains(other)) { return; }

            alreadyCollidedWith.Add(other);

            if(other.TryGetComponent<Health>(out Health health))
            {
                health.DealDamage(damage);
            }
            if(other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
            {
                Vector3 direction = (other.transform.position - mainCollider.transform.position).normalized;
                forceReceiver.AddForce(direction * knockback);
            }
        }
        public void SetAttack(int damage,float knockback)
        {
            this.damage = damage;
            this.knockback = knockback;
        }
    }
}

