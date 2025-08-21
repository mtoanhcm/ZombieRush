using UnityEngine;
using ZRCore;

namespace ZRComponent
{
    public class CharacterAnimationComponent : MonoBehaviour, IAnimationComponent
    {
        private Animator animator;
        private int moveSpeedHash;
        private float currentMoveSpeed;

        private void Awake()
        {
            animator = transform.GetComponentInChildren<Animator>();
            moveSpeedHash = Animator.StringToHash("MoveSpeed");
        }

        private void Update()
        {
            if (animator == null)
            {
                return;
            }

            animator.SetFloat(moveSpeedHash, currentMoveSpeed);
        }

        public void OnSpeedChanged(Vector2 direction)
        {
            currentMoveSpeed = direction.magnitude;
        }
    }
}
