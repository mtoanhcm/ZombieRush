using System;

namespace ZRCore.Comp
{
    public interface IHealthComponent
    {
        event Action OnDeath;
        float MaxHealth { get; }
        float CurrentHealth { get; }
        void Init(float maxHealth);
        void TakeDamage(int damage);
        void RestoreHealth(int health);
    }
}
