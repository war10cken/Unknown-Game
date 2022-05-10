using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject Player;
    [Header("Velocities")]
    public float CameraTrackingSpeed = 0.1f;
    [Header("Coordinates")]
    public float OffsetPositionY = 1.5f;
    public float OffsetPositionZ = -10f;

    private Vector3 Velocity = new(0, 0, 0);
    void Update()
    {
        Vector3 newPosition = new(Player.transform.position.x, Player.transform.position.y + OffsetPositionY, Player.transform.position.z - OffsetPositionZ);
        //transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref Velocity, CameraTrackingSpeed * Time.deltaTime);
        transform.position = newPosition;
        transform.LookAt(Player.transform);
    }
}
/* 
 *     public float MaxSpeed = 15f;
transform.position = Vector3.MoveTowards(transform.position, newPosition, MaxSpeed);
transform.position = Vector3.Lerp(transform.position, newPosition, CameraTrackingSpeed);
*/