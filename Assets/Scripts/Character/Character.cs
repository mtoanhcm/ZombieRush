using System;
using UnityEngine;
using ZRCharacter.Config;
using ZRCore;
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
        public IAnimationComponent AnimationComponent { get; private set; }
        public IColliderDetectComponent ColliderDetectorComponent { get; private set; }

        public CharacterID ID => config.ID;

        public GameObject CharacterObject => gameObject;

        private CharacterConfig config;
        private bool isInit;

        private void OnDisable()
        {
            if (!isInit)
            {
                return;
            }

            isInit = false;
            RemoveListener();
        }

        public void Init(CharacterConfig config)
        {
            this.config = config;

            InitComponent();
            ListenerEvents();

            isInit = true;
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

            if (TryGetComponent<IAnimationComponent>(out var animComp))
            {
                AnimationComponent = animComp;
            }

            if(TryGetComponent<IColliderDetectComponent>(out var colliderDetectComp))
            {
                ColliderDetectorComponent = colliderDetectComp;
            }

        }

        private void ListenerEvents()
        {
            
            HealthComponent.OnDeath += OnCharacterDeath;

            InputComponent.OnMoveInput += MovementComponent.Move;
            InputComponent.OnJumpInput += MovementComponent.Jump;
            InputComponent.OnSprintInput += MovementComponent.Sprint;

            InputComponent.OnMoveInput += AnimationComponent.OnSpeedChanged;

            if(ColliderDetectorComponent != null)
            {
                ColliderDetectorComponent.OnCollisionEnterDetect += OnColliderWithEnemy;
                ColliderDetectorComponent.CollisionEnterCondition = CheckIsColliderWithEnemy;
            }
            

            GameplayEvent.OnGetWinKey += CheckKillCharacter;
            GameplayEvent.OnGameover += StopAllActivity;
        }

        private void RemoveListener()
        {
            HealthComponent.OnDeath -= OnCharacterDeath;

            InputComponent.OnMoveInput -= MovementComponent.Move;
            InputComponent.OnJumpInput -= MovementComponent.Jump;
            InputComponent.OnSprintInput -= MovementComponent.Sprint;

            if(ColliderDetectorComponent!= null)
            {
                ColliderDetectorComponent.OnCollisionEnterDetect += OnColliderWithEnemy;
                ColliderDetectorComponent.CollisionEnterCondition = null;
            }
            

            GameplayEvent.OnGetWinKey -= CheckKillCharacter;
            GameplayEvent.OnGameover -= StopAllActivity;
        }

        private void StopAllActivity(bool obj)
        {
            MovementComponent.SetCanMove(false);
        }

        private void OnColliderWithEnemy(Collision collision)
        {
            HealthComponent.TakeDamage(9999);
        }

        private bool CheckIsColliderWithEnemy(Collision collision)
        {
            return collision.gameObject.tag.Equals("Enemy");
        }

        private void CheckKillCharacter()
        {
            if(gameObject == null)
            {
                return;
            }

            if (gameObject.tag.Equals("Player"))
            {
                return;
            }

            HealthComponent.TakeDamage(9999);
        }

        private void OnCharacterDeath()
        {
            if (gameObject.tag.Equals("Player"))
            {
                GameplayEvent.TriggerGameover(isWin: false);
            }

            RemoveListener();
            MovementComponent.SetCanMove(false);

            CharacterFactory.DespawnCharacter(this);
        }
    }
}
