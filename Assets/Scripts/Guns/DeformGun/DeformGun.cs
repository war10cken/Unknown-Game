using Guns;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DeformGun : Gun
{
    [Header("Raycast")]
    public float LaserLenght = 100f;
    public float MaxDistanceCollision = 100f;
    RaycastHit hit;
    [Header("ShowingRay")]
    public GameObject BeamGameObject;
    public GameObject RayOriginMark;
    public Material RayMaterial;
    [Header("Deformation")]
    public float ForceDeform = 0.1f;
    public float RadiusDeform = 0.5f;
    public float AddforceToObject = 1000f;
    [Header("Particles")]
    public ParticleSystem HitPointParticle;
    public float ParticleDissapeareTime = 1f;

    private AudioSource _laserSound;
    private void Awake()
    {
        _laserSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        Ray ray = new(RayOriginMark.transform.position, RayOriginMark.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * LaserLenght, Color.red);
        LayerMask Mask = ~LayerMask.GetMask("Player");
        if (Physics.Raycast(ray.origin, ray.direction * LaserLenght, out hit, MaxDistanceCollision) && Input.GetButton("Fire1"))
        {
            ShowLaser();
            if (hit.collider.gameObject.GetComponent<Rigidbody>() != null)
            {
                LaserSoundOn();
                Deformation(ray);
                Addforce(ray);
                HitParticle(hit);
            }            
        }else
        {
            DestructLaser();
            LaserSoundOff();
        }
    }
    void LaserSoundOn() 
    {
        if (!_laserSound.isPlaying)
        {
            _laserSound.Play();
        }
    }
    void LaserSoundOff()
    {
        _laserSound.Stop();
    }

    void HitParticle(RaycastHit hit)
    {
        ParticleSystem instantiatedParticle;
        instantiatedParticle = Instantiate(HitPointParticle, hit.point, Quaternion.identity);

        instantiatedParticle.gameObject.SetActive(true);

        Destroy(instantiatedParticle.gameObject, ParticleDissapeareTime);
    }
   
    void ShowLaser()
    {
        LineRenderer gunBeam;

        gunBeam = BeamGameObject.GetComponent<LineRenderer>();

        gunBeam.startWidth = 0.2f;
        gunBeam.endWidth = 0.1f;

        gunBeam.useWorldSpace = true;

        gunBeam.SetPosition(0, RayOriginMark.transform.position);
        gunBeam.SetPosition(1, hit.point);
        //Debug.Log(hit.point);   
    }
    void DestructLaser()
    {
        BeamGameObject.GetComponent<LineRenderer>().SetPosition(0, Vector3.zero);
        BeamGameObject.GetComponent<LineRenderer>().SetPosition(1, Vector3.zero);
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

        var boxCollider = deformingGameObject.GetComponent<BoxCollider>();
        var meshCollider = deformingGameObject.GetComponent<MeshCollider>();
        
        if (boxCollider != null)
        {
            Destroy(boxCollider);
        }
        if (meshCollider == null)
        {
            deformingGameObject.AddComponent<MeshCollider>().convex = true;
        }
        if (meshCollider != null)
        {
            meshCollider.convex = false;
            meshCollider.convex = true;
        }
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
/*
if (Physics.Raycast(ray.origin, ray.direction * LaserLenght, out hit) && Input.GetButton("Fire1"))
{
    ShowLaser();
}
else
{
    beamGameObject.GetComponent<LineRenderer>().SetPosition(0, Vector3.zero);
    beamGameObject.GetComponent<LineRenderer>().SetPosition(1, Vector3.zero);
}
*/
/*ShowLaser
 * GameObject beamGameObject = new ("Beam");
 * beamGameObject.AddComponent<LineRenderer>(); 
 * gunBeam.material = RayMaterial;
 * Destroy(beamGameObject, BeamDissapeareTime);
 * public float BeamDissapeareTime = 0.2f;
*/
//if (Physics.Raycast(ray.origin, ray.direction * LaserLenght, out hit, MaxDistanceCollision) && Input.GetButton("Fire1") && hit.collider.gameObject.layer == 6)

//deformingGameObject.AddComponent<MeshCollider>().convex = true;
//Destroy(deformingGameObject.GetComponent<BoxCollider>());
//deformingGameObject.AddComponent<BoxCollider>();
