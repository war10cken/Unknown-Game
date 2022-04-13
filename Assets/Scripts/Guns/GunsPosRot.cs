using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsPosRot : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject _Player;
    public GameObject _LaserModel;
    public GameObject _RayOriginMark;
    [Header("Vectors")]
    public Vector3 _offset;
    [Header("Raycast")]
    public float _LaserLenght = 100;
    public float _MaxDistanceCollision = 100;
    RaycastHit hit;
    [Header("Velocities")]
    public float _DeformGunPositionTrackingSpeed = 0.2f;
    public float _LaserSpeedRot = 5f;
    void FixedUpdate()
    {
        Ray ray = new(_RayOriginMark.transform.position, _RayOriginMark.transform.forward);
        if (Physics.Raycast(ray.origin, ray.direction * _LaserLenght, out hit, _MaxDistanceCollision))
        {
            Vector3 targetPos = _Player.transform.TransformPoint(_offset);
            _LaserModel.transform.position = Vector3.MoveTowards(_LaserModel.transform.position, targetPos, _DeformGunPositionTrackingSpeed);
            if (hit.point == Vector3.zero)
            {
                _LaserModel.transform.rotation = Quaternion.RotateTowards(_LaserModel.transform.rotation, _Player.transform.rotation, _LaserSpeedRot);
            }
        }
    }
}
