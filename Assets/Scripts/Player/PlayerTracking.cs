using UnityEngine;
public class PlayerTracking : MonoBehaviour
{
    LayerMask Mask;
    RaycastHit hit;
    public float MaxRayDistance = 50f;
    [Header("Velocities")]
    public float PlayerTrackingSpeed = 0.2f;
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Vector3 target;
        Mask = ~LayerMask.GetMask("NoneCollision");
        if (!Input.GetButton("G"))
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, MaxRayDistance, Mask))
            {
                target = (hit.point - transform.position);
                target = new Vector3(target.x, 0, target.z);
                transform.forward = Vector3.Slerp(transform.forward, target, PlayerTrackingSpeed * Time.deltaTime);
                Debug.DrawLine(ray.origin, hit.point, Color.red);
            }else
            {
                target = ray.direction;
                target = new Vector3(target.x, 0, target.z);
                transform.forward = Vector3.Slerp(transform.forward, target, PlayerTrackingSpeed * Time.deltaTime);
                Debug.DrawRay(ray.origin, ray.direction * 10000, Color.green);
            }
        }
    }
}
/*
[Header("Player")]
public GameObject Player;

target = (hit.point - Player.transform.position);
target = new Vector3(target.x, 0, target.z);
Player.transform.forward = Vector3.Slerp(Player.transform.forward, target, PlayerTrackingSpeed); 

target = ray.direction;
target = new Vector3(target.x, 0, target.z);
Player.transform.forward = Vector3.Slerp(Player.transform.forward, target, PlayerTrackingSpeed);
*/

/*
[Header("Raycast")]
public float LaserLenght = 100;
RaycastHit hit;
*/