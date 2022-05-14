using Enemies;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Slider _health;

        [SerializeField] private Canvas _ui;
        [SerializeField] private Canvas _gameOverScreen;
        // 09.05.2022.
        [Header("Ragdoll")]
        Rigidbody[] Ragdoll;
        CapsuleCollider[] Capsule;
        BoxCollider[] Box;
        ConstantForce[] CForce;
        SphereCollider[] Sphere;
        private void Start()
        {
            Ragdoll = GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody rb in Ragdoll)
            {
                rb.isKinematic = true;
            }
            GetComponent<Rigidbody>().isKinematic = false;
            Capsule = GetComponentsInChildren<CapsuleCollider>();
            foreach (CapsuleCollider capsule in Capsule)
            {
                capsule.enabled = false;
            }

            Box = GetComponentsInChildren<BoxCollider>();
            foreach (BoxCollider box in Box)
            {
                box.enabled = false;
            }
            CForce = GetComponentsInChildren<ConstantForce>();
            foreach (ConstantForce force in CForce)
            {
                force.enabled = false;
            }
            Sphere = GetComponentsInChildren<SphereCollider>();
            foreach (SphereCollider sphere in Sphere)
            {
                sphere.enabled = false;
            }

            GetComponent<BoxCollider>().enabled = true;
        }
        void RagdollOff()
        {
            if (Input.GetButtonDown("NONE"))
            {
                Ragdoll = GetComponentsInChildren<Rigidbody>();
                foreach (Rigidbody rb in Ragdoll)
                {
                    rb.isKinematic = true;
                }
                GetComponent<BoxCollider>().enabled = true;
            }
        }
        void RagdollOn()
        {
            if (Input.GetButtonDown("CTRL"))
            {
                Capsule = GetComponentsInChildren<CapsuleCollider>();
                foreach (CapsuleCollider capsule in Capsule)
                {
                    capsule.enabled = true;
                }

                Box = GetComponentsInChildren<BoxCollider>();
                foreach (BoxCollider box in Box)
                {
                    box.enabled = true;
                }
                GetComponent<BoxCollider>().enabled = false;
                Destroy(GetComponent<Rigidbody>());

                Sphere = GetComponentsInChildren<SphereCollider>();
                foreach (SphereCollider sphere in Sphere)
                {
                    sphere.enabled = true;
                }
                CForce = GetComponentsInChildren<ConstantForce>();
                foreach (ConstantForce force in CForce)
                {
                    //force.enabled = true;
                }

                Ragdoll = GetComponentsInChildren<Rigidbody>();
                foreach (Rigidbody rb in Ragdoll)
                {
                    rb.isKinematic = false;
                }
                GetComponent<PlayerMovement>().enabled = false;
                GetComponent<PlayerTracking>().enabled = false;
                GetComponent<Jump>().enabled = false;
                GetComponent<Dash>().enabled = false;
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