using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracking : MonoBehaviour
{
    [Header("Raycast")]
    public float _LaserLenght = 100;
    RaycastHit hit;
    [Header("Player")]
    public GameObject _Player;
    [Header("Velocities")]
    public float _PlayerTrackingSpeed = 0.2f;
    public void FixedUpdate()
    {
        Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        Vector3 _target;
        if (Physics.Raycast(ray.origin, ray.direction * _LaserLenght, out hit))
        {
            _target = (hit.point - _Player.transform.position);
            _target = new Vector3(_target.x, 0, _target.z);
            _Player.transform.forward = Vector3.Slerp(_Player.transform.forward, _target, _PlayerTrackingSpeed);
        }
        else
        {
            _target = ray.direction;
            _target = new Vector3(_target.x, 0, _target.z);
            _Player.transform.forward = Vector3.Slerp(_Player.transform.forward, _target, _PlayerTrackingSpeed);
        }
        Debug.DrawRay(ray.origin, ray.direction * _LaserLenght, Color.cyan);
    }
}