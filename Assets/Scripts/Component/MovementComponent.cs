using UnityEngine;
using ZRCore.Comp;

namespace ZRComponent
{
    public class MovementComponent : MonoBehaviour, IMovementComponent
    {
        public float MoveSpeed {  get; private set; }

        private Vector2 moveDirection;
        private Rigidbody rb;

        private void FixedUpdate()
        {
            if(rb == null)
            {
                return;
            }

            Vector3 moveVector = new Vector3(moveDirection.x, 0, moveDirection.y) * MoveSpeed * Time.fixedDeltaTime;

            if (moveVector != Vector3.zero)
            {
                rb.MovePosition(rb.position + moveVector);

                Quaternion targetRotation = Quaternion.LookRotation(moveVector, Vector3.up);
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, 10 * Time.fixedDeltaTime));
            }
        }

        public void Init(float moveSpeed)
        {
            MoveSpeed = moveSpeed;
            
            if(!TryGetComponent(out rb))
            {
                rb = gameObject.AddComponent<Rigidbody>();
            }
        }

        public void Move(Vector2 direction)
        {
            moveDirection = direction;
        }
    }
}
