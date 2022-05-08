using UnityEngine;

public class GunsMouseTracking : MonoBehaviour
{
    [Header("Raycast")]
    public float RayLenght = 100;
    RaycastHit hit;
    [Header("Velocities")]
    public float GunTrackingSpeed = 0.2f;
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray.origin, ray.direction * RayLenght, out hit))
        {
            Vector3 direction = hit.point - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, GunTrackingSpeed);
            Debug.DrawRay(transform.position, direction, Color.blue);
        }
        else
        {
            Quaternion lookRotation = Quaternion.LookRotation(ray.direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, GunTrackingSpeed);
        }
    }
}
//Vector3 direction = (hit.point - LaserModel.transform.position).normalized;