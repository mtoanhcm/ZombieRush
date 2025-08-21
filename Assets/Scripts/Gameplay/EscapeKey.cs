using System;
using UnityEngine;
using ZRCore.Comp;
using ZREvent;

namespace ZRGameplay
{
    public class EscapeKey : MonoBehaviour
    {
        public bool IsActive => gameObject.activeSelf;

        private IColliderDetectComponent colliderDetector;

        private void Awake()
        {
            colliderDetector = gameObject.GetComponentInChildren<IColliderDetectComponent>();
        }

        public void SetKeyPosition(Vector3 pos)
        {
            transform.position = pos;
        }

        private void OnEnable()
        {
            colliderDetector.OnColliderEnterDetect += ClaimKey;
            colliderDetector.ColliderEnterCondition = IsPlayerClaimKey;
        }

        private void OnDisable()
        {
            colliderDetector.OnColliderEnterDetect += ClaimKey;
            colliderDetector.ColliderEnterCondition = null;
        }

        private void ClaimKey(Collider collider)
        {
            GameplayEvent.TriggerGetWinKey();
            gameObject.SetActive(false);
        }

        private bool IsPlayerClaimKey(Collider collider)
        {
            return collider.tag.Equals("Player");
        }
    }
}
