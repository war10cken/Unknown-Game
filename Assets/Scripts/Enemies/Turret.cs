using UnityEngine.UI;
using UnityEngine;
public class Turret : MonoBehaviour
{
    public GameObject Player;
    public float TurretRotationSpeed = 100f;
    [Header("RaycastToPlayer")]
    RaycastHit Hit;
    public float DistanceDetection = 10f;
    [Header("ShowingRay")]
    public GameObject BeamGameObject;
    public GameObject RayOriginMark;
    public GameObject RayOriginMark2;
    public Material RayMaterial;
    [Header("HP")]
    [SerializeField] private Slider _health;
    [SerializeField] private Player.Player _player;
    void Update()
    {
        Vector3 DirectionToPlayer = (Player.transform.position - transform.position).normalized;
        Ray Ray = new(transform.position, DirectionToPlayer * DistanceDetection);
        LayerMask Mask = LayerMask.GetMask("Player");
        if (Physics.Raycast(Ray, DistanceDetection, Mask))
        {
            TurretLook();
            Ray ray = new(transform.position, transform.forward * DistanceDetection);
            if ( Physics.Raycast(ray, out Hit, DistanceDetection, Mask) )
            {
                ShowLaser(RayOriginMark);
                ShowLaser(RayOriginMark2);
                _health.value -= 0.005f;
                _player.TakeDamage(1f);
            }//else{DestructLaser();}
        }

        Debug.DrawRay(transform.position, DirectionToPlayer * DistanceDetection, Color.black);
        Debug.DrawRay(RayOriginMark.transform.position, transform.forward * DistanceDetection, Color.yellow);
    }
    void TurretLook()
    {
        Vector3 DirectionToPlayer = (Player.transform.position - transform.position);
        Quaternion lookRotation = Quaternion.LookRotation(DirectionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, TurretRotationSpeed * Time.deltaTime);
    }
    void ShowLaser(GameObject RayOriginMark1)
    {
        GameObject beamGameObject = new("Beam");
        beamGameObject.AddComponent<LineRenderer>();

        LineRenderer gunBeam = beamGameObject.GetComponent<LineRenderer>();

        gunBeam.material = RayMaterial;

        gunBeam.startWidth = 0.2f;
        gunBeam.endWidth = 0.1f;

        gunBeam.useWorldSpace = true;

        gunBeam.SetPosition(0, RayOriginMark1.transform.position);
        gunBeam.SetPosition(1, Hit.point);

        Destroy(beamGameObject, 0.1f);
    }
    void DestructLaser()
    {
        BeamGameObject.GetComponent<LineRenderer>().SetPosition(0, Vector3.zero);
        BeamGameObject.GetComponent<LineRenderer>().SetPosition(1, Vector3.zero);
    }
}
