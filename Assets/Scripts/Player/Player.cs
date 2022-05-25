using Enemies;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    // [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerTracking))]
    [RequireComponent(typeof(Jump))]
    [RequireComponent(typeof(Dash))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(AudioSource))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private Slider _health;

        [SerializeField] private Canvas _ui;
        [SerializeField] private Canvas _gameOverScreen;
        // 09.05.2022.
        [Header("Ragdoll")]
        private Rigidbody[] _ragdoll;
        private CapsuleCollider[] _capsuleColliders;
        private BoxCollider[] _boxColliders;
        private ConstantForce[] _constantForces;
        private SphereCollider[] _sphereColliders;

        // private BoxCollider _boxCollider;
        private Rigidbody _rigidbody;
        private PlayerMovement _playerMovement;
        private PlayerTracking _playerTracking;
        private Jump _jump;
        private Dash _dash;
        private CapsuleCollider PlayerCapsule;
        private void Awake()
        {
            //_boxCollider = GetComponent<BoxCollider>();
            _ragdoll = GetComponentsInChildren<Rigidbody>();
            _capsuleColliders = GetComponentsInChildren<CapsuleCollider>();
            _boxColliders = GetComponentsInChildren<BoxCollider>();
            _constantForces = GetComponentsInChildren<ConstantForce>();
            _sphereColliders = GetComponentsInChildren<SphereCollider>();
        }

        private void Start()
        {
            // RigidBody Settings.
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.mass = 50;
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            _rigidbody.angularDrag = 5;
            _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
            _rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            // CapsuleCollider Settings.
            PlayerCapsule = GetComponent<CapsuleCollider>();
            PlayerCapsule.height = 0.76f;
            PlayerCapsule.radius = 0.1f;
            PlayerCapsule.center = new(0, 0.34f, 0);
            //
            foreach (var rigidbody in _ragdoll)
            {
                rigidbody.isKinematic = true;
            }
            
            foreach (var constantForce in _constantForces)
            {
                constantForce.enabled = false;
            }
            foreach (var Capsule in _capsuleColliders)
            {
                Capsule.enabled = false;
            }
            foreach (var Box in _boxColliders)
            {
                Box.enabled = false;
            }
            foreach (var Sphere in _sphereColliders)
            {
                Sphere.enabled = false;
            }
            
            _rigidbody.isKinematic = false;
            PlayerCapsule.enabled = true;

            _playerMovement = GetComponent<PlayerMovement>();
            _playerTracking = GetComponent<PlayerTracking>();
            _jump = GetComponent<Jump>();
            _dash = GetComponent<Dash>();
        }

        private void RagdollOff()
        {
            foreach (var rigidbody in _ragdoll)
            {
                rigidbody.isKinematic = true;
            }   
            //_boxCollider.enabled = true;
        }
        
        private void RagdollOn()
        {
            if (Input.GetButtonDown("F1"))
            {
                foreach (var rigidbody in _ragdoll)
                {
                    rigidbody.isKinematic = false;
                }
                foreach (var constantForce in _constantForces)
                {
                    constantForce.enabled = true;
                }
                foreach (var Capsule in _capsuleColliders)
                {
                    Capsule.enabled = true;
                }
                foreach (var Box in _boxColliders)
                {
                    Box.enabled = true;
                }
                foreach (var Sphere in _sphereColliders)
                {
                    Sphere.enabled = true;
                }

                _playerMovement.enabled = false;
                _playerTracking.enabled = false;
                _jump.enabled = false;
                _dash.enabled = false;
                PlayerCapsule.enabled = false;

                _rigidbody.isKinematic = true;
                _rigidbody.mass = 0;
                /*
                _constantForces = GetComponentsInChildren<ConstantForce>();
                foreach (ConstantForce force in _constantForces)
                {
                     force.enabled = true;
                }
                */
            }
        }

        private void ChangeEnabledState<T>(T[] array, bool state)
            where T : Collider
        {
            foreach (var item in array)
            {
                item.enabled = state;
            }
        }
        
        //
        public void TakeDamage(float damage)
        {
            _health.value -= damage / 1000f;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out Enemy enemy))
            {
                enemy.DealDamage(this);
            }
        }

        private void OnCollisionStay(Collision other)
        {
            if (other.gameObject.TryGetComponent(out Enemy enemy))
            {
                enemy.DealDamage(this);
            }
        }

        private void Update()
        {
            if (_health.value <= 0)
            {
                Time.timeScale = 0;
                _ui.gameObject.SetActive(false);
                _gameOverScreen.gameObject.SetActive(true);
            }
            //25.05.2022
            RagdollOn();
            //
        }
    }
}

/*
                 Ragdoll = GetComponentsInChildren<Rigidbody>();
                foreach (Rigidbody rb in Ragdoll)
                {
                    rb.isKinematic = false;
                } 
*/