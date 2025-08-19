using UnityEngine;
using ZRCharacter.Config;
using ZRCore.Character;
using ZRCore.Comp;

namespace ZRCharacter
{
    public class Character : MonoBehaviour, ICharacter
    {
        public IHealthComponent HealthComponent { get; private set; }
        public IMovementComponent MovementComponent { get; private set; }

        public CharacterID ID => config.ID;

        private CharacterConfig config;

        public void Init(CharacterConfig config)
        {
            this.config = config;

            InitComponent();
            ListenerEvents();
        }

        private void InitComponent()
        {
            if(TryGetComponent<IHealthComponent>(out var healthComp))
            {
                HealthComponent = healthComp;
                HealthComponent.Init(config.Health);
            }

            if (TryGetComponent<IMovementComponent>(out var movementComp))
            {
                MovementComponent = movementComp;
                MovementComponent.Init(config.MoveSpeed);
            }
            
        }

        private void ListenerEvents()
        {
            HealthComponent.OnDeath -= OnCharacterDeath;
            HealthComponent.OnDeath += OnCharacterDeath;
        }

        private void OnCharacterDeath()
        {
            CharacterFactory.DespawnCharacter(this);
        }
    }
}
