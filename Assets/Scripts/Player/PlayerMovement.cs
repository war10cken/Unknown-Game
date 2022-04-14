using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public float _PlayerSpeed = 5f;
        public float _JumpForce = 1500f;
        public float _MaxRayDistance = 1f;
        Rigidbody Player_Rigidbody;
        RaycastHit hit;
        void Start()
        {
            Player_Rigidbody = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            float _Xaxis = Input.GetAxis("Horizontal");
            float _Zaxis = Input.GetAxis("Vertical");

            //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, _MaxRayDistance))

            {
                Vector3 _MoveDirection = new(_Xaxis, 0, _Zaxis);
                if (Input.GetButtonDown("Jump"))
                {
                    //player jump
                    Player_Rigidbody.AddForce((Vector3.up + _MoveDirection.normalized) * _JumpForce);
                }
                //Standard movement
                transform.position += _MoveDirection.normalized * _PlayerSpeed;

                //направление движения пользователя
                Debug.DrawRay(transform.position + Vector3.up, transform.forward, Color.yellow);
                //Луч проверки пользователя на прыжок
                Debug.DrawRay(transform.position, Vector3.down * _MaxRayDistance, Color.red);
            }
        }
    }
}

//public float _RotSpeed = 0.2f;
//Player_Rigidbody.AddForce((Vector3.up + transform.forward * _Yaxis + Vector3.right * _Xaxis) * _JumpForce);
//transform.position += transform.forward.normalized * _PlayerSpeed * _Yaxis + transform.right.normalized * _PlayerSpeed * _Xaxis;
/*
                //Movement with physics
                Player_Rigidbody.AddForce(_MoveDirection.normalized * _PlayerSpeed, ForceMode.Force);

                if (_MoveDirection != Vector3.zero)
                {
                    //rotation
                    transform.forward = Vector3.Slerp(transform.forward, _MoveDirection, _RotSpeed);
                }
*/