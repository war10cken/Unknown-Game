using UnityEngine;

public class DeformGun : MonoBehaviour
{
    [Header("3DModels")]
    public GameObject _Player;
    public GameObject _LaserModel;
    [Header("Raycast")]
    public float _LaserLenght = 100;
    public float _MaxDistanceCollision = 100;
    RaycastHit hit;
    [Header("ShowingRay")]
    public float duration = 5f;
    public GameObject _RayOriginMark;
    public Material _Material;
    public float _BeamDissapeareTime = 0.2f;
    [Header("Deformation")]
    public float _forceDeform = 0.1f;
    public float _RadiusDeform = 1f;
    public float _Addforce = 5f;
    [Header("Velocities")]
    public float _PlayerTrackingSpeed = 0.2f;
    public float _DeformGunTrackingSpeed = 0.2f;
    public float _DeformGunPositionTrackingSpeed = 0.2f;
    public float _LaserSpeedRot = 5f;
    [Header("Particles")]
    public ParticleSystem _Particles;
    private ParticleSystem _instantiatedParticle;
    [Header("Vectors")]
    public Vector3 _offset = new(1, 0, 0);
    void FixedUpdate()
    {
        Ray ray = new(_RayOriginMark.transform.position, _RayOriginMark.transform.forward);

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
   
    void ShowLaser()
    {
        LineRenderer _GunBeam;

        GameObject _beam = new ("Beam");
        _beam.AddComponent<LineRenderer>();
        _GunBeam = _beam.GetComponent<LineRenderer>();
        _GunBeam.material = _Material;

        _GunBeam.startWidth = 0.2f;
        _GunBeam.endWidth = 0.1f;

        _GunBeam.useWorldSpace = true;

        _GunBeam.SetPosition(0, _RayOriginMark.transform.position);
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