using UnityEngine;

public class DeformGun : MonoBehaviour
{
    [Header("Raycast")]
    public float _LaserLenght = 100;
    public float _MaxDistanceCollision = 100;
    RaycastHit hit;
    RaycastHit hit2;
    [Header("ShowingRay")]
    public float duration = 5f;
    public GameObject _LineRendererMark;
    public Material _Material;
    public float _BeamDissapeareTime = 0.2f;
    [Header("Deformation")]
    public float _forceDeform = 0.1f;
    public float _RadiusDeform = 1f;
    public float _Addforce = 5f;
    [Header("3DModels")]
    public GameObject _LaserModel;
    public Vector3 _offset;
    public float _LaserSpeedRot = 5f;
    [Header("Player")]
    public GameObject _Player;
    [Header("Velocities")]
    public float _PlayerTrackingSpeed = 0.2f;
    public float _DeformGunTrackingSpeed = 0.2f;
    public float _DeformGunPositionTrackingSpeed = 0.2f;
    [Header("Particles")]
    public ParticleSystem _Particles;
    private ParticleSystem _instantiatedParticle;

    PlayerTracking playertracking;
    void FixedUpdate()
    {
        Ray ray = new(_LineRendererMark.transform.position, _LineRendererMark.transform.forward);
        Ray ray2 = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        GunPosRot();
        DeformGunTracking(ray2);

        Debug.DrawRay(ray.origin, ray.direction * _LaserLenght, Color.red);

        if (Physics.Raycast(ray.origin, ray.direction * _LaserLenght, out hit, _MaxDistanceCollision) && Input.GetButtonDown("Fire1") && hit.collider.gameObject.layer == 6)
        {
            ShowLaser();
            Deformation(ray);
            AddforceToObject(ray);
            Particles(hit);
        }
    }
    void Particles(RaycastHit hit)
    {
        _instantiatedParticle = Instantiate(_Particles, hit.point, Quaternion.identity);

        _instantiatedParticle.gameObject.SetActive(true);

        Destroy(_instantiatedParticle.gameObject, _BeamDissapeareTime * 3);
    }
    void GunPosRot()
    {
        Vector3 targetPos = _Player.transform.TransformPoint(_offset);
        _LaserModel.transform.position = Vector3.MoveTowards(_LaserModel.transform.position, targetPos, _DeformGunPositionTrackingSpeed);
        if (hit.point == Vector3.zero)
        {
            _LaserModel.transform.rotation = Quaternion.RotateTowards(_LaserModel.transform.rotation, _Player.transform.rotation, _LaserSpeedRot);
        }
    }
    void DeformGunTracking(Ray ray2)
    {
        if (Physics.Raycast(ray2.origin, ray2.direction * _LaserLenght, out hit2))
        {
            Vector3 _Dir = (hit2.point - _LaserModel.transform.position).normalized;
            Quaternion _lookRotation = Quaternion.LookRotation(_Dir);
            _LaserModel.transform.rotation = Quaternion.Slerp(_LaserModel.transform.rotation, _lookRotation, _DeformGunTrackingSpeed);
            Debug.DrawRay(_LaserModel.transform.position, _Dir, Color.blue);
        }
    }
    void ShowLaser()
    {
        LineRenderer _GunBeam;

        GameObject _beam = new GameObject("Beam");
        _beam.AddComponent<LineRenderer>();
        _GunBeam = _beam.GetComponent<LineRenderer>();
        _GunBeam.material = _Material;

        _GunBeam.startWidth = 0.2f;
        _GunBeam.endWidth = 0.1f;

        _GunBeam.useWorldSpace = true;

        _GunBeam.SetPosition(0, _LineRendererMark.transform.position);
        _GunBeam.SetPosition(1, hit.point);

        Destroy(_beam, _BeamDissapeareTime);
    }
    void AddforceToObject(Ray ray)
    {
        //Получаем доступ к rigidbody объекта
        Rigidbody _DeformRigidbody = hit.collider.gameObject.GetComponent<Rigidbody>();
        //Откидываем объект
        _DeformRigidbody.AddForce(ray.direction * _Addforce);
    }
    void Deformation(Ray ray)
    {
        Mesh _deformingMesh = hit.collider.gameObject.GetComponent<MeshFilter>().mesh;

        GameObject _GameObjectDeformingMesh = hit.collider.gameObject;

        //копируем все вершины меша.
        Vector3[] _MeshVertices = _deformingMesh.vertices;
        //
        Vector3 _HitPointLocalCoor = _GameObjectDeformingMesh.transform.InverseTransformPoint(hit.point);

        for (int i = 0; i < _MeshVertices.Length; i++)
        {
            float _Distance = (float)Vector3.Distance(_HitPointLocalCoor, _MeshVertices[i]);
            if (_RadiusDeform > _Distance)
            {
                _MeshVertices[i] += ray.direction.normalized * _forceDeform;
            }
        }

        _deformingMesh.vertices = _MeshVertices;
        _deformingMesh.RecalculateBounds();
        //_deformingMesh.RecalculateNormals();

        //Destroy(_GameObjectDeformingMesh.GetComponent<MeshCollider>());
        //_GameObjectDeformingMesh.AddComponent<MeshCollider>().convex = true;
    }
}

/*
 * //PlayerTracking(ray2);
    void PlayerTracking(Ray ray2)
    {
        Vector3 _target;
        if (Physics.Raycast(ray2.origin, ray2.direction * _LaserLenght, out hit2))
        {
            _target = (hit2.point - _Player.transform.position);
            _target = new Vector3(_target.x, 0, _target.z);
            _Player.transform.forward = Vector3.Slerp(_Player.transform.forward, _target, _PlayerTrackingSpeed);
        }
        Debug.DrawRay(ray2.origin, ray2.direction * _LaserLenght, Color.cyan);
    }
    */