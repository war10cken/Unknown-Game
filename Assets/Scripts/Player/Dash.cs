using UnityEngine;

public class Dash : MonoBehaviour
{
    public float DashVelocity = 5f;
    Rigidbody PlayerRigidbody;

    private int Counter = 0;
    private void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire3") && Counter > 4000)
        {
            PlayerRigidbody.AddForce(transform.forward * DashVelocity);
            Counter = 0;
        }
        Counter += (int)(1 / Time.fixedDeltaTime);
    }
}
/*
Vector3 direction = transform.position + transform.forward * DashSpeed;
transform.position = Vector3.Slerp(transform.position, direction, DashVelocity);
*/
