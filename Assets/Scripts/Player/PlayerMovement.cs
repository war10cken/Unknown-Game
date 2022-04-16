using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public float PlayerSpeed = 5f;
    public float JumpForce = 1500f;
    public float MaxRayDistance = 1f;
    Rigidbody PlayerRigidbody;
    void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        float HorizontalAxis = Input.GetAxis("Horizontal");
        float VerticalAxis = Input.GetAxis("Vertical");

        if (Physics.Raycast(transform.position, Vector3.down, MaxRayDistance))
        {
            Vector3 CharacterMoveDirection = new(HorizontalAxis, 0, VerticalAxis);
            if (Input.GetButtonDown("Jump"))
            {
                //Player's jump
                PlayerRigidbody.AddForce((Vector3.up + CharacterMoveDirection).normalized * JumpForce);
            }
            //Standard movement
            transform.position += PlayerSpeed * CharacterMoveDirection.normalized;

            //направление движения пользователя
            Debug.DrawRay(transform.position + Vector3.up, transform.forward, Color.yellow);
            //Луч проверки пользователя на прыжок
            Debug.DrawRay(transform.position, Vector3.down * MaxRayDistance, Color.red);
        }
    }
}

//if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, _MaxRayDistance))
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