using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Constitution
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 100;
        private int currentHealth;
        public event Action onTakeDamage;
        public event Action onDie;

        private bool isInvulnerable;
        public bool isDead => currentHealth == 0;
        // Start is called before the first frame update
        private void Start()
        {
            currentHealth = maxHealth;
        }
        public void DealDamage(int damage)
        {
            if (isInvulnerable) { return; }

            if(currentHealth > 0)
            {
                currentHealth = Mathf.Max(currentHealth - damage,0);

                Debug.Log(currentHealth);

                onTakeDamage?.Invoke();
            } 
            if(currentHealth <= 0)
            {
                onDie?.Invoke();
            }
        }
        public void SetInvulnerable(bool isInvulnerable)
        {
            this.isInvulnerable = isInvulnerable;
        }
    }
}

