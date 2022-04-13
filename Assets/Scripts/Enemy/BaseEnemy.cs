using UnityEngine;

namespace Enemy
{
    public abstract class BaseEnemy : MonoBehaviour
    {
        [SerializeField] protected float Hp;

        public virtual void TakeDamage(float damage)
        {
            Hp -= damage;
        }

        private void Update()
        {
            if (Hp <= 0)
            {
                Hp = 0;
                Destroy(gameObject);
            }
        }
    }
}
