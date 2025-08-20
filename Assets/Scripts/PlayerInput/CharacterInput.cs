using System;
using UnityEngine;

namespace ZRPlayerInput
{
    public class CharacterInput
    {
        public event Action<Vector2> OnMoveInput;
        public event Action OnJumpInput;
        public event Action<bool> OnSprintInput;

        private PlayerInput playerInput;

        public CharacterInput()
        {
            playerInput = new PlayerInput();
        }

        public void Enable()
        {
            playerInput.Player.Move.performed += HandleMoveInput;
            playerInput.Player.Move.canceled += HandleMoveInput;

            playerInput.Player.Jump.performed += HandleJumpInput;

            playerInput.Player.Sprint.performed += HandleSpintInput;
            playerInput.Player.Sprint.canceled += HandleSpintInput;

            playerInput.Enable();
        }

        public void Disable() 
        {
            playerInput.Player.Move.performed -= HandleMoveInput;
            playerInput.Player.Move.canceled -= HandleMoveInput;

            playerInput.Player.Jump.performed -= HandleJumpInput;

            playerInput.Player.Sprint.performed -= HandleSpintInput;
            playerInput.Player.Sprint.canceled -= HandleSpintInput;

            playerInput.Disable();
        }

        private void HandleSpintInput(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            OnSprintInput?.Invoke(context.ReadValue<float>() > 0);
        }

        private void HandleJumpInput(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            OnJumpInput?.Invoke();
        }

        private void HandleMoveInput(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            OnMoveInput?.Invoke(context.ReadValue<Vector2>());
        }
    }
}
