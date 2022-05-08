using UnityEngine;

public class Jump : MonoBehaviour
{
    public float JumpForce = 1500f;
    public Vector3 PositionOffset;
    public float MaxRayDistance = 1f;
    private float JumpTimer = 0f;
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
                GetComponent<Rigidbody>().AddForce((Vector3.up + CharacterMoveDirection).normalized * JumpForce);
                JumpTimer = 0;
            }
        }

        if (JumpTimer > 8000)
        {
            JumpTimer = 8000;
        }
        else
        {
            JumpTimer += (int)(1 / Time.deltaTime);
        }
        //Debug.Log(JumpTimer);
    }
}
