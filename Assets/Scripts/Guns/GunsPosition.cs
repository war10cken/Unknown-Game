using UnityEngine;

public class GunsPosition : MonoBehaviour
{
    [Header("GameObjects")]
    public Player.Player Player;
    public GameObject RayOriginMark;
    [Header("Vectors")]
    public Vector3 Offset;
    Vector3 TargetPos;
    [Header("Raycast")]
    public float MaxDistanceCollision = 100;
    RaycastHit hit;
    [Header("Velocities")]
    public float DeformationGunPositionTrackingSpeed = 0.2f;
    public float LaserSpeedRotation = 5f;
    LayerMask Mask;
    private void Update()
    {
        Ray ray = new(RayOriginMark.transform.position, RayOriginMark.transform.forward);
        Mask = ~LayerMask.GetMask("Player");
        if ( Physics.Raycast(ray.origin, ray.direction, out hit, MaxDistanceCollision, Mask) )
        {
            TargetPos = Player.transform.TransformPoint(Offset);
            transform.position = Vector3.MoveTowards(transform.position, TargetPos, DeformationGunPositionTrackingSpeed * Time.deltaTime);
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