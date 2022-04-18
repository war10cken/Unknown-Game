using DG.Tweening;
using UnityEngine;

namespace Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        [SerializeField] protected float Hp;
        [SerializeField] private float _damage;
        [SerializeField] private Player.Player _player;

        public virtual void TakeDamage(float damage)
        {
            Hp -= damage;
        }

        public virtual void DealDamage(Player.Player player)
        {
            player.TakeDamage(_damage);
        }

        private void Update()
        {
            if (_player is not null)
            {
                transform.DOMove(_player.transform.position, 8f);
            }
            
            
            if (Hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
