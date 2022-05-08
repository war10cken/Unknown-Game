using UnityEngine;
using DG.Tweening;
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
    void Update()
    {
        float HorizontalAxis = Input.GetAxisRaw("Horizontal");
        float VerticalAxis = Input.GetAxisRaw("Vertical");

        if (Physics.Raycast(transform.position + IfJumpedRayPositionOffset, Vector3.down, MaxRayDistance))
        {
            CharacterMoveDirection = new(HorizontalAxis, 0, VerticalAxis);
            // �������������� ��������.
            Ray DistanceToCollisionRay = new(transform.position + CollisionRayOrigin, CharacterMoveDirection);
            if (!Physics.Raycast(DistanceToCollisionRay, out Hit, RayLenght) && !Input.GetButton("Fire3") && !Input.GetButton("Jump") )
            {
                GetComponent<Rigidbody>().velocity = PlayerSpeed * Time.deltaTime * CharacterMoveDirection.normalized;
            }
            else if (Physics.Raycast(DistanceToCollisionRay, out Hit, RayLenght))
            {
                Rigidbody hitRigidBody = Hit.collider.gameObject.GetComponent<Rigidbody>();
                if (hitRigidBody != null)
                {
                    hitRigidBody.AddForce(CharacterMoveDirection.normalized * ForceCollision);
                }
                //Debug.Log(hitRigidBody);
            }
        }
        // �������� ������������ � ������������.
        Debug.DrawRay(transform.position + CollisionRayOrigin, CharacterMoveDirection * RayLenght, Color.black);
        // ����������� �������� ������������.
        Debug.DrawRay(transform.position + Vector3.up, transform.forward, Color.yellow);
        // ��� �������� ������������ �� ������.
        Debug.DrawRay(transform.position + IfJumpedRayPositionOffset, Vector3.down * MaxRayDistance, Color.red);
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