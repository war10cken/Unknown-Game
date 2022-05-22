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
        // Jump direction.
        Vector3 CharacterMoveDirection = new(HorizontalAxis, 0, VerticalAxis);
        if ( Physics.Raycast(transform.position + PositionOffset, Vector3.down, MaxRayDistance) && Input.GetButton("Jump") )
        {
            // Player's jump.
            if (JumpTimer >= 8000)
            {
                Rb.AddForce( JumpForce * (Vector3.up + CharacterMoveDirection).normalized);
                JumpTimer = 0;
            }
        }
        if (!Physics.Raycast(transform.position + PositionOffset, Vector3.down, MaxRayDistance))
        {
            Rb.AddForce(InJumpSpeed * Time.deltaTime * CharacterMoveDirection);
        }
        JumpTimerF();
    }
    void JumpTimerF()
    {
        if (JumpTimer > 7999)
        {
            JumpTimer = 8000;
        }
        else
        {
            JumpTimer += (1 / Time.deltaTime);
        }
    }
}
