using UnityEngine;

public class GunsPosition : MonoBehaviour
{
    [Header("GameObjects")]
    public Player.Player Player;
    public GameObject RayOriginMark;
    [Header("Vectors")]
    public Vector3 Offset;
    [Header("Raycast")]
    public float RayLenght = 100;
    public float MaxDistanceCollision = 100;
    RaycastHit hit;
    [Header("Velocities")]
    public float DeformationGunPositionTrackingSpeed = 0.2f;
    public float LaserSpeedRotation = 5f;

    private void Update()
    {
        Ray ray = new(RayOriginMark.transform.position, RayOriginMark.transform.forward);

        if (Physics.Raycast(ray.origin, ray.direction * RayLenght, out hit, MaxDistanceCollision))
        {
            Vector3 targetPos = Player.transform.TransformPoint(Offset);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, DeformationGunPositionTrackingSpeed);
        }
    }
}
/*
 * public GameObject LaserModel;
 * 
if (Physics.Raycast(ray.origin, ray.direction * RayLenght, out hit, MaxDistanceCollision))
{
    Vector3 targetPos = Player.transform.TransformPoint(Offset);
    LaserModel.transform.position = Vector3.MoveTowards(LaserModel.transform.position, targetPos, DeformationGunPositionTrackingSpeed);
    if (hit.point == Vector3.zero)
    {
        LaserModel.transform.rotation = Quaternion.RotateTowards(LaserModel.transform.rotation,
                                                                 Player.transform.rotation, LaserSpeedRotation);
    }
}
*/