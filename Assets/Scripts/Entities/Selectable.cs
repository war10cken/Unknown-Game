#region

using Enemies;
using UnityEngine;

#endregion

namespace Entities
{
    [RequireComponent(typeof(Rigidbody))]
    public class Selectable : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private float _damage;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.rigidbody is not null)
            {
                if (other.gameObject.TryGetComponent(out Enemy enemy))
                {
                    enemy.TakeDamage(_damage);
                }

                if (_rigidbody.mass > other.rigidbody.mass)
                {
                    Destroy(other.gameObject);
                }
            }
        }

        public static void SetDamage(float value, Selectable item)
        {
            item._damage = value;
        }
    }
}
