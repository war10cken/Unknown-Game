using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        public float PlayerSpeed = 5f;
        public float JumpForce = 1500f;
        public float MaxRayDistance = 1f;

        private Rigidbody _rigidbody;
        private RaycastHit _hit;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            float xAxis = Input.GetAxis("Horizontal");
            float yAxis = Input.GetAxis("Vertical");

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out _hit, MaxRayDistance))
            {
                if (Input.GetButtonDown("Jump"))
                {
                    //player jump
                    _rigidbody.AddForce((Vector3.up + transform.forward * yAxis + Vector3.right * xAxis) * JumpForce);
                }
                //Vector movement
                Vector3 moveDirection = new Vector3(xAxis, 0, yAxis);
                //Standard movement
                transform.position += moveDirection.normalized * PlayerSpeed;
                //Movement with physics
                //Player_Rigidbody.AddForce(_MoveDirection.normalized * _PlayerSpeed, ForceMode.Force);

                if (moveDirection != Vector3.zero)
                {
                    //rotation
                    //transform.forward = Vector3.Slerp(transform.forward, _MoveDirection, _RotSpeed);
                }
                
                Debug.DrawRay(transform.position + Vector3.up, transform.forward, Color.yellow);
                Debug.DrawRay(transform.position, Vector3.down * MaxRayDistance, Color.red);
            }
        }
    }

}