using System;
using UnityEngine;

namespace Enemy
{
    public abstract class BaseEnemy : MonoBehaviour
    {
        [SerializeField] private float _hp;

        public virtual void TakeDamage(float damage)
        {
            _hp -= damage;
        }

        private void Update()
        {
            if (_hp <= 0)
            {
                _hp = 0;
                Destroy(gameObject);
            }
        }
    }
}
