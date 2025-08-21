using System;
using UnityEngine;

namespace ZRCore.Comp
{
    public interface IColliderDetectComponent
    {
        /// <summary>
        /// Event detect collider for non-trigger check
        /// </summary>
        event Action<Collision> OnCollisionEnterDetect;

        /// <summary>
        /// Event detect collider for trigger check
        /// </summary>
        event Action<Collider> OnColliderEnterDetect;

        /// <summary>
        /// Condition for detecting trigger collider
        /// </summary>
        Func<Collider, bool> ColliderEnterCondition { get; set; }

        /// <summary>
        /// Condition for detecting non-trigger collider
        /// </summary>
        Func<Collision, bool> CollisionEnterCondition { get; set; }
    }
}
