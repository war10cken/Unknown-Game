using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFastTravel : MonoBehaviour
{
    [Header("Position1")]
    public Vector3 Point1;
    public Quaternion Rotation1;
    [Header("Position2")]
    public Vector3 Point2;
    public Quaternion Rotation2;

    Vector3 DefaultPosition;
    Quaternion DefaultRotation;
    private void Awake()
    {
        DefaultPosition = transform.position;
        DefaultRotation = transform.rotation;
    }
    void Update()
    {
        if (Input.GetButtonDown("F1"))
        {
            transform.SetPositionAndRotation(DefaultPosition, DefaultRotation);
        }else if (Input.GetButtonDown("F2")) 
        {
            transform.SetPositionAndRotation(Point1, Rotation1);
        }
    }
}
