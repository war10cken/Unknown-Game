using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Jump : MonoBehaviour
{
    [Header("Velocities")]
    public float JumpForce = 1500f;
    public float InJumpSpeed = 1000f;
    [Header("Vectors")]
    public Vector3 PositionOffset;
    public float MaxRayDistance = 1f;
    private float JumpTimer = 8000f;
    Rigidbody Rb;
    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if ( Physics.Raycast(transform.position + PositionOffset, Vector3.down, MaxRayDistance) && Input.GetButton("Jump") )
        {
            float HorizontalAxis = Input.GetAxis("Horizontal");
            float VerticalAxis = Input.GetAxis("Vertical");
            // Направление прыжка.
            Vector3 CharacterMoveDirection = new(HorizontalAxis, 0, VerticalAxis);
            // Player's jump.
            if (JumpTimer >= 8000)
            {
                Rb.AddForce((Vector3.up + CharacterMoveDirection).normalized * JumpForce);
                JumpTimer = 0;
            }

        }
        else if (!Physics.Raycast(transform.position + PositionOffset, Vector3.down, MaxRayDistance))
        {   //15.05.2022
            float HorizontalAxis = Input.GetAxis("Horizontal");
            float VerticalAxis = Input.GetAxis("Vertical");
            Vector3 CharacterMoveDirection = new(HorizontalAxis, 0, VerticalAxis);
            Rb.AddForce(CharacterMoveDirection * InJumpSpeed * Time.deltaTime);
            //
        }

            if (JumpTimer > 8000)
        {
            JumpTimer = 8000;
        }
        else
        {
            JumpTimer += (int)(1 / Time.deltaTime);
        }
    }
}
