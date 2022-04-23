using Guns;
using UnityEngine;

public class DeformGun : Gun
{
    [Header("Raycast")]
    public float LaserLenght = 100f;
    public float MaxDistanceCollision = 100f;
    RaycastHit hit;
    [Header("ShowingRay")]
    public float BeamDissapeareTime = 0.2f;
    public GameObject RayOriginMark;
    public Material RayMaterial;
    [Header("Deformation")]
    public float ForceDeform = 0.1f;
    public float RadiusDeform = 0.5f;
    public float AddforceToObject = 1000f;
    [Header("Particles")]
    public ParticleSystem HitPointParticle;
    void FixedUpdate()
    {
        Ray ray = new(RayOriginMark.transform.position, RayOriginMark.transform.forward);

        Debug.DrawRay(ray.origin, ray.direction * LaserLenght, Color.red);

        if (Physics.Raycast(ray.origin, ray.direction * LaserLenght, out hit, MaxDistanceCollision)
         && Input.GetButtonDown("Fire1") && hit.collider.gameObject.layer == 6)
        {
            // TrackMouse(hit);
            ShowLaser();
            Deformation(ray);
            Addforce(ray);
            HitParticle(hit);
        }
    }
    void HitParticle(RaycastHit hit)
    {
        ParticleSystem instantiatedParticle;
        instantiatedParticle = Instantiate(HitPointParticle, hit.point, Quaternion.identity);

        instantiatedParticle.gameObject.SetActive(true);

        Destroy(instantiatedParticle.gameObject, BeamDissapeareTime * 3);
    }
   
    void ShowLaser()
    {
        LineRenderer gunBeam;

        GameObject beamGameObject = new ("Beam");

        beamGameObject.AddComponent<LineRenderer>();
        gunBeam = beamGameObject.GetComponent<LineRenderer>();
        gunBeam.material = RayMaterial;

        gunBeam.startWidth = 0.2f;
        gunBeam.endWidth = 0.1f;

        gunBeam.useWorldSpace = true;

        gunBeam.SetPosition(0, RayOriginMark.transform.position);
        gunBeam.SetPosition(1, hit.point);

        Destroy(beamGameObject, BeamDissapeareTime);
    }
    void Addforce(Ray ray)
    {
        // Получаем доступ к rigidbody объекта.
        Rigidbody deformMeshRigidbody = hit.collider.gameObject.GetComponent<Rigidbody>();
        // Откидываем объект.
        deformMeshRigidbody.AddForce(ray.direction * AddforceToObject);
    }
    void Deformation(Ray ray)
    {
        Mesh deformingMesh = hit.collider.gameObject.GetComponent<MeshFilter>().mesh;

        GameObject deformingGameObject = hit.collider.gameObject;

        // Копируем все вершины меша.
        Vector3[] copyMeshVertices = deformingMesh.vertices;
        // Calculate the transform's position relative to the DeformingGameObject.
        Vector3 hitPointToLocalCoordinatesMesh = deformingGameObject.transform.InverseTransformPoint(hit.point);

        for (int i = 0; i < copyMeshVertices.Length; i++)
        {
            // Находим дистанцию между точкой столкновения луча и вершиной меша.
            float _Distance = (float)Vector3.Distance(hitPointToLocalCoordinatesMesh, copyMeshVertices[i]);
            if (RadiusDeform > _Distance)
            {
                copyMeshVertices[i] += ray.direction.normalized * ForceDeform;
            }
        }

        deformingMesh.vertices = copyMeshVertices;
        deformingMesh.RecalculateBounds();
        //deformingMesh.RecalculateNormals();

        Destroy(deformingGameObject.GetComponent<BoxCollider>());
        deformingGameObject.AddComponent<BoxCollider>();
        //deformingGameObject.AddComponent<MeshCollider>().convex = true;
    }
}
//public float LaserSpeedRotation = 5f;
//public GameObject LaserObject;
//public float PlayerTrackingSpeed = 0.2f;
//public float DeformGunTrackingSpeed = 0.2f;
//[Header("Vectors")]
//public Vector3 Offset = new(1, 0, 0);
//public float DeformGunPositionTrackingSpeed = 0.2f;

//[Header("Objects")]
//public GameObject Player;
//public float duration = 5f;

//private ParticleSystem InstantiatedParticle;