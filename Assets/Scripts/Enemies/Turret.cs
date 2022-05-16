using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Enemies
{
    public class Turret : Enemy
    {
        public float TurretRotationSpeed = 100f;
        [Header("RaycastToPlayer")] 
        public float DistanceDetection = 10f;
        
        [Header("ShowingRay")]
        public GameObject BeamGameObject;
        public GameObject RayOriginMark;
        public GameObject RayOriginMark2;
        public Material RayMaterial;
        
        [Header("HP")]
        [SerializeField] private Slider _health;

        private RaycastHit _hit;
        private GameObject _bottomBeam;
        private GameObject _upperBeam;
        private Vector3 _directionToPlayer;

        private void Awake()
        {
            _bottomBeam = new GameObject("BottomTurretBeam");
            _bottomBeam.SetActive(false);
            _bottomBeam.AddComponent<LineRenderer>();
        
            _upperBeam = new GameObject("UpperTurretBeam");
            _upperBeam.SetActive(false);
            _upperBeam.AddComponent<LineRenderer>();
        }

        private void FixedUpdate()
        {
            _directionToPlayer = _player.transform.position - transform.position;

            Ray ray = new(transform.position, _directionToPlayer.normalized * DistanceDetection);
            LayerMask mask = LayerMask.GetMask("Player");
            
            if (Physics.Raycast(ray, DistanceDetection, mask))
            {
                TurretLook(_directionToPlayer);
                // Ray ray = new(transform.position, transform.forward * DistanceDetection);
                if (Physics.Raycast(ray, out _hit, DistanceDetection, mask))
                {
                    ShowLaser(RayOriginMark, _upperBeam);
                    ShowLaser(RayOriginMark2, _bottomBeam);
                    _health.value -= 0.005f;
                    DealDamage(_player);
                } //else{DestructLaser();}
            }
            else
            {
                _bottomBeam.SetActive(false);
                _upperBeam.SetActive(false);
            }

            Debug.DrawRay(transform.position, _directionToPlayer.normalized * DistanceDetection, Color.black);
            Debug.DrawRay(RayOriginMark.transform.position, transform.forward * DistanceDetection, Color.yellow);
        }

        private void TurretLook(Vector3 direction)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.DORotateQuaternion(lookRotation, 1f);
            // transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, TurretRotationSpeed); //* Time.deltaTime);
        }

        private void ShowLaser(GameObject RayOriginMark1, GameObject beam)
        {
            LineRenderer gunBeam = beam.GetComponent<LineRenderer>();

            gunBeam.material = RayMaterial;

            gunBeam.startWidth = 0.2f;
            gunBeam.endWidth = 0.1f;

            gunBeam.useWorldSpace = true;
        
            gunBeam.SetPosition(0, RayOriginMark1.transform.position);
            gunBeam.SetPosition(1, _hit.point);

            beam.SetActive(true);
        }

        private void DestructLaser()
        {
            BeamGameObject.GetComponent<LineRenderer>().SetPosition(0, Vector3.zero);
            BeamGameObject.GetComponent<LineRenderer>().SetPosition(1, Vector3.zero);
        }
    }
}
