using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsMouseTracking : MonoBehaviour
{
    [Header("Raycast")]
    public float LaserLenght = 100;
    
    RaycastHit hit;
    
    [Header("3DModels")]
    public GameObject LaserModel;
    
    [Header("Velocities")]
    public float DeformationGunTrackingSpeed = 0.2f;

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray.origin, ray.direction * LaserLenght, out hit))
        {
            Vector3 direction = (hit.point - LaserModel.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            LaserModel.transform.rotation = Quaternion.Slerp(LaserModel.transform.rotation, 
                                                              lookRotation, DeformationGunTrackingSpeed);
            
            Debug.DrawRay(LaserModel.transform.position, direction, Color.blue);
        }
    }
}
