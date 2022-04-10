using UnityEngine;

public class ObjectsRotation : MonoBehaviour
{
    RaycastHit hit;
    //Vector3 _Raystart = new Vector3(0,0,0);
    public float _RayMaxdistance = 15f;
    public float _RotationSpeed = 2f;
    void Update()
    {
        //_Raystart = Input.mousePosition;
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_ray.origin, _ray.direction * _RayMaxdistance, out hit, _RayMaxdistance))
        {
            if (hit.collider) 
            {
                GameObject _hittedObject = hit.collider.gameObject;
                float X = Input.GetAxis("Mouse X") * _RotationSpeed;
                float Y = Input.GetAxis("Mouse Y") * _RotationSpeed;
                Vector3 _RotateDirection = new Vector3(0,-X,-Y);
                if (Input.GetButton("leftctrl"))
                {
                    //_hittedObject.transform.position = Vector3.Slerp(_hittedObject.transform.position,hit.point,0.1f);
                    _hittedObject.transform.Rotate(_RotateDirection);
                }
            }
        }
        Debug.DrawRay(transform.position, _ray.direction * _RayMaxdistance, Color.black);
    }
}