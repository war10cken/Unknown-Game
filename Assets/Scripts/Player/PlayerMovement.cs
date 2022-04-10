using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public float _PlayerSpeed = 5f;
        public float _JumpForce = 1500f;
        public float _MaxRayDistance = 1f;
        public float _RotSpeed = 0.2f;
        Rigidbody Player_Rigidbody;
        RaycastHit hit;
        void Start()
        {
            Player_Rigidbody = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            float _Xaxis = Input.GetAxis("Horizontal");
            float _Yaxis = Input.GetAxis("Vertical");

            if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, _MaxRayDistance))
            {
                if (Input.GetButtonDown("Jump"))
                {
                    //player jump
                    Player_Rigidbody.AddForce((Vector3.up + transform.forward * _Yaxis + Vector3.right * _Xaxis) * _JumpForce);
                }
                
                //Vector movement
                Vector3 _MoveDirection = new Vector3(_Xaxis / 10f, 0, _Yaxis / 10f);
                //Standard movement
                transform.position += _MoveDirection * _PlayerSpeed;
                //Movement with physics
                // Player_Rigidbody.AddForce(_MoveDirection.normalized * _PlayerSpeed, ForceMode.Force);

                if (_MoveDirection != Vector3.zero)
                {
                    //rotation
                    //transform.forward = Vector3.Slerp(transform.forward, _MoveDirection, _RotSpeed);
                }
                Debug.DrawRay(transform.position + Vector3.up, transform.forward, Color.yellow);
                Debug.DrawRay(transform.position, Vector3.down * _MaxRayDistance, Color.red);
            }
        }
    }
}

//public float _PlayerRot = 0f;
//transform.right = Vector3.Slerp(transform.right, _MoveDirection, _RotSpeed);