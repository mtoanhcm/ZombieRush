using System;
using UnityEngine;
using ZRCharacter.Config;
using ZRCore.Character;
using ZRCore.Comp;
using ZREvent;

namespace ZRCharacter
{
    public class Character : MonoBehaviour, ICharacter
    {
        public IHealthComponent HealthComponent { get; private set; }
        public IMovementComponent MovementComponent { get; private set; }
        public IInputComponent InputComponent { get; private set; }

        public CharacterID ID => config.ID;

        public GameObject CharacterObject => gameObject;

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
                MovementComponent.Init(config.MoveSpeed, config.SprintSpeed, config.JumForce);
            }

            if (TryGetComponent<IInputComponent>(out var inputComp))
            {
                InputComponent = inputComp;
                InputComponent.Init(config.MoveSpeed);
            }

        }

        private void ListenerEvents()
        {
            
            HealthComponent.OnDeath += OnCharacterDeath;

            if (InputComponent != null) {
                InputComponent.OnMoveInput += MovementComponent.Move;
                InputComponent.OnJumpInput += MovementComponent.Jump;
                InputComponent.OnSprintInput += MovementComponent.Sprint;
            }

            GameplayEvent.OnGetWinKey += CheckKillCharacter;
        }

        private void RemoveListener()
        {
            HealthComponent.OnDeath -= OnCharacterDeath;
            if (InputComponent != null)
            {
                InputComponent.OnMoveInput -= MovementComponent.Move;
                InputComponent.OnJumpInput -= MovementComponent.Jump;
                InputComponent.OnSprintInput -= MovementComponent.Sprint;
            }

            GameplayEvent.OnGetWinKey -= CheckKillCharacter;
        }

        private void CheckKillCharacter()
        {
            if (gameObject.tag.Equals("Player"))
            {
                return;
            }

            OnCharacterDeath();
        }

        private void OnCharacterDeath()
        {
            RemoveListener();
            MovementComponent.SetCanMove(false);

            CharacterFactory.DespawnCharacter(this);
        }
    }
}
