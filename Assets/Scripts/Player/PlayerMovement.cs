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
    Rigidbody ThisRb;
    [Header("Animator")]
    Animator PlayerAnimator;
    private void Awake()
    {
        FootSteps = GetComponent<AudioSource>();
        ThisRb = GetComponent<Rigidbody>();
        PlayerAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        float HorizontalAxis = Input.GetAxisRaw("Horizontal");
        float VerticalAxis = Input.GetAxisRaw("Vertical");

        // �������� �� IsJumped.
        if (Physics.Raycast(transform.position + IfJumpedRayPositionOffset, Vector3.down, MaxRayDistance))
        {
            CharacterMoveDirection = new(HorizontalAxis, 0, VerticalAxis);
            Ray[] DistanceToCollisionRays = new Ray[4];
            for (int i = 0; i < 1; i++)
            {
                //
                Vector3 Direction = VerticalAxis * PlayerSpeed * Time.deltaTime * transform.forward + HorizontalAxis * PlayerSpeed * Time.deltaTime * transform.right;
                //
                DistanceToCollisionRays[i] = new(transform.position + CollisionRayOrigin,RayLenght * CharacterMoveDirection);
                // �������� ������������ � ������������.
                Debug.DrawRay(transform.position + CollisionRayOrigin, RayLenght * Direction);
                if (!Physics.Raycast(DistanceToCollisionRays[i], RayLenght))
                {
                    if (!Input.GetButtonDown("Fire3") && Dash.Counter > 1000)
                    {
                        // �������������� ��������.
                        //ThisRb.velocity = PlayerSpeed * Time.deltaTime * CharacterMoveDirection.normalized;
                        ThisRb.velocity = Direction;
                    }
                    // FootStep sound.
                    if (CharacterMoveDirection != Vector3.zero && FootSteps.isPlaying == false) 
                    {
                        FootSteps.Play();
                    }else if (CharacterMoveDirection == Vector3.zero)
                    {
                        FootSteps.Stop();
                    }
                    // Animation.
                    if (ThisRb.velocity.sqrMagnitude != 0 && Dash.Counter > 1000)
                    {
                        PlayerAnimator.SetBool("IsRunning", true);
                        PlayerAnimator.SetBool("IsIdle", false);
                        PlayerAnimator.SetBool("IsJumping", false);
                        PlayerAnimator.SetBool("IsDash", false);
                    }
                    else if (ThisRb.velocity.sqrMagnitude == 0)
                    { 
                        PlayerAnimator.SetBool("IsIdle", true);
                        PlayerAnimator.SetBool("IsRunning", false);
                        PlayerAnimator.SetBool("IsJumping", false);
                        PlayerAnimator.SetBool("IsDash", false);
                    }
                    else if(Dash.Counter <= 1000)
                    {
                        PlayerAnimator.SetBool("IsDash", true);
                        PlayerAnimator.SetBool("IsIdle", false);
                        PlayerAnimator.SetBool("IsRunning", false);
                        PlayerAnimator.SetBool("IsJumping", false);
                        Debug.Log(Dash.Counter);
                    }
                }
            }
            // ��� �������� ������������ �� ������.
            Debug.DrawRay(transform.position + IfJumpedRayPositionOffset, Vector3.down * MaxRayDistance, Color.red);
        }else
        {
            PlayerAnimator.SetBool("IsJumping", true);
            PlayerAnimator.SetBool("IsIdle", false);
            PlayerAnimator.SetBool("IsRunning", false);
            PlayerAnimator.SetBool("IsDash", false);
        }
        // ����������� �������� ������������.
        Debug.DrawRay(transform.position + Vector3.up, transform.right, Color.yellow);
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
// ���������� ��������.
PlayerRigidbody.velocity = CharacterMoveDirection.normalized * PlayerSpeed;
PlayerRigidbody.AddForce(CharacterMoveDirection.normalized * PlayerSpeed);
// ��������� ��������.
transform.position += CharacterMoveDirection.normalized * PlayerSpeed;
*/
//public float MaxPlayerSpeed = 1000f;

// ��� �������� ������������ �� ������.
//Debug.DrawRay(transform.position + IfJumpedRayPositionOffset, Vector3.down * MaxRayDistance, Color.red);
//&& !Input.GetButton("Fire3") ) //&& !Input.GetButton("Jump")

/*09.05.2022
Ray DistanceToCollisionRay = new(transform.position + CollisionRayOrigin, CharacterMoveDirection);
// �������� �� ���������� �� ������ ����������.
if ( !Physics.Raycast(DistanceToCollisionRay, out Hit, RayLenght) )
{
    if ( !Input.GetButtonDown("Fire3") && Dash.Counter > 1000)
    {
        // �������������� ��������.
        GetComponent<Rigidbody>().velocity = PlayerSpeed * Time.deltaTime * CharacterMoveDirection.normalized;
    }
}
*/
/* 18.05.2022
 ( transform.forward * Mathf.Cos(Angle) + transform.right * Mathf.Sin(Angle) ).normalized * RayLenght 
              float Angle = 45f * i * Mathf.Deg2Rad;
*/ 