using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public float PlayerSpeed = 5f;
    public float MaxRayDistance = 1f;
    public Vector3 IfJumpedRayPositionOffset;
    private Vector3 CharacterMoveDirection;
    [Header("CollisionSystem")]
    public float RayLenght = 1f;
    public float ForceCollision = 5f;
    RaycastHit Hit;
    public Vector3 CollisionRayOrigin;
    [Header("Sounds")]
    AudioSource FootSteps;
    private void Start()
    {
        FootSteps = GetComponent<AudioSource>();
    }
    void Update()
    {
        float HorizontalAxis = Input.GetAxisRaw("Horizontal");
        float VerticalAxis = Input.GetAxisRaw("Vertical");

        // Проверка на IsJumped.
        if (Physics.Raycast(transform.position + IfJumpedRayPositionOffset, Vector3.down, MaxRayDistance))
        {
            CharacterMoveDirection = new(HorizontalAxis, 0, VerticalAxis);
            Ray[] DistanceToCollisionRays = new Ray[4];
            for (int i = 0; i < 4; i++)
            {
                DistanceToCollisionRays[i] = new(transform.position + CollisionRayOrigin + Vector3.up * i, CharacterMoveDirection);
                // Проверка столкновений с коллайдерами.
                Debug.DrawRay(transform.position + CollisionRayOrigin - Vector3.up * i / 2, CharacterMoveDirection * RayLenght, Color.black);
                if (!Physics.Raycast(DistanceToCollisionRays[i], RayLenght))
                {
                    if (!Input.GetButtonDown("Fire3") && Dash.Counter > 1000)
                    {
                        // Полуфизическое Движение.
                        GetComponent<Rigidbody>().velocity = PlayerSpeed * Time.deltaTime * CharacterMoveDirection.normalized;
                    }
                    // FootStep sound.
                    if (CharacterMoveDirection != Vector3.zero && FootSteps.isPlaying == false) 
                    {
                        FootSteps.Play();
                    }else if (CharacterMoveDirection == Vector3.zero)
                    {
                        FootSteps.Stop();
                    }
                    //
                }
            }
            // Луч проверки пользователя на прыжок.
            Debug.DrawRay(transform.position + IfJumpedRayPositionOffset, Vector3.down * MaxRayDistance, Color.red);
        }
        // Направление движения пользователя.
        Debug.DrawRay(transform.position + Vector3.up, transform.forward, Color.yellow);
    }
}

//    public float JumpForce = 1500f;
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
/*
// Физическое движение.
PlayerRigidbody.velocity = CharacterMoveDirection.normalized * PlayerSpeed;
PlayerRigidbody.AddForce(CharacterMoveDirection.normalized * PlayerSpeed);
// Векторное движение.
transform.position += CharacterMoveDirection.normalized * PlayerSpeed;
*/
//public float MaxPlayerSpeed = 1000f;

// Луч проверки пользователя на прыжок.
//Debug.DrawRay(transform.position + IfJumpedRayPositionOffset, Vector3.down * MaxRayDistance, Color.red);
//&& !Input.GetButton("Fire3") ) //&& !Input.GetButton("Jump")

/*09.05.2022
Ray DistanceToCollisionRay = new(transform.position + CollisionRayOrigin, CharacterMoveDirection);
// Проверка на расстояние до любого коллайдера.
if ( !Physics.Raycast(DistanceToCollisionRay, out Hit, RayLenght) )
{
    if ( !Input.GetButtonDown("Fire3") && Dash.Counter > 1000)
    {
        // Полуфизическое Движение.
        GetComponent<Rigidbody>().velocity = PlayerSpeed * Time.deltaTime * CharacterMoveDirection.normalized;
    }
}
*/