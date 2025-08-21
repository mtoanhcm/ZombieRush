using UnityEngine;

namespace ZRCore
{
    public interface IAnimationComponent
    {
        void OnSpeedChanged(Vector2 moveDirect);
    }
}
