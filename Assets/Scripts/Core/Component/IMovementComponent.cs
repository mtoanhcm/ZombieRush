using UnityEngine;

namespace ZRCore.Comp
{
    public interface IMovementComponent
    {
        float MoveSpeed { get; }
        float SprintSpeed { get; }
        float JumpForce { get; }
        void Init(float moveSpeed, float sprintSpeed, float jumpForce);
        void Move(Vector2 direction);
        void SetCanMove(bool canMove);
        void Jump();
        void Sprint(bool isSpinning);
    }
}
