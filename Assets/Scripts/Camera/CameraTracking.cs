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
    void Update()
    {
        Vector3 newPosition = new(Player.transform.position.x, Player.transform.position.y + OffsetPositionY, Player.transform.position.z - OffsetPositionZ);
        transform.position = Vector3.Slerp(transform.position, newPosition, CameraTrackingSpeed);
    }
}