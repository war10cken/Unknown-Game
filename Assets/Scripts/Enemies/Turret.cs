using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Enemies
{
    public class Turret : Enemy
    {
        public float TurretRotationSpeed = 1f;
        [Header("RaycastToPlayer")] 
        public float DistanceDetection = 10f;
        
        [Header("ShowingRay")]
        public GameObject _upperBeamMark;
        public GameObject _bottomBeamMark;
        public Material RayMaterial;
        
        [Header("HP")]
        [SerializeField] private Slider _health;

        private RaycastHit _hit;
        private Vector3 _directionToPlayer;

        Ray ray;
        Ray rayFromMark;
        private void Update()
        {
            _directionToPlayer = _player.transform.position - transform.position;
            ray = new(transform.position, _directionToPlayer.normalized * DistanceDetection);
            //LayerMask mask = LayerMask.GetMask("Player");
            if ( Physics.Raycast(ray, DistanceDetection) )
            {
                TurretLook(_directionToPlayer);
                rayFromMark = new(transform.position, transform.forward * DistanceDetection);
                if ( Physics.Raycast(rayFromMark, out _hit, DistanceDetection) && _hit.collider.gameObject.CompareTag("Player") )
                {
                    ShowLaser(_upperBeamMark);
                    ShowLaser(_bottomBeamMark);

                    _health.value -= 0.005f;
                    DealDamage(_player);
                    Debug.DrawRay(transform.position, transform.forward * DistanceDetection, Color.green);
                }else
                { HideLaser(_upperBeamMark); HideLaser(_bottomBeamMark); }
            }
            Debug.DrawRay(transform.position, _directionToPlayer.normalized * DistanceDetection, Color.black);
            Debug.DrawRay(_upperBeamMark.transform.position, transform.forward * DistanceDetection, Color.yellow);
        }

        private void TurretLook(Vector3 direction)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, TurretRotationSpeed * Time.deltaTime);
        }

        private void ShowLaser(GameObject beamMark)
        {
            LineRenderer gunBeam = beamMark.GetComponent<LineRenderer>();

            gunBeam.material = RayMaterial;

            gunBeam.startWidth = 0.2f;
            gunBeam.endWidth = 0.1f;

            gunBeam.useWorldSpace = true;
        
            gunBeam.SetPosition(0, beamMark.transform.position);
            gunBeam.SetPosition(1, _hit.point);

            beamMark.SetActive(true);
        }
        void HideLaser(GameObject beamMark)
        { beamMark.SetActive(false); }
    }
}
/*
private void DestructLaser()
{
    BeamGameObject.GetComponent<LineRenderer>().SetPosition(0, Vector3.zero);
    BeamGameObject.GetComponent<LineRenderer>().SetPosition(1, Vector3.zero);
}
transform.DORotateQuaternion(lookRotation, TurretRotationSpeed * Time.deltaTime);
*/