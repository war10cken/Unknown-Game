using UnityEngine;

public class CamMove : MonoBehaviour
{
    public GameObject _Player;
    public float _PosSpeed = 0.1f;
    public float _posy = 1.5f, _posz = -10f;
    void FixedUpdate()
    {
        //Movement
        Vector3 _Pos = new Vector3(_Player.transform.position.x, _Player.transform.position.y + _posy, _Player.transform.position.z - _posz);
        transform.position = Vector3.Slerp(transform.position, _Pos, _PosSpeed);
    }
}