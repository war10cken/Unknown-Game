#region

using Enemy;
using UnityEngine;

#endregion

namespace Guns
{
    [RequireComponent(typeof(Rigidbody))]
    public class Selectable : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private float _forcePower;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.rigidbody is not null)
            {
                if (other.gameObject.TryGetComponent(out BaseEnemy enemy))
                {
                    enemy.TakeDamage(_forcePower);
                }
                
                if (_rigidbody.mass > other.rigidbody.mass)
                {
                    Destroy(other.gameObject);
                }
            }
        }

        public static void SetForcePower(float value, Selectable item)
        {
            item._forcePower = value;
        }
    }
}
