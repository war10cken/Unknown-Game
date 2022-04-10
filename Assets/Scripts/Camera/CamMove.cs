using UnityEngine;

public class CamMove : MonoBehaviour
{
    public GameObject _Player;
    public float _PosSpeed = 0.1f;
    public float  _posy = 1.5f, _posz = -10f;
    void FixedUpdate()
    {
        //Movement
        Vector3 _Pos = new Vector3(_Player.transform.position.x, _Player.transform.position.y + _posy, _Player.transform.position.z - _posz);
        transform.position = Vector3.Slerp(transform.position, _Pos, _PosSpeed);
    }
}

//Quaternion _OriginRot;
//Rotation
//_OriginRot = transform.rotation;
//
//Rotation
/*
Quaternion _Rotation = new Quaternion(_rotx, _roty, 0, 0);
Quaternion _rot = Quaternion.Slerp(_OriginRot, _Rotation, _RotSpeed);
transform.rotation = _rot;
*/


//public float _RotSpeed = 0.1f;
//posrot
//public float _rotx = 0f, _roty = 0.7f;

//public float _MouseSpeed = 1;
/*
float _MouseX = Input.GetAxis("Mouse X") * _MouseSpeed;
float _MouseY = Input.GetAxis("Mouse Y") * _MouseSpeed;
*/