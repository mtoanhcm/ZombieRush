using System;
using UnityEngine;

namespace ZRUtility
{
    public class ColliderDetector : MonoBehaviour
    {
        /// <summary>
        /// Event detect collider for non-trigger check
        /// </summary>
        public event Action<Collision> OnCollisionEnterDetect;

        /// <summary>
        /// Event detect collider for trigger check
        /// </summary>
        public event Action<Collider> OnColliderEnterDetect;

        /// <summary>
        /// Condition for detecting trigger collider
        /// </summary>
        public Func<Collider, bool> ColliderEnterCondition;

        /// <summary>
        /// Condition for detecting non-trigger collider
        /// </summary>
        public Func<Collision, bool> CollisionEnterCondition;

        private void OnCollisionEnter(Collision collision)
        {
            bool isValidCollision = false;
            if (CollisionEnterCondition != null) {
                isValidCollision = CollisionEnterCondition(collision);
            }

            if (isValidCollision)
            {
                OnCollisionEnterDetect?.Invoke(collision);
            }
        }

        private void OnTriggerEnter(Collider collider)
        {
            bool isValidCollision = false;
            if (ColliderEnterCondition != null)
            {
                isValidCollision = ColliderEnterCondition(collider);
            }

            if (isValidCollision)
            {
                OnColliderEnterDetect?.Invoke(collider);
            }
        }
    }
}
