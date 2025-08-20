using System;
using UnityEngine;

namespace ZRCore.Comp
{
    public interface IInputComponent
    {
        event Action<Vector2> OnMoveInput;
        event Action<bool> OnSprintInput;
        event Action OnJumpInput;

        void Init(float movementSpeed);
    }
}
