using System;
using UnityEngine;
using ZRAI.Behaviour;
using ZRCore.Comp;
using ZRCore.Context;

namespace ZRComponent
{
    public class BOTInputComponent : MonoBehaviour, IInputComponent
    {
        public event Action<Vector2> OnMoveInput;
        public event Action<bool> OnSprintInput;
        public event Action OnJumpInput;

        private SteeringBehaviour steeringBehaviour;
        private Transform targetObject;

        private float DELAY_FIND_PATH = 0.1f;
        private float tempDelayFindPathTime;

        public void Init(float movementSpeed)
        {
            steeringBehaviour = new SteeringBehaviour(transform, movementSpeed);
            if (SceneContext.MainCharacter != null)
            {
                targetObject = SceneContext.MainCharacter.CharacterObject.transform;
            }
        }

        private void Update()
        {
            if(targetObject == null)
            {
                return;
            }

            if(tempDelayFindPathTime > Time.time)
            {
                return;
            }

            var direction = steeringBehaviour.SeekToTarget(targetObject.position, false, false).normalized;

            OnMoveInput?.Invoke(new Vector2(direction.x, direction.z));

            tempDelayFindPathTime = Time.time + DELAY_FIND_PATH;
        }
    }
}
