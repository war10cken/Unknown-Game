 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracking : MonoBehaviour
{
    [Header("Raycast")]
    public float LaserLenght = 100;
    RaycastHit hit;
    [Header("Velocities")]
    public float PlayerTrackingSpeed = 0.2f;
    void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Vector3 target;
        if (Physics.Raycast(ray.origin, ray.direction * LaserLenght, out hit))
        {
            target = (hit.point - transform.position);
            target = new Vector3(target.x, 0, target.z);
            transform.forward = Vector3.Slerp(transform.forward, target, PlayerTrackingSpeed);
        }
        else
        {
            target = ray.direction;
            target = new Vector3(target.x, 0, target.z);
            transform.forward = Vector3.Slerp(transform.forward, target, PlayerTrackingSpeed);
        }
        Debug.DrawRay(ray.origin, ray.direction * LaserLenght, Color.cyan);
    }
}
/*
 * [Header("Player")]
    public GameObject Player;
 * 
target = (hit.point - Player.transform.position);
target = new Vector3(target.x, 0, target.z);
Player.transform.forward = Vector3.Slerp(Player.transform.forward, target, PlayerTrackingSpeed); 

target = ray.direction;
target = new Vector3(target.x, 0, target.z);
Player.transform.forward = Vector3.Slerp(Player.transform.forward, target, PlayerTrackingSpeed);
*/