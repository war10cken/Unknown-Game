using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsMouseTracking : MonoBehaviour
{
    [Header("Raycast")]
    public float _LaserLenght = 100;
    RaycastHit hit;
    [Header("3DModels")]
    public GameObject _LaserModel;
    [Header("Velocities")]
    public float _DeformGunTrackingSpeed = 0.2f;
    void FixedUpdate()
    {
        Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray.origin, ray.direction * _LaserLenght, out hit))
        {
            Vector3 _Dir = (hit.point - _LaserModel.transform.position).normalized;
            Quaternion _lookRotation = Quaternion.LookRotation(_Dir);
            _LaserModel.transform.rotation = Quaternion.Slerp(_LaserModel.transform.rotation, _lookRotation, _DeformGunTrackingSpeed);
            Debug.DrawRay(_LaserModel.transform.position, _Dir, Color.blue);
        }
    }
}
