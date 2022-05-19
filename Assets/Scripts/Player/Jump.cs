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
        float HorizontalAxis = Input.GetAxis("Horizontal");
        float VerticalAxis = Input.GetAxis("Vertical");
        // Направление прыжка.
        Vector3 CharacterMoveDirection = new(HorizontalAxis, 0, VerticalAxis);
        if ( Physics.Raycast(transform.position + PositionOffset, Vector3.down, MaxRayDistance) && Input.GetButton("Jump") )
        {
            // Player's jump.
            if (JumpTimer >= 8000)
            {
                Rb.AddForce( 1000000 * JumpForce * Time.deltaTime * (Vector3.up + CharacterMoveDirection).normalized);
                JumpTimer = 0;
            }
        }
        else if (!Physics.Raycast(transform.position + PositionOffset, Vector3.down, MaxRayDistance))
        {
            Rb.AddForce(InJumpSpeed * Time.deltaTime * CharacterMoveDirection);
        }
        if (JumpTimer > 7999)
        {
            JumpTimer = 8000;
        }
        else
        {
            JumpTimer += (int)(1 / Time.deltaTime);
        }
    }
}
