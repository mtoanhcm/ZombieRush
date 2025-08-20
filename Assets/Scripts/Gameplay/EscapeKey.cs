using System;
using Unity.Loading;
using UnityEngine;
using ZREvent;
using ZRUtility;

namespace ZRGameplay
{
    public class EscapeKey : MonoBehaviour
    {
        public bool IsActive => gameObject.activeSelf;

        [SerializeField]
        private ColliderDetector colliderDetector;

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
