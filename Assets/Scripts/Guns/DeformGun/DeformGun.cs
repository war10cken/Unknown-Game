using UnityEngine;

public class DeformGun : MonoBehaviour
{
    [Header("3DModels")]
    public Player.Player Player;
    public GameObject LaserModel;
    
    [Header("Raycast")]
    public float RayLenght = 100;
    public float MaxDistanceCollision = 100;
    
    RaycastHit hit;
    
    [Header("ShowingRay")]
    public float Duration = 5f;
    public GameObject RayOriginMark;
    public Material BeamMaterial;
    public float BeamDissapeareTime = 0.2f;
    
    [Header("Deformation")]
    public float DeformationForce = 0.1f;
    public float DeformationRadius = 1f;
    public float ForcePower = 5f;
    
    [Header("Velocities")]
    public float PlayerTrackingSpeed = 0.2f;
    public float DeformationGunTrackingSpeed = 0.2f;
    public float DeformationGunPositionTrackingSpeed = 0.2f;
    public float LaserSpeedRotation = 5f;
    
    [Header("Particles")]
    public ParticleSystem Particles;
    
    private ParticleSystem _instantiatedParticles;
    
    [Header("Vectors")]
    public Vector3 Offset = new(1, 0, 0);

    private void FixedUpdate()
    {
        Ray ray = new(RayOriginMark.transform.position, RayOriginMark.transform.forward);

        Debug.DrawRay(ray.origin, ray.direction * RayLenght, Color.red);

        if (Physics.Raycast(ray.origin, ray.direction * RayLenght, out hit, MaxDistanceCollision)
         && Input.GetButtonDown("Fire1") && hit.collider.gameObject.layer == 6)
        {
            ShowLaser();
            ObjectDeformation(ray);
            AddForceToObject(ray);
            InstantiateParticles(hit);
        }
    }

    private void InstantiateParticles(RaycastHit hit)
    {
        _instantiatedParticles = Instantiate(Particles, hit.point, Quaternion.identity);

        _instantiatedParticles.gameObject.SetActive(true);

        Destroy(_instantiatedParticles.gameObject, BeamDissapeareTime * 3);
    }

    private void ShowLaser()
    {
        GameObject beam = new ("Beam");
        beam.AddComponent<LineRenderer>();
        
        var gunBeam = beam.GetComponent<LineRenderer>();
        gunBeam.material = BeamMaterial;
        gunBeam.startWidth = 0.2f;
        gunBeam.endWidth = 0.1f;
        gunBeam.useWorldSpace = true;
        gunBeam.SetPosition(0, RayOriginMark.transform.position);
        gunBeam.SetPosition(1, hit.point);

        Destroy(beam, BeamDissapeareTime);
    }

    private void AddForceToObject(Ray ray)
    {
        //Получаем доступ к rigidbody объекта
        Rigidbody deformRigidbody = hit.collider.gameObject.GetComponent<Rigidbody>();
        //Откидываем объект
        deformRigidbody.AddForce(ray.direction * ForcePower);
    }

    private void ObjectDeformation(Ray ray)
    {
        Mesh deformingMesh = hit.collider.gameObject.GetComponent<MeshFilter>().mesh;

        GameObject gameObjectDeformingMesh = hit.collider.gameObject;

        //копируем все вершины меша.
        Vector3[] meshVertices = deformingMesh.vertices;
        //
        Vector3 hitPointLocalCoordinate = gameObjectDeformingMesh.transform.InverseTransformPoint(hit.point);

        for (int i = 0; i < meshVertices.Length; i++)
        {
            float distance = Vector3.Distance(hitPointLocalCoordinate, meshVertices[i]);
            
            if (DeformationRadius > distance)
            {
                meshVertices[i] += ray.direction.normalized * DeformationForce;
            }
        }

        deformingMesh.vertices = meshVertices;
        deformingMesh.RecalculateBounds();
        //deformingMesh.RecalculateNormals();

        //Destroy(gameObjectDeformingMesh.GetComponent<MeshCollider>());
        //gameObjectDeformingMesh.AddComponent<MeshCollider>().convex = true;
    }
}