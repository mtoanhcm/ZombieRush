using UnityEngine;
using ZRCore.Comp;
using ZRUtility;

namespace ZRComponent
{
    public class MovementComponent : MonoBehaviour, IMovementComponent
    {
        public float MoveSpeed {  get; private set; }

        public float SprintSpeed {  get; private set; }

        public float JumpForce {  get; private set; }

        private Vector2 moveDirection;
        private Rigidbody rb;
        private bool isSprint;
        private bool canMove;

        private void FixedUpdate()
        {
            if (!canMove)
            {
                return;
            }

            if(rb == null)
            {
                return;
            }

            var currentMoveSpeed = isSprint ? SprintSpeed : MoveSpeed;
            Vector3 moveVector = new Vector3(moveDirection.x, 0, moveDirection.y) * currentMoveSpeed * Time.fixedDeltaTime;

            if (moveVector != Vector3.zero)
            {
                rb.MovePosition(rb.position + moveVector);

                Quaternion targetRotation = Quaternion.LookRotation(moveVector, Vector3.up);
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, 10 * Time.fixedDeltaTime));
            }
        }

        public void Init(float moveSpeed, float sprintSpeed, float jumpForce)
        {
            MoveSpeed = moveSpeed;
            SprintSpeed = sprintSpeed;
            JumpForce = jumpForce;
            
            if(rb == null && !TryGetComponent(out rb))
            {
                rb = gameObject.AddComponent<Rigidbody>();
            }

            canMove = true;
        }

        [SerializeField]
        private bool isDebug;
        public void Move(Vector2 direction)
        {
            if (isDebug)
            {
                Debug.Log(direction);
            }

            moveDirection = direction;
        }

        public void SetCanMove(bool canMove)
        {
            this.canMove = canMove;
        }

        public void Jump()
        {
            if (!canMove)
            {
                return;
            }

            bool isOnGround = Physics.OverlapSphere(transform.position, 0.01f, ObjectLayer.SolidObjectLayer).Length > 0;
            if (!isOnGround) {
                return;
            }

            rb.AddForce(Vector3.up * JumpForce * 2, ForceMode.Impulse);
            rb.AddForce(transform.forward * JumpForce, ForceMode.Impulse);

        } 

        public void Sprint(bool isSpinning)
        {
            isSprint = isSpinning;
        }
    }
}
