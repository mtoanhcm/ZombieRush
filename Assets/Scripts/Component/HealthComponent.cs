using System;
using UnityEngine;
using ZRCore.Comp;

namespace ZRComponent
{
    public class HealthComponent : MonoBehaviour, IHealthComponent
    {
        public float MaxHealth { get; private set; }

        public float CurrentHealth { get; private set; }

        public event Action OnDeath;

        public void Init(float maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }

        public void RestoreHealth(int health)
        {
            CurrentHealth = Mathf.Min(CurrentHealth + health, MaxHealth);
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
            if (CurrentHealth <= 0) { 
                OnDeath?.Invoke();
            }
        }
    }
}
