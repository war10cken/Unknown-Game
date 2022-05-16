using System;
using System.Collections;
using Enemies;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerTracking))]
    [RequireComponent(typeof(Jump))]
    [RequireComponent(typeof(Dash))]
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

        private Rigidbody _rigidbody;
        private BoxCollider _boxCollider;
        private PlayerMovement _playerMovement;
        private PlayerTracking _playerTracking;
        private Jump _jump;
        private Dash _dash;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _boxCollider = GetComponent<BoxCollider>();
            _ragdoll = GetComponentsInChildren<Rigidbody>();
            _capsuleColliders = GetComponentsInChildren<CapsuleCollider>();
            _boxColliders = GetComponentsInChildren<BoxCollider>();
            _constantForces = GetComponentsInChildren<ConstantForce>();
            _sphereColliders = GetComponentsInChildren<SphereCollider>();
        }

        private void Start()
        {
            foreach (var rigidbody in _ragdoll)
            {
                rigidbody.isKinematic = true;
            }
            
            foreach (var constantForce in _constantForces)
            {
                constantForce.enabled = false;
            }
            
            ChangeEnabledState(_capsuleColliders, false);
            ChangeEnabledState(_boxColliders, false);
            ChangeEnabledState(_sphereColliders, false);
            
            _rigidbody.isKinematic = false;
            _boxCollider.enabled = true;
        }

        private void RagdollOff()
        {
            if (Input.GetButtonDown("NONE"))
            {
                foreach (var rigidbody in _ragdoll)
                {
                    rigidbody.isKinematic = true;
                }
                
                _boxCollider.enabled = true;
            }
        }
        
        private void RagdollOn()
        {
            if (Input.GetButtonDown("CTRL"))
            {
                ChangeEnabledState(_capsuleColliders, true);
                ChangeEnabledState(_boxColliders, true);
                ChangeEnabledState(_sphereColliders, true);
                
                // _constantForces = GetComponentsInChildren<ConstantForce>();
                // foreach (ConstantForce force in _constantForces)
                // {
                //     //force.enabled = true;
                // }

                foreach (Rigidbody rb in _ragdoll)
                {
                    rb.isKinematic = false;
                }
                
                _playerMovement.enabled = false;
                _playerTracking.enabled = false;
                _jump.enabled = false;
                _dash.enabled = false;
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
            // 09.05.2022.
            if (other.gameObject.transform.position.sqrMagnitude > 1)
            {
                RagdollOff();
            }
            //
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
            // 09.05.2022.
            RagdollOff();
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