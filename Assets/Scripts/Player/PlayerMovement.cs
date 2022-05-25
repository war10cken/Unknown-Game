using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class PlayerMovement : MonoBehaviour
{
    Animator PlayerAnimator;
    Rigidbody ThisRb;
    public float PlayerSpeed = 5f;
    public float MaxRayDistance = 1f;
    public Vector3 IfJumpedRayPositionOffset;
    private Vector3 CharacterMoveDirection;

    float HorizontalAxis;
    float VerticalAxis;
    Ray[] DistanceToCollisionRays;
    [Header("Sounds")]
    AudioSource FootSteps;
    private float FootStepTimer = 20f;
    public float FootStepsStep = 20f;
    [Header("CollisionSystem")]
    public Vector3 CollisionRayOrigin;
    public float RayLenght = 1f;
    public float ForceCollision = 5f;
    RaycastHit Hit;
    private void Start()
    {
        FootSteps = GetComponent<AudioSource>();
        ThisRb = GetComponent<Rigidbody>();
        PlayerAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        HorizontalAxis = Input.GetAxisRaw("Horizontal");
        VerticalAxis = Input.GetAxisRaw("Vertical");

        // IsJumped detection.
        if (Physics.Raycast(transform.position + IfJumpedRayPositionOffset, Vector3.down, MaxRayDistance))
        {
            CharacterMoveDirection = new(HorizontalAxis, 0, VerticalAxis);
            DistanceToCollisionRays = new Ray[1];
            for (int i = 0; i < 1; i++)
            {
                DistanceToCollisionRays[i] = new(transform.position + CollisionRayOrigin, RayLenght * CharacterMoveDirection);
                Debug.DrawRay(transform.position + CollisionRayOrigin, RayLenght * CharacterMoveDirection, Color.black);
                if (!Physics.Raycast(DistanceToCollisionRays[i], RayLenght))
                {
                    if (!Input.GetButtonDown("Fire3") && Dash.Counter > 1000)
                    {
                        // Almose physical movement.
                        ThisRb.velocity = PlayerSpeed * Time.deltaTime * CharacterMoveDirection.normalized;
                    }
                    FootStepsSounds();
                    Animations();
                }
            }
            // IsJumpedRay.
            Debug.DrawRay(transform.position + IfJumpedRayPositionOffset, Vector3.down * MaxRayDistance, Color.red);
        }else
        {
            // Animation.
            PlayerAnimator.SetBool("IsJumping", true);
            PlayerAnimator.SetBool("IsIdle", false);
            PlayerAnimator.SetBool("IsRunning", false);
            PlayerAnimator.SetBool("IsDash", false);
        }
        // Player move direction.
        Debug.DrawRay(transform.position + Vector3.up, transform.forward, Color.yellow);
    }
    void FootStepsSounds()
    {
        // FootStep sound.
        if (FootStepTimer > FootStepsStep)
        {
            FootStepTimer = FootStepsStep;
        }
        else
        {
            FootStepTimer += 1 / Time.deltaTime;
        }
        if (CharacterMoveDirection != Vector3.zero && FootSteps.isPlaying == false && FootStepTimer == FootStepsStep)
        {
            FootSteps.Play();
            FootStepTimer = 0;
        }
        else if (CharacterMoveDirection == Vector3.zero)
        {
            FootSteps.Stop();
        }
    }
    void Animations()
    {
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
        else if (Dash.Counter <= 1000)
        {
            PlayerAnimator.SetBool("IsDash", true);
            PlayerAnimator.SetBool("IsIdle", false);
            PlayerAnimator.SetBool("IsRunning", false);
            PlayerAnimator.SetBool("IsJumping", false);
        }
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

// Shooter Movement. 20.05.2022
//Vector3 Direction = VerticalAxis * PlayerSpeed * Time.deltaTime * transform.forward + HorizontalAxis * PlayerSpeed * Time.deltaTime * transform.right;
//ThisRb.velocity = Direction;
//