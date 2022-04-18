using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsMouseTracking : MonoBehaviour
{
    [Header("Raycast")]
    public float RayLenght = 100;
    RaycastHit hit;
    [Header("GameObjects")]
    public GameObject GunModel;
    [Header("Velocities")]
    public float GunTrackingSpeed = 0.2f;
    void FixedUpdate()
    {
        Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray.origin, ray.direction * RayLenght, out hit))
        {
            Vector3 direction = hit.point - GunModel.transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            GunModel.transform.rotation = Quaternion.Slerp(GunModel.transform.rotation, lookRotation, GunTrackingSpeed);
            Debug.DrawRay(GunModel.transform.position, direction, Color.blue);
        }
        else
        {
            Quaternion lookRotation = Quaternion.LookRotation(ray.direction);
            GunModel.transform.rotation = Quaternion.Slerp(GunModel.transform.rotation, lookRotation, GunTrackingSpeed);
        }
    }
}
//Vector3 direction = (hit.point - LaserModel.transform.position).normalized;