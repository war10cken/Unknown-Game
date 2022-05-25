using UnityEngine;
public class CameraTracking : MonoBehaviour
{
    RaycastHit Hit;
    /*
    [Header("GameObjects")]
    public GameObject Player;
    [Header("Velocities")]
    public float CameraTrackingSpeed = 0.1f;
    [Header("Coordinates")]
    public float OffsetPositionY = 1.5f;
    public float OffsetPositionZ = -10f;
    public float MaxRayDistance = 5f;
    private Vector3 Velocity = new(0, 0, 0);
    */
    void Update()
    {
        Ray RayDirectionToRotate = Camera.main.ScreenPointToRay(Input.mousePosition);
        /*
        if ( Physics.Raycast(RayDirectionToRotate, out Hit, Mathf.Infinity))
        {
            
        }
        */
    }
}
/* 
 *     public float MaxSpeed = 15f;
transform.position = Vector3.MoveTowards(transform.position, newPosition, MaxSpeed);
transform.position = Vector3.Lerp(transform.position, newPosition, CameraTrackingSpeed);
*/
//transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref Velocity, CameraTrackingSpeed * Time.deltaTime);

//transform.LookAt(Player.transform);
//transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref Velocity, CameraTrackingSpeed * Time.deltaTime);


//transform.position = newPosition;
/*
Ray ray = new(transform.position, -transform.forward);
if (Physics.Raycast(ray, MaxRayDistance))
{
    //Vector3 TargetPos = transform.position - transform.forward;
    //Vector3.MoveTowards(transform.position, TargetPos, CameraTrackingSpeed);
    //transform.position -= transform.forward * 5f;
}
else
{
    //transform.position = newPosition;
}
*/

/*
 Vector3 OFFSET = new(0, OffsetPositionY, OffsetPositionZ);
        Vector3 newPosition = (Player.transform.position - transform.forward) + OFFSET;
        //Vector3 newPosition = new(Player.transform.position.x, Player.transform.position.y + OffsetPositionY, Player.transform.position.z - OffsetPositionZ);
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref Velocity, CameraTrackingSpeed * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(Player.transform.forward);
        Debug.DrawRay(transform.position, -transform.forward, Color.red);
 */