using UnityEngine;

public class GunsMouseTracking : MonoBehaviour
{
    LayerMask Mask;
    RaycastHit hit;
    [Header("Velocities")]
    public float GunTrackingSpeed = 0.2f;
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Mask = ~LayerMask.GetMask("NoneCollision");

        if ( Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, Mask) )
        {
            Vector3 direction = hit.point - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, GunTrackingSpeed * Time.deltaTime);

            Debug.DrawRay(ray.origin, hit.point, Color.blue);
        }
        else
        {
            Quaternion lookRotation = Quaternion.LookRotation(ray.direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, GunTrackingSpeed);
        }
    }
}
//Vector3 direction = (hit.point - LaserModel.transform.position).normalized;
/*
[Header("Raycast")]
public float RayLenght = 100;
*/