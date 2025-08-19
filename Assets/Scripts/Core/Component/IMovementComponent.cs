using UnityEngine;

namespace ZRCore.Comp
{
    public interface IMovementComponent
    {
        float MoveSpeed { get; }
        void Init(float moveSpeed);
        void Move(Vector2 direction);
    }
}
