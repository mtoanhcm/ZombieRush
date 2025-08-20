using System;
using UnityEngine;
using ZRCore.Comp;
using ZRPlayerInput;

namespace ZRComponent
{
    public class PlayerInputComponent : MonoBehaviour, IInputComponent
    {
        public event Action<Vector2> OnMoveInput;
        public event Action<bool> OnSprintInput;
        public event Action OnJumpInput;

        private CharacterInput characterInput;

        private void Awake()
        {
            characterInput = new CharacterInput();
        }

        private void OnEnable()
        {
            characterInput.OnMoveInput += HandleMoveInput;
            characterInput.OnSprintInput += HandleSprintInput;
            characterInput.OnJumpInput += HandleJumpInput;

            characterInput.Enable();
        }

        private void OnDisable()
        {
            characterInput.OnMoveInput -= HandleMoveInput;
            characterInput.OnSprintInput -= HandleSprintInput;
            characterInput.OnJumpInput -= HandleJumpInput;

            characterInput.Disable();
        }

        private void OnDestroy()
        {
            characterInput = null;
        }

        private void HandleMoveInput(Vector2 input)
        {
            OnMoveInput?.Invoke(input);
        }

        private void HandleSprintInput(bool isSpinning)
        {
            OnSprintInput?.Invoke(isSpinning);
        }

        private void HandleJumpInput()
        {
            OnJumpInput?.Invoke();
        }

        public void Init(float movementSpeed)
        {
            
        }
    }
}
