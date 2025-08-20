using System;
using UnityEngine;
using ZREvent;
using ZRUtility;
using DG.Tweening;
using System.Collections;

namespace ZRGameplay
{
    public class EscapeDoor : MonoBehaviour
    {
        [SerializeField]
        private Transform door;
        [SerializeField]
        private ColliderDetector colliderDetector;
        [SerializeField]
        private Collider roomBox;
        [SerializeField]
        private EscapeKey key;
        [SerializeField]
        private float keyChangeTime;

        private void OnEnable()
        {
            colliderDetector.OnColliderEnterDetect += SendWinGameSignal;
            colliderDetector.ColliderEnterCondition = IsPlayerEnterDoor;
            GameplayEvent.OnGetWinKey += OpenWinDoor;
            GameplayEvent.OnStarGame += StartChangeKeyPosition;
        }

        private void OnDisable()
        {
            colliderDetector.OnColliderEnterDetect -= SendWinGameSignal;
            GameplayEvent.OnGetWinKey -= OpenWinDoor;
            GameplayEvent.OnStarGame -= StartChangeKeyPosition;
        }

        private bool IsPlayerEnterDoor(Collider collider)
        {
            return collider.tag.Equals("Player");
        }

        private void OpenWinDoor()
        {
            Debug.Log("OPen door");
            door.DORotate(new Vector3(0, 90, 0), 3f);
        }

        private void SendWinGameSignal(Collider collider)
        {
            GameplayEvent.TriggerGameover(isWin: true);
        }

        private void StartChangeKeyPosition()
        {
            //StartCoroutine(ChangeKeyPosition());
        }

        private IEnumerator ChangeKeyPosition()
        {
            var delay = new WaitForSeconds(keyChangeTime);
            while (key.IsActive)
            {
                key.transform.position = GetNewKeyPosition();

                yield return delay;
            }
        }

        private Vector3 GetNewKeyPosition()
        {
            for (int i = 0; i < 20; i++)
            {
                Bounds bounds = roomBox.bounds;
                Vector3 randomPos = new Vector3(
                    UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
                    transform.position.y,
                    UnityEngine.Random.Range(bounds.min.z, bounds.max.z)
                );

                if(Physics.OverlapSphere(randomPos, 3f, ObjectLayer.ObstacleLayer).Length > 0)
                {
                    return randomPos;
                }
            }

            return Vector3.zero;
        }
    }
}
